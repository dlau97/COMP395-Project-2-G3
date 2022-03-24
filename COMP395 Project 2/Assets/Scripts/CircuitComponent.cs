using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitComponent
{

    public GameObject nextComponent = null;

    public CircuitComponent(){

    }


    public void setNextComponent(GameObject obj){
        nextComponent = obj;
    }

    public void removeNextComponent(){
        nextComponent = null;
    }

    public GameObject getNextComponent(){
        return nextComponent;
    }




}
