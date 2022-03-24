using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbController : CircuitComponent
{
    public bool isPowered = false;
    public float brightness = 1; //1 = Max brightness, 0 = min brightness
    public float watts = 40f;

    public LightbulbController(float w = 40f){
        watts = w;
        nextComponent = null;
    }

    public LightbulbController(float w = 40f, GameObject nextObj = null)
    {
        watts = w;
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

    public float getWatts()
    {
        return watts;
    }

    public float getResistance(float voltage)
    {
        return Mathf.Pow(voltage, 2) / watts;
    }
}
