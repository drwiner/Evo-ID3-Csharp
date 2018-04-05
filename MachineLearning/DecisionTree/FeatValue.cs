using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.DecisionTree
{
    public class FeatValue : IValue
    {
        public int Value;
        public FeatValue (int _value){
            Value = _value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
