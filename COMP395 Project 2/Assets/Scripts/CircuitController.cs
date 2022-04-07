using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitController : MonoBehaviour
{
    public dynamic component;
    public float value = 0;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        if (gameObject.CompareTag("Battery"))
        {
            component = new BatteryController(value);
        } else if (gameObject.CompareTag("Resistor"))
        {
            component = new ResistorController(value);
        } else if (gameObject.CompareTag("Bulb"))
        {
            component = new LightbulbController(value);
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
            manager.CheckCircuit(gameObject);
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
        float resistance = 0;
        float resistanceWBulbs = 0;
        float voltage = component.getVoltage();
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
        resistanceWBulbs = resistance;
        foreach (GameObject bulb in bulbs)
        {
            resistanceWBulbs += bulb.GetComponent<CircuitController>().component.getResistance(voltage);
        }
        foreach (GameObject bulb in bulbs)
        {
            bulb.GetComponent<CircuitController>().component.setBrightness(Mathf.Pow(voltage / resistanceWBulbs, 2) * (resistance + bulb.GetComponent<CircuitController>().component.getResistance(voltage)));
        }
        return true;
    }
}
