using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bdi.Actions;

namespace Bdi.Planning
{
    public class Plan
    {
        public List<SingleAction> ActionSequence { get; set; }

        public int CurrentStage { get; set; }

        public Plan()
        {
            ActionSequence = new List<SingleAction>();
            CurrentStage = 0;
        }
    }
}
