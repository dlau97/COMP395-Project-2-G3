using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorController : CircuitComponent
{
    public int resistance = 100;

    public ResistorController(int r){
        resistance = r;
        nextComponent = null;
    }

    public ResistorController(int r, GameObject nextObj){
        resistance = r;
        nextComponent = nextObj;
    }

    public int getResistance(){
        return resistance;
    }

    public void setResistance(int r){
        resistance = r;
    }
}
