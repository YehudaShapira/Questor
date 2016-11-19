using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdi.Concepts;
using Bdi.Actions;
using Bdi.Concepts.Statements;
using Bdi.Characters;
using Bdi.Concepts.Queries;
using Bdi.Planning;

namespace Bdi.Objects
{
    public class Person : Noun
    {
        public BeliefSet Beliefs { get; set; }

        public List<StaticStatement> Goals { get; set; }

        public List<SingleAction> AvailableActions { get; set; }

        public List<string> FamiliarObjects { get; set; }

        public Plan Plan { get; set; }

        private Person()
            : base(string.Empty)
        {

        }

        public Person(string name, BeliefSet beliefs, List<StaticStatement> goals,
            List<SingleAction> availableActions, List<string> familiarObjects)
            : base(name)
        {
            Beliefs = beliefs;
            Goals = goals;
            AvailableActions = availableActions;
            FamiliarObjects = familiarObjects;
        }

        public Statement Answer(Question question)
        {
            if (question is AttributeQuestion)
            {
                var attributeQuestion = question as AttributeQuestion;
                var relevant = Beliefs.GetRelevantBeliefs(attributeQuestion.SubjectName);
                if (!relevant.Any())
                {
                    return Statement.Empty;
                }
                else
                {
                    var haveStatic = relevant
                        .Where(r => r.Statement is StaticStatement);
                    if (!haveStatic.Any())
                    {
                        return Statement.Empty;
                    }
                    else
                    {
                        var staticStatements = haveStatic
                            .Select(r => r.Statement)
                            .Cast<StaticStatement>();
                        var matching = staticStatements.FirstOrDefault(s =>
                            s.Attribute.Key.Equals(attributeQuestion.Attribute, StringComparison.InvariantCultureIgnoreCase));
                        if (matching == null)
                        {
                            return Statement.Empty;
                        }
                        else
                        {
                            return matching;
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void RecalculatePlan()
        {
            Plan = new Plan();

            CalculateReducingPlan();
            if (!Plan.ActionSequence.Any())
            {
                PlanInformationGathering();
            }
        }

        protected void CalculateReducingPlan()
        {
            var potentialActions = AvailableActions;
            foreach (var goal in Goals)
            {
                foreach (var action in AvailableActions)
                {
                    var result = Beliefs.WhatWouldHappenIf(action);
                    var ok = goal.IsConsistent(result);
                    if (!ok)
                    {
                        potentialActions.Remove(action);
                    }
                }
            }

            foreach (var goal in Goals)
            {
                var contradictingBeliefs = Beliefs.GetContradictingBeliefs(goal);
                var problems = contradictingBeliefs.Select(b => b.Statement);
                foreach (var problem in problems)
                {
                    if (problem is StaticStatement)
                    {
                        var staticProblem = problem as StaticStatement;

                        foreach (var action in potentialActions)
                        {
                            var result = Beliefs.WhatWouldHappenIf(action);

                            if (!result.IsConsistent(problem))
                            {
                                Plan.ActionSequence.Add(action);
                            }
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        protected void PlanInformationGathering()
        {
            throw new NotImplementedException();
        }
    }
}
