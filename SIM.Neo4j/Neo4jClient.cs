using Neo4j.Driver;
using SIM.Core.Extensions;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using INode = SIM.Core.Objects.INode;
using Neo4jNode = Neo4j.Driver.INode;

namespace SIM.Neo4j
{
    public class Neo4jClient
    {
        private IDriver _driver;

        public Neo4jClient(string uri, string username = null, string password = null)
        {
            uri.ValidateNullOrWhitespace(nameof(uri));
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
        }

        public void Create(ISimObject simObject)
        {
            string cypher = CypherBuilder.Build(simObject, CypherCommandType.Create);
            CypherExecuter(cypher);
        }

        public object GetAll<T>() where T : class, INode, new()
        {
            string cypher = $"MATCH (a:{typeof(T).Name}) RETURN a";
            var result = CypherGetExecuter(cypher);
            result.Wait();
            return result.Result;
        }

        public async Task<IEnumerable<Neo4jNode>> GetAll(string type)
        {
            string cypher = $"MATCH (a:{type}) RETURN a";
            var result = await CypherGetExecuter(cypher);
            return result;
        }

        private void CypherExecuter(string cypher)
        {
            var session = _driver.AsyncSession();
            session.WriteTransactionAsync(a =>
            {
                return a.RunAsync(cypher);
            }).Wait();
        }

        private async Task<IEnumerable<Neo4jNode>> CypherGetExecuter(string cypher)
        {
            var session = _driver.AsyncSession();
            var result = await session.ReadTransactionAsync(async a =>
            {
                var res = await a.RunAsync(cypher);
                return (await res.ToListAsync());
            });
            var x = new List<Neo4jNode>();
            result.ForEach(a => x.AddRange(a.Values.Values.Select(b=>b as Neo4jNode)));
            return x;
        }
    }
}
