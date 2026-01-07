import pandas as pd
from sklearn.tree import DecisionTreeClassifier
from sklearn.linear_model import LogisticRegression
from sklearn.neighbors import KNeighborsClassifier
from sklearn.model_selection import train_test_split
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from pathlib import Path
import joblib as jb


app_root = Path(__file__).resolve().parent.parent.parent
python_root = Path(__file__).resolve().parent.parent

csv_filepath = Path.joinpath(app_root, "Data", "Processed", "ProcessedPokemonData.csv")
model_dirpath = Path.joinpath(python_root, "TrainedModels")


def TrainAndSaveModels():
    if not csv_filepath.exists():
        return 0
    
    if not model_dirpath.exists():
        model_dirpath.mkdir(parents=True, exist_ok=True)
    
    data = pd.read_csv(csv_filepath)
    X = data[["Hp", "Attack", "Defense", "SpecialAttack", "SpecialDefense", "Speed"]]
    y = data["PrimaryType"]

    Pipelines = {
        "Tree" : Pipeline([
            ("TreeScaler", StandardScaler()),
            ("TreeModel", DecisionTreeClassifier())
        ]),

        "Log" : Pipeline([
            ("LogScaler", StandardScaler()),
            ("LogModel", LogisticRegression())
        ]),

        "KNN" : Pipeline([
            ("KNNScaler", StandardScaler()),
            ("KNNModel", KNeighborsClassifier())
        ])
    }

    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.20, random_state=42)

    Pipelines["Tree"].fit(X_train, y_train)
    Pipelines["Log"].fit(X_train, y_train)
    Pipelines["KNN"].fit(X_train, y_train)

    jb.dump(Pipelines["Tree"], Path.joinpath(model_dirpath, "Tree.pkl"))
    jb.dump(Pipelines["Log"], Path.joinpath(model_dirpath, "Log.pkl"))
    jb.dump(Pipelines["KNN"], Path.joinpath(model_dirpath, "KNN.pkl"))

    test_data : pd.DataFrame = X_test.copy()
    test_data["PrimaryType"] = y_test.copy()
    test_data.to_csv(Path.joinpath(model_dirpath, "TestData.csv"), index=False)

    return 1

TrainAndSaveModels()


