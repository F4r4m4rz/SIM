using SIM.CodeEngine.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.CodeEngine.Factory
{
    public class SimDynamicFactory
    {
        public virtual T New<T>(params object[] contructionArguments) where T : DynamicObject, new()
        {
            return Activator.CreateInstance(typeof(T), contructionArguments) as T;
        }

        public virtual ParameterInfo[] GetConstructionArguments<T>() where T : DynamicObject, new()
        {
            return typeof(T).GetConstructors(BindingFlags.Public).FirstOrDefault()?.GetParameters();
        }

        public virtual ParameterInfo[] GetConstructionArguments(Type type)
        {
            return type.GetConstructors().FirstOrDefault()?.GetParameters();
        }
    }
}
