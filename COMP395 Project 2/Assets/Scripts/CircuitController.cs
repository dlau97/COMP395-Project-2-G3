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
        } else if (gameObject.CompareTag("Bulb"))
        {
            component = new LightbulbController(1);
        }
        else
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
        if (gameObject.CompareTag("Bulb"))
        {
            gameObject.GetComponentInChildren<Light>().intensity = component.brightness;
        }
    }

    public void SetNext(GameObject next)
    {
        component.setNextComponent(next);
    }

    public GameObject GetNext()
    {
        return component.getNextComponent();
    }

    public bool CheckCircuit()
    {
        GameObject currObj = component.getNextComponent();
        int resistance = 0;
        int voltage = component.getVoltage();
        List<GameObject> bulbs = new List<GameObject>();
        while (gameObject != currObj)
        {
            if (currObj == null) return false;
            if (currObj.CompareTag("Battery"))
            {
                voltage += currObj.GetComponent<CircuitController>().component.getVoltage();
            }
            if (currObj.CompareTag("Resistor"))
            {
                resistance += currObj.GetComponent<CircuitController>().component.getResistance();
            }
            if (currObj.CompareTag("Bulb"))
            {
                bulbs.Add(currObj);
            }
            currObj = currObj.GetComponent<CircuitController>().GetNext();
        }
        foreach (GameObject bulb in bulbs)
        {
            bulb.GetComponent<CircuitController>().component.setBrightness(Mathf.Pow(voltage / resistance, 2) * resistance);
        }
        return true;
    }
}
