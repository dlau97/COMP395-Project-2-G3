using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitController : MonoBehaviour
{
    public dynamic component;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Battery"))
        {
            component = new BatteryController(9);
        } else if (gameObject.CompareTag("Resistor"))
        {
            component = new ResistorController(10);
        } else
        {
            component = new CircuitComponent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(component.getVoltage());
        if (gameObject.CompareTag("Battery") && Input.GetMouseButtonUp(0))
        {
            CheckCircuit();
        }
    }

    public void SetNext(GameObject next)
    {
        component.setNextComponent(next);
    }

    public void CheckCircuit()
    {
        //while ()
    }
}
