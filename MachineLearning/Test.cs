using MachineLearning.DecisionTree;
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
        static void Main(string[] args)
        {
            var featValueDict = new Dictionary<IFeature, List<IValue>>();
            var feats = new List<Feature>() { new Feature(), new Feature(), new Feature(), new Feature() };
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

            var prediction = DT.UseTree(testObs, root);
            Console.WriteLine(prediction);
        }
    }
}
