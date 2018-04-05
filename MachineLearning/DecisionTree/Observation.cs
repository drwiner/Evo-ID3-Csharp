using MachineLearning.Enums;
using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.DecisionTree
{

    public class Observation
    {
        private static int Counter = -1;
        private int id;
        private Label label;
        private Dictionary<IFeature, IValue> featDict;

        public Dictionary<IFeature, IValue> FeatDict
        {
            get { return featDict; }
            set { featDict = value; }
        }

        public int ID{
            get {return id;}
            set { id = value;}
        }

        public Label Label
        {
            get { return label ; }
            set { label = value; }
        }

        // Returns a hashcode.
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + ID.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return id.ToString() + this.Label.ToString();
        }


        public Observation()
        {
            id = System.Threading.Interlocked.Increment(ref Counter);
        }

        public Observation(Dictionary<IFeature, IValue> FL)
        {
            id = System.Threading.Interlocked.Increment(ref Counter);
            featDict = FL;
        }

        public Observation(Dictionary<IFeature, IValue> FL, Label label)
        {
            id = System.Threading.Interlocked.Increment(ref Counter);
            featDict = FL;
            this.label = label;
        }

        public Observation(int id, Dictionary<IFeature, IValue> featDict, Label label)
        {
            this.id = id;
            this.featDict = featDict;
            this.label = label;
        }
    }
}
