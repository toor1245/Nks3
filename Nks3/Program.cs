using Nks3;

SchemaLab3 lab3 =
    new(Constants.Schema, Constants.Probabilities, Constants.InputIndexes, Constants.OutputIndexes, Constants.Hours);

lab3.EvaluateLoadedGeneral(Constants.Multiplicities[0]);
lab3.ShowResult();
lab3.EvaluateNotLoadedGeneral(Constants.Multiplicities[1]);
lab3.ShowResult();
lab3.EvaluateLoadedSeparate(Constants.Multiplicities[0]);
lab3.ShowResult();
lab3.EvaluateNotLoadedSeparate(Constants.Multiplicities[1]);
lab3.ShowResult();