import joblib as jb
from pathlib import Path
import pandas as pd
from sklearn.metrics import confusion_matrix, accuracy_score, precision_score, recall_score, f1_score

# for intellisense only, explicit type shii
from sklearn.tree import DecisionTreeClassifier
from sklearn.linear_model import LogisticRegression
from sklearn.neighbors import KNeighborsClassifier


modelPath = Path.joinpath(Path(__file__).resolve().parent.parent, "TrainedModels")

knnPath = Path.joinpath(modelPath, "KNN.pkl")
logPath = Path.joinpath(modelPath, "Log.pkl")
treePath = Path.joinpath(modelPath, "tree.pkl")

knnModel : KNeighborsClassifier = jb.load(knnPath)
logModel : LogisticRegression = jb.load(logPath)
treeModel : DecisionTreeClassifier = jb.load(treePath)

test_data = pd.read_csv(Path.joinpath(modelPath, "TestData.csv"))
X_test = test_data[["Hp", "Attack", "Defense", "SpecialAttack", "SpecialDefense", "Speed"]]
y_test = test_data["PrimaryType"]

def TestModels():
    knnPrediction = knnModel.predict(X_test)
    logPrediction = logModel.predict(X_test)
    treePrediction = treeModel.predict(X_test)

    result = X_test.copy()
    result["PrimaryType"] = y_test.copy()
    result["KnnPrediction"] = knnPrediction
    result["LogPrediction"] = logPrediction
    result["TreePrediction"] = treePrediction
    return result

def ModelMetricsMeasurement():
    result = TestModels()

    trueValue = result["PrimaryType"]
    knnPrediction = result["KnnPrediction"]
    logPrediction = result["LogPrediction"]
    treePrediction = result["TreePrediction"]

    knnMetricsEval = {
        "confusionMetrics" : confusion_matrix(trueValue, knnPrediction),
        "accuracyScore" : accuracy_score(trueValue, knnPrediction),
        "precisionScore" : precision_score(trueValue, knnPrediction, average="weighted", zero_division=0),
        "recallScore" : recall_score(trueValue, knnPrediction, average="weighted", zero_division=0),
        "f1Score" : f1_score(trueValue, knnPrediction, average="weighted")
    }

    logMetricsEval = {
        "confusionMetrics" : confusion_matrix(trueValue, logPrediction),
        "accuracyScore" : accuracy_score(trueValue, logPrediction),
        "precisionScore" : precision_score(trueValue, logPrediction, average="weighted", zero_division=0),
        "recallScore" : recall_score(trueValue, logPrediction, average="weighted", zero_division=0),
        "f1Score" : f1_score(trueValue, logPrediction, average="weighted")
    }

    treeMetricsEval = {
        "confusionMetrics" : confusion_matrix(trueValue, treePrediction),
        "accuracyScore" : accuracy_score(trueValue, treePrediction),
        "precisionScore" : precision_score(trueValue, treePrediction, average="weighted", zero_division=0),
        "recallScore" : recall_score(trueValue, treePrediction, average="weighted", zero_division=0),
        "f1Score" : f1_score(trueValue, treePrediction, average="weighted")
    }


    return [knnMetricsEval, logMetricsEval, treeMetricsEval]