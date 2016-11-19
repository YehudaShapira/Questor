using Bdi.Actions;
using Bdi.Concepts.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdi.Concepts
{
    public class BeliefSet
    {
        public List<Belief> Beliefs { get; set; }

        public BeliefSet()
        {
            Beliefs = new List<Belief>();
        }

        public IList<Belief> GetRelevantBeliefs(string subjectName)
        {
            var relevant = from b in Beliefs
                           where b.Statement is StaticStatement &&
                                (b.Statement as StaticStatement).SubjectName == subjectName
                           select b;

            return relevant.ToList();
        }

        public IList<Belief> GetContradictingBeliefs(Statement statement)
        {
            var relevant = from b in Beliefs
                           where !b.Statement.IsConsistent(statement)
                           select b;

            return relevant.ToList();
        }

        public Statement WhatWouldHappenIf(SingleAction action)
        {
            var dynamic = GetBelievedDynamicStatements();

            var relevant = dynamic.Where(d => d.Action.Name == action.Name);
            if (relevant.Any())
            {
                return  relevant.First().Result;
            }

            return Statement.Empty;
        }

        public IList<DynamicStatement> GetBelievedDynamicStatements()
        {
            var res = new List<DynamicStatement>();

            var relevant = from b in Beliefs
                           where b.Statement is DynamicStatement
                           select b;
            if (relevant.Any())
            {
                var dynamic = relevant.Select(r => r.Statement)
                    .Cast<DynamicStatement>();

                res.AddRange(dynamic);
            }

            return res;
        }

        public void Add(Belief belief)
        {
            Beliefs.Add(belief);
        }
    }
}
