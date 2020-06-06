using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j
{
    public enum CypherCommandType { Create, Update, Delete, Get }

    public static class CypherBuilder
    {
        public static string Build(ISimObject simObject, CypherCommandType commandType)
        {
            switch (commandType)
            {
                case CypherCommandType.Create:
                    return CreateCommand(simObject);
                case CypherCommandType.Update:
                    return UpdateCommand(simObject);
                case CypherCommandType.Delete:
                    return DeleteCommand(simObject);
                default:
                    return null;
            }
        }

        private static string DeleteCommand(ISimObject simObject)
        {
            throw new NotImplementedException();
        }

        private static string UpdateCommand(ISimObject simObject)
        {
            throw new NotImplementedException();
        }

        private static string CreateCommand(ISimObject simObject)
        {
            IEnumerable<IRelation> inwardRelations = GetInwardRelations(simObject);
        }

        private static IEnumerable<IRelation> GetInwardRelations(ISimObject simObject)
        {
            
        }
    }
}
