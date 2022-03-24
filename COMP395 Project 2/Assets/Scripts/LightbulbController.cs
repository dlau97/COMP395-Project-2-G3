using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbController : CircuitComponent
{
    public bool isPowered = false;
    public float brightness = 1; //1 = Max brightness, 0 = min brightness

    public LightbulbController(float b = 1f){
        brightness = b;
        nextComponent = null;
    }

    public LightbulbController(float b = 1f, GameObject nextObj = null){
        brightness = b;
        nextComponent = nextObj;
    }

    public void setPowered(bool p){
        isPowered = p;
    }

    public void togglePowered(){
        isPowered = !isPowered;
    }

    public void setBrightness(float val){
        brightness = val;
    }
}
