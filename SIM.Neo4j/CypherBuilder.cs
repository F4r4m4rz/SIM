using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j
{
    public enum CypherCommandType { Create, Update, Delete, Get }

    public static class CypherBuilder
    {
        private static ObjectIDGenerator IDGenerator = new ObjectIDGenerator();
        private static Dictionary<ISimObject, string> TagHistory = new Dictionary<ISimObject, string>();
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
            StringBuilder sb = new StringBuilder();
            sb.Append("create ");

            Graph graph = simObject as Graph;

            // Go through relations
            for (int i = 0; i < graph.Relations.Count; i++)
            {
                string s = AnalyseRelation(graph.Relations.ElementAt(i));
                sb.Append(s);
                sb.Append(",");
            }

            // Go through nodes
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                string s = AnalyseNodeProperties(graph.Nodes.ElementAt(i));
                sb.Append(s);
                sb.Append(",");
            }
            
            return sb.ToString().Trim(',');
        }

        private static string AnalyseNodeProperties(Node node)
        {
            Type nodeType = node.GetType();
            var properties = nodeType.GetProperties();
            StringBuilder cmd = new StringBuilder();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(node);
                if (value == null) continue;
                string s = AnalyseRelation(value as PropertyRelation, prop.Name);
                cmd.Append(s);
                cmd.Append(",");
            }

            return cmd.ToString().Trim(',');
        }

        private static string AnalyseRelation(PropertyRelation relation, string name)
        {
            var id = IDGenerator.GetId(relation, out bool isFirstTime);
            string tag = string.Empty;
            if (TagHistory.ContainsKey(relation))
                tag = TagHistory[relation];
            else
            {
                tag = $"r{id}";
                TagHistory.Add(relation, tag);
            }
            string relationCmd = $"[{tag}:{relation.GetType().Name}]";

            // Origin
            string origin = AnalyseNode(relation.Origin);

            // Target
            string target = AnalyseNode(relation.Target, name);

            return string.Format("{0}-{1}->{2}", origin, relationCmd, target);
        }

        private static string AnalyseNode(PropertyNode node, string name)
        {
            var id = IDGenerator.GetId(node, out bool isFirstTime);
            Type nodeType = node.GetType();
            string tag = string.Empty;
            string nodeCmd = string.Empty;
            if (TagHistory.ContainsKey(node))
            {
                tag = TagHistory[node];
                nodeCmd = string.Format("({0})", tag);
            }
            else
            {
                tag = $"n{id}";
                TagHistory.Add(node, tag);
                nodeCmd = string.Format("({0}:{1}{{Value:'{2}'}})", tag, name, node.GetValue());
            }

            return nodeCmd;
        }

        private static string AnalyseRelation(Relation relation)
        {
            var id = IDGenerator.GetId(relation, out bool isFirstTime);
            string tag = string.Empty;
            if (TagHistory.ContainsKey(relation))
                tag = TagHistory[relation];
            else
            {
                tag = $"r{id}";
                TagHistory.Add(relation, tag);
            }
            string relationCmd = $"[{tag}:{relation.GetType().Name}]";

            // Origin
            string origin = AnalyseNode(relation.Origin);

            // Target
            string target = AnalyseNode(relation.Target);

            return string.Format("{0}-{1}->{2}", origin, relationCmd, target);
        }

        private static string AnalyseNode(Node node)
        {
            var id = IDGenerator.GetId(node, out bool isFirstTime);
            Type nodeType = node.GetType();
            string tag = string.Empty;
            string nodeCmd = string.Empty;
            if (TagHistory.ContainsKey(node))
            {
                tag = TagHistory[node];
                nodeCmd = string.Format("({0})", tag);
            }
            else
            {
                tag = $"n{id}";
                TagHistory.Add(node, tag);
                nodeCmd = string.Format("({0}:{1})", tag, nodeType.Name);
            }

            return nodeCmd;
        }
    }
}
