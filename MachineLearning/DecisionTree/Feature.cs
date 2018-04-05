using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.DecisionTree
{
    public class Feature : IFeature
    {
        private static int Counter = -1;
        public int id;

        public Feature()
        {
            id = System.Threading.Interlocked.Increment(ref Counter);
        }

        public IValue GetValue(Observation obs)
        {
            return obs.FeatDict[this];
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
