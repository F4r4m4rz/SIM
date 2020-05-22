using SIM.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class SimNodeConstructionArgument<T> where T : Node, new()
    {
        public SimNodeConstructionArgument()
        {
            ObjectType = typeof(T);
            Arguments = GetConstructionArguments();
        }

        internal void Populate(T obj)
        {
            for (int i = 0; i < Arguments.Length; i++)
            {
                ArgumentValues[i].Origin = obj;
                Arguments[i].SetValue(obj, ArgumentValues[i]);
            }
        }

        private PropertyInfo[] GetConstructionArguments()
        {
            return ObjectType.GetProperties()
                .Where(a => a.PropertyType == typeof(Relation) || 
                           (a.CustomAttributes.Where(b => b.AttributeType == typeof(RequiredAttribute)).Count() != 0 ))
                .Select(a => a).ToArray();
        }

        public void AssignArgumentValues(params PropertyNode[] values)
        {
            // Check if size of passed in values are the same as excpected
            if (Arguments.Length != values.Length)
                throw new ArgumentException($"Not enough data provided to construct an instance of {ObjectType}");

            // Loop through and assign
            ArgumentValues = new PropertyRelation[Arguments.Length];
            for (int i = 0; i < ArgumentValues.Length; i++)
            {
                ValidateAndAssignValue(i, values[i]);
            }
        }

        public IEnumerable<Type> GetExpectedTypes()
        {
            return Arguments.Select(c => c.GetCustomAttribute<PropertyNodeTypeAttribute>().AllowedType);
        }

        private void ValidateAndAssignValue(int i, PropertyNode v)
        {
            // Check the type
            var expextedType = Arguments[i].GetCustomAttribute<PropertyNodeTypeAttribute>().AllowedType;
            if (v.GetType() != expextedType)
                throw new ArgumentException($"Provided value of type {v.GetType()} " +
                    $"not expected.\nExpected {expextedType}");

            ArgumentValues[i] = new PropertyRelation() { Target = v };
        }

        public Node Object { get; }
        public Type ObjectType { get; }
        public PropertyInfo[] Arguments { get; }
        public PropertyRelation[] ArgumentValues { get; private set; }
    }
}
