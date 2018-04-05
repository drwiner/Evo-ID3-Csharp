using MachineLearning.Enums;
using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.DecisionTree
{

    public static class ID3
    {
        public static Dictionary<IFeature, IValue> FeatValueDict;

        public static double PlogP(List<Observation> obsList, Label label, int numLabels)
        {
            //var p = obsList.Sum(obs)
            var p = obsList.Count(obs => obs.Label == label) / obsList.Count;
            if (p == 0)
            {
                return 0;
            }
            return p * Math.Log(p, numLabels);
        }

        public static double Entropy(List<Observation> obsList, List<Label> labels)
        {
            return -labels.Sum(label => PlogP(obsList, label, labels.Count));
        }

        public static double Gain(List<Observation> obsList, IFeature feature, List<IValue> possibleValues, List<Label> labels, double totalEntropy)
        {
            var gain = 0.0;
            foreach (var value in possibleValues)
            {
                var split = obsList.Where(obs => feature.GetValue(obs) == value);
                gain += Entropy(obsList, labels) * split.Count() / obsList.Count;
            }
            return totalEntropy - gain;
        }

        public static IFeature PickBestAttribute(List<Observation> obsList, Dictionary<IFeature, List<IValue>> featValueDict, List<Label> labels)
        {
            var totalEntropy = Entropy(obsList, labels);
            // - infinity
            var bestScore = -1 / 0d;
            var bestFeat = new Feature() as IFeature;

            foreach (var keyvalue in featValueDict)
            {
                var g = Gain(obsList, keyvalue.Key, keyvalue.Value, labels, totalEntropy);
                if (g > bestScore)
                {
                    bestScore = g;
                    bestFeat = keyvalue.Key;
                }
            }
            return bestFeat;
        }

        public static bool AllSameLabel(List<Observation> obsList)
        {
            var firstObserved = obsList[0].Label;
            return !obsList.Any(obs => obs.Label != firstObserved);
        }

        public static Label MostCommonLabel(List<Observation> obsList)
        {
            var labelDict = new Dictionary<Label, int>();

            var bestScore = -1 / 0d;
            var bestLabel = Label.none;

            foreach (var obs in obsList)
            {
                if (!labelDict.ContainsKey(obs.Label))
                {
                    labelDict[obs.Label] = 0;
                }
                labelDict[obs.Label]++;
                if (labelDict[obs.Label] > bestScore)
                {
                    bestLabel = obs.Label;
                }
            }

            return bestLabel;
        }


        public static Node Learn(List<Observation> obsList, Dictionary<IFeature, List<IValue>> featValueDict, List<Label> labels, int depth)
        {
            if (depth == 0 || featValueDict.Keys.Count == 0)
            {
                return new LeafNode(MostCommonLabel(obsList));
            }
            if (AllSameLabel(obsList))
            {
                return new LeafNode(obsList[0].Label);
            }

            var feat = PickBestAttribute(obsList, featValueDict, labels);
            var thisNode = new Node(feat);
            foreach (var value in featValueDict[feat])
            {
                var split = obsList.Where(obs => featValueDict[feat] == value);
                if (split.Count() == 0)
                {
                    thisNode.ChildOnValue[value] = new LeafNode(MostCommonLabel(obsList));
                    continue;
                }
                Dictionary<IFeature, List<IValue>> fvDictClone = featValueDict;
                var child = Learn(split.ToList(), fvDictClone, labels, depth - 1);
                thisNode.ChildOnValue[value] = child;
            }

            return thisNode;
        }

    }


}
