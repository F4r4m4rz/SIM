using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public abstract class Node : INode
    {
        protected Node()
        {
            Properties = new Dictionary<string, object>();
            ProvidePropertyKeys();
        }

        private void ProvidePropertyKeys()
        {
            var allProperties = GetType().GetProperties();
            foreach (var prop in allProperties)
            {
                if (prop.Name == nameof(Properties)) continue;
                Properties.Add(prop.Name, null);
            }
        }

        public IDictionary<string, object> Properties { get; set; }
        public string Status { get; protected set; }

        /// <summary>
        /// Collection of applicable outwards/inwards relations on the Node
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Relation> Relations(ISimRepository repository)
        {
            List<Relation> relations = new List<Relation>();
            relations.AddRange(InwardsRelations(repository));
            relations.AddRange(OutwardsRelations(repository));
            return relations.AsReadOnly();
        }

        /// <summary>
        /// Collection of applicable inwards relations on the Node
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Relation> InwardsRelations(ISimRepository repository)
        {
            return repository.GetAll(a => a is Relation && ((a as Relation).Target == this))
                .Select(a => a as Relation)
                .ToList().AsReadOnly();
        }

        /// <summary>
        /// Collection of applicable outwards relations on the Node
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Relation> OutwardsRelations(ISimRepository repository)
        {
            return repository.GetAll(a => a is Relation && ((a as Relation).Origin == this))
                .Select(a => a as Relation)
                .ToList().AsReadOnly();
        }

        public Relation RelateTo<T>(Node node, bool isOutward) where T : Relation, new()
        {
            if (isOutward) return CreateRelation<T>(this, node);
            else return CreateRelation<T>(node, this);
        }

        public Relation RelateTo(Type relationType, Node node, bool isOutward)
        {
            var method = GetType().GetMethod(nameof(RelateTo), new Type[] { typeof(Node), typeof(bool) });
            var genericMethod = method.MakeGenericMethod(relationType);
            return genericMethod.Invoke(this, new object[] { node, isOutward }) as Relation;
        }

        private Relation CreateRelation<T>(Node origin, Node target) where T : Relation, new()
        {
            var relation = new T() { Origin = origin, Target = target };
            Validator.ValidateObject(relation, new ValidationContext(relation), true);
            return relation;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check all required properites are in place
            var requiredProperties = GetType().GetProperties()
                .Where(a => a.GetCustomAttribute<RequiredAttribute>() != null)
                .Where(a => a.GetValue(this) == null);

            if (requiredProperties == null || requiredProperties.Count() == 0)
                return new ValidationResult[] { ValidationResult.Success };

            return new ValidationResult[] { new ValidationResult("Required properties not set", requiredProperties.Select(a => a.Name)) };
        }

        protected virtual void SetState(string state = null)
        {
            Status = state;
            OnStateChanged();
        }

        protected virtual void OnInitialize()
        {
            // Custom implementation from the derived types
        }

        protected virtual void OnStateChanged()
        {
            // Custom implementation from the derived types
        }

        protected virtual void OnDelete()
        {
            // Custom implementation from the derived types
        }

        protected virtual void OnPropertyChanged()
        {
            // Custom implementation from the derived types
        }

        protected virtual object GetProperty(string key)
        {
            if (Properties.ContainsKey(nameof(key)))
            {
                return Properties[nameof(key)] as string;
            }
            else
                return null;
        }

        protected virtual void SetProperty(string key, object value)
        {
            Properties[key] = value;
        }
    }
}
