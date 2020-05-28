using SIM.Core.Attributes;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Factory
{
    public class SimNodeConstructionArgument<T> : ISimNodeConstructionArgument where T : Node, new()
    {
        public SimNodeConstructionArgument()
        {
            ObjectType = typeof(T);
            Arguments = GetConstructionArguments();
        }

        public void Populate(T obj)
        {
            for (int i = 0; i < Arguments.Length; i++)
            {
                ArgumentValues[i].GetType().GetProperty("Origin").SetValue(ArgumentValues[i], obj);
                Arguments[i].SetValue(obj, ArgumentValues[i]);
            }
        }

        public PropertyInfo[] GetConstructionArguments()
        {
            return ObjectType.GetProperties()
                .Where(a => a.CustomAttributes.Where(b => b.AttributeType == typeof(RequiredAttribute)).Count() != 0 )
                .Select(a => a).ToArray();
        }

        public void AssignArgumentValues(params INode[] values)
        {
            // Check if size of passed in values are the same as excpected
            if (Arguments.Length != values.Length)
                throw new ArgumentException($"Not enough data provided to construct an instance of {ObjectType}");

            // Loop through and assign
            ArgumentValues = new IRelation[Arguments.Length];
            for (int i = 0; i < ArgumentValues.Length; i++)
            {
                ValidateAndAssignValue(i, values[i]);
            }
        }

        public IEnumerable<Type> GetExpectedTypes()
        {
            return Arguments.Select(c => c.GetCustomAttribute<PropertyNodeTypeAttribute>().AllowedType);
        }

        private void ValidateAndAssignValue(int i, INode v)
        {
            ValidateValue(i, v);

            if (v is PropertyNode)
                AssignValue(i, v as PropertyNode);

            else
                AssignValue(i, v as Node);
        }

        private void AssignValue(int i, Node node)
        {
            // Find proper relation
            Type relationType = GetProperRelation(node);

            var relation = (Activator.CreateInstance(relationType) as Relation);
            relation.Origin = Object;
            relation.Target = node;
            ArgumentValues[i] = relation;
        }

        private Type GetProperRelation(Node node)
        {
            // Get Assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains("SIM."))
                .Except(new Assembly[] { Assembly.GetAssembly(GetType()) });

            // Get context assembly
            var contextAssembly = assemblies
                .Where(a => a.GetTypes().Where(b => b.GetInterface(nameof(ISimObject)) != null).Count() != 0)
                .FirstOrDefault();

            // Get Relation types
            return contextAssembly.GetTypes()
                .Where(a => a.GetCustomAttributes<RelationEndTypeAttribute>().Where(b => b.Node == "Target" && b.Type == node.GetType()).Count() != 0)
                .FirstOrDefault();
        }

        private void AssignValue(int i, PropertyNode propertyNode)
        {
            ArgumentValues[i] = new PropertyRelation() { Target = propertyNode };
        }

        private void ValidateValue(int i, INode v)
        {
            // Check the type
            var expextedType = v is PropertyNode ? Arguments[i].GetCustomAttribute<PropertyNodeTypeAttribute>().AllowedType :
                Arguments[i].GetCustomAttribute<PropertyRelationTargetTypeAttribute>().TargetType;
            if (v.GetType() != expextedType)
                throw new ArgumentException($"Provided value of type {v.GetType()} " +
                    $"not expected.\nExpected {expextedType}");
        }

        public Node Object { get; }
        public Type ObjectType { get; }
        public PropertyInfo[] Arguments { get; }
        public IRelation[] ArgumentValues { get; private set; }
    }
}
