from fastapi import FastAPI
import joblib as jb
from pathlib import Path

from Models.Response import Response
from Services.Test import ModelMetricsMeasurement



app = FastAPI(title="ModelApiService")


@app.get("/check-connection")
def CheckConnection():
    return {"status" : "ok"}

@app.get("/predict/pokemon-type")
def PredictPokemenType():
    return ModelMetricsMeasurement()
    




