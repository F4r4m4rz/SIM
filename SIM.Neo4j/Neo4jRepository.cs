﻿using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Neo4j
{
    public class Neo4jRepository
    {
        private readonly Neo4jClient _client;

        public Neo4jRepository()
        {
            _client = new Neo4jClient("bolt://localhost:7687", "neo4j", "1234");
        }

        public void Add(ISimObject simObject)
        {
            _client.Create(simObject);
        }

        public ISimObject Get(Func<ISimObject, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISimObject> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ISimObject>> GetAll(string type)
        {
            var x = new List<ISimObject>();
            (await _client.GetAll(type)).ToList().ForEach(a=>x.Add(Convertor.Convert(a)));
            return x;
        }

        public void Remove(ISimObject simObject)
        {
            throw new NotImplementedException();
        }

        public void Update(ISimObject simObject)
        {
            throw new NotImplementedException();
        }
    }
}
