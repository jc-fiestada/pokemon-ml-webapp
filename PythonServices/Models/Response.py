from pydantic import BaseModel

class Response(BaseModel):
    hp : str
    attack : int
    defense : int
    specialAttack : int
    specialDefense : int
    speed : int
    primaryType : str
    

    