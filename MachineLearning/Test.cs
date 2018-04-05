using MachineLearning.DecisionTree;
using MachineLearning.Enums;
using MachineLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Test
    {

        // A random observation generator
        public static List<Observation> ObservationGenerator(int numObs, List<IFeature> Feats, Dictionary<IFeature, List<IValue>> featValueDict, List<Label> labels)
        {
            // Number of values for each attribute/feature
            var valueCountDict = featValueDict.ToDictionary(fv => fv.Key, fv => fv.Value.Count);

            // Blank liset of observations
            var obsList = new List<Observation>();

            // System must be specified for inclusion with Unity
            var rnd = new System.Random();

            // Generating numbObs new observations
            for (int i =0; i< numObs; i++)
            {
                // Select n random numbers for n = Feats.Count;
                var obsDict = Feats.ToDictionary(feat => feat, feat => featValueDict[feat][rnd.Next(valueCountDict[feat])] as IValue);

                // Pick random label
                var label = labels[rnd.Next(0, labels.Count)];

                // Create and add new observation
                obsList.Add(new Observation(obsDict as Dictionary<IFeature, IValue>, label));
            }

            // return observation List
            return obsList;
        }

        public static void HandCodedDT()
        {
            var featValueDict = new Dictionary<IFeature, List<IValue>>();
            var feats = new List<IFeature>() { new Feature() as IFeature, new Feature() as IFeature, new Feature() as IFeature, new Feature() as IFeature };
            featValueDict[feats[0]] = new List<IValue>() { new FeatValue(0), new FeatValue(1) };
            featValueDict[feats[1]] = new List<IValue>() { new FeatValue(3), new FeatValue(18), new FeatValue(20) };
            featValueDict[feats[2]] = new List<IValue>() { new FeatValue(0), new FeatValue(1) };
            featValueDict[feats[3]] = new List<IValue>() { new FeatValue(0), new FeatValue(1), new FeatValue(2), new FeatValue(3) };

            var obsList = new List<Observation>();

            var obs1 = new Dictionary<IFeature, IValue>()
            {
                { feats[0] as IFeature, featValueDict[feats[0]][0] },
                { feats[1] as IFeature, featValueDict[feats[1]][0] },
                { feats[2] as IFeature, featValueDict[feats[2]][0] },
                { feats[3] as IFeature, featValueDict[feats[3]][3] }
            };
            obsList.Add(new Observation(obs1, Enums.Label.up));

            var obs2 = new Dictionary<IFeature, IValue>()
            {
                { feats[0] as IFeature, featValueDict[feats[0]][1] },
                { feats[1] as IFeature, featValueDict[feats[1]][0] },
                { feats[2] as IFeature, featValueDict[feats[2]][1] },
                { feats[3] as IFeature, featValueDict[feats[3]][3] }
            };
            obsList.Add(new Observation(obs2, Enums.Label.down));

            var obs3 = new Dictionary<IFeature, IValue>()
            {
                { feats[0] as IFeature, featValueDict[feats[0]][0] },
                { feats[1] as IFeature, featValueDict[feats[1]][2] },
                { feats[2] as IFeature, featValueDict[feats[2]][1] },
                { feats[3] as IFeature, featValueDict[feats[3]][3] }
            };
            obsList.Add(new Observation(obs3, Enums.Label.left));

            var obs4 = new Dictionary<IFeature, IValue>()
            {
                { feats[0] as IFeature, featValueDict[feats[0]][0] },
                { feats[1] as IFeature, featValueDict[feats[1]][2] },
                { feats[2] as IFeature, featValueDict[feats[2]][0] },
                { feats[3] as IFeature, featValueDict[feats[3]][3] }
            };
            obsList.Add(new Observation(obs4, Enums.Label.right));

            var root = ID3.Learn(obsList, featValueDict, new List<Enums.Label>() { Enums.Label.up, Enums.Label.down, Enums.Label.left, Enums.Label.right }, 3);

            var DT = new DTree(root);


            var testObservationFeatures = new Dictionary<IFeature, IValue>()
            {
                { feats[0] as IFeature, featValueDict[feats[0]][0] },
                { feats[1] as IFeature, featValueDict[feats[1]][2] },
                { feats[2] as IFeature, featValueDict[feats[2]][0] },
                { feats[3] as IFeature, featValueDict[feats[3]][2] }
            };
            var testObs = new Observation(testObservationFeatures, Enums.Label.right);

            var prediction = DT.UseTree(testObs);
            Console.WriteLine(prediction);
            Console.ReadLine();
        }

        public static void TestDecisionTree()
        {
            var featValueDict = new Dictionary<IFeature, List<IValue>>();
            var feats = new List<IFeature>() { new Feature() as IFeature, new Feature() as IFeature, new Feature() as IFeature, new Feature() as IFeature };
            featValueDict[feats[0]] = new List<IValue>() { new FeatValue(0), new FeatValue(1) };
            featValueDict[feats[1]] = new List<IValue>() { new FeatValue(3), new FeatValue(18), new FeatValue(20) };
            featValueDict[feats[2]] = new List<IValue>() { new FeatValue(0), new FeatValue(1) };
            featValueDict[feats[3]] = new List<IValue>() { new FeatValue(0), new FeatValue(1), new FeatValue(2), new FeatValue(3) };

            var labelList = new List<Enums.Label>() { Enums.Label.up, Enums.Label.down, Enums.Label.left, Enums.Label.right };
            var obsList = ObservationGenerator(200, feats, featValueDict, labelList);


            var root = ID3.Learn(obsList, featValueDict, labelList, 3);
            var DT = new DTree(root);

            var testObsList = ObservationGenerator(200, feats, featValueDict, labelList);

            // how many correct?
            var sumCorrect = 0;

            // Precision: given guess of this label, how many correct?
            var precisionDict = new Dictionary<Label, int>();

            // Recall: given actual label, how many correct?
            var recallDict = new Dictionary<Label, int>();

            foreach (var testobs in testObsList)
            {
                // Do total, precision, and recall here.
            }
          
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            TestDecisionTree();
        }
    }
}
