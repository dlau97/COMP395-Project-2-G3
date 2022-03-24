using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorController : CircuitComponent
{
    public float resistance = 100;

    public ResistorController(float r){
        resistance = r;
        nextComponent = null;
    }

    public ResistorController(float r, GameObject nextObj){
        resistance = r;
        nextComponent = nextObj;
    }

    public float getResistance(){
        return resistance;
    }

    public void setResistance(float r){
        resistance = r;
    }
}
