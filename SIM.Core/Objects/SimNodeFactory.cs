using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Core.Objects
{
    public class SimNodeFactory
    {
        public virtual T New<T>(SimNodeConstructionArgument<T> constructionArgument) where T: Node, new()
        {
            var obj = new T();
            constructionArgument.Populate(obj);
            Validator.ValidateObject(obj, new ValidationContext(obj), true);
            return obj;
        }

        public virtual Node New(ISimNodeConstructionArgument constructionArgument)
        {
            var method = GetType().GetMethods().Where(a => a.Name == nameof(New) && a.IsGenericMethod).FirstOrDefault();
            var genericMethod = method.MakeGenericMethod(constructionArgument.GetType().GetGenericArguments());
            return genericMethod.Invoke(this, new[] { constructionArgument }) as Node;
        }

        public virtual SimNodeConstructionArgument<T> GetConstructionArguments<T>() where T : Node, new()
        {
            return new SimNodeConstructionArgument<T>();
        }

        public virtual ISimNodeConstructionArgument GetConstructionArguments(Type type)
        {
            var method = GetType().GetMethod(nameof(GetConstructionArguments), new Type[] { });
            var genericMethod = method.MakeGenericMethod(type);
            return genericMethod.Invoke(this, new object[0]) as ISimNodeConstructionArgument;
        }
    }
}
