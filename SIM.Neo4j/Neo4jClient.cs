using Neo4j.Driver;
using SIM.Core.Extensions;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        private void CypherExecuter(string cypher)
        {
            var session = _driver.AsyncSession();
            session.WriteTransactionAsync(a =>
            {
                return a.RunAsync(cypher);
            }).Wait();
        }
    }
}
