using MachineLearning.Enums;
using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.DecisionTree
{
    public class DTree : IFeature
    {
        public Node Root;

        public DTree()
        {
            Root = new Node(new Feature());
        }

        public DTree(Node root)
        {
            Root = root;
        }

        public IValue GetValue(Observation obs)
        {
            var leafnode = UseTree(obs, Root);
            return leafnode as IValue;
        }

        public Label UseTree(Observation obs)
        {
            var leafnode = UseTree(obs, Root);
            return leafnode.Label;
        }

        private LeafNode UseTree(Observation obs, Node currentRoot)
        {
            // Base Case
            if (currentRoot.IsLeaf)
            {
                var AsLeaf = currentRoot as LeafNode;
                return AsLeaf;
            }
            
            // Recursion -> find child node
            var feat = currentRoot.feature;
            var value = obs.FeatDict[feat];
            var child = currentRoot.ChildOnValue[value];
            return UseTree(obs, child);
        }
    }


    public class Node
    {
        private static int Counter = -1;
        public int id;
        public IFeature feature;
        public Dictionary<IValue, Node> ChildOnValue;
        public bool IsLeaf = false;
        public Node(IFeature _feature)
        {
            feature = _feature;
            id = System.Threading.Interlocked.Increment(ref Counter);
            ChildOnValue = new Dictionary<IValue, Node>();
        }



    }

    public class LeafNode : Node, IValue
    {
        public Label Label;
        public LeafNode(Label label) : base(new Feature()) {
            Label = label;
            IsLeaf = true;
        }
    }

}
