using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : CircuitComponent
{
    public bool isPowered = false;
    float volume = 0.5f; //1 = max volume, 0 = off

    public SpeakerController(float vol = 0.5f){
        volume = vol;
        nextComponent = null;
    }

    public SpeakerController(float vol = 0.5f, GameObject nextObj = null){
        volume = vol;
        nextComponent = nextObj;
    }

    public void setPowered(bool p){
        isPowered = p;
    }

    public void togglePowered(){
        isPowered = !isPowered;
    }

    public void setVolume(float v){
        volume = v;
    }

}
