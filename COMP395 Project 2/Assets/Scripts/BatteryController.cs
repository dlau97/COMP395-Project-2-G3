using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : CircuitComponent
{

    public float voltage = 9f;

    public BatteryController(float v){
        voltage = v;
        nextComponent = null;
    }

    public BatteryController(float v, GameObject nextObj){
        voltage = v;
        nextComponent = nextObj;
    }

    public float getVoltage(){
        return voltage;
    }

    public void setVoltage(float v){
        voltage = v;
    }


}
