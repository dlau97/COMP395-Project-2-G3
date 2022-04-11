using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitController : MonoBehaviour
{
    public float value = 0;
    public Text text;
    public BatteryController battComp;
    public ResistorController resComp;
    public LightbulbController liComp;
    public CircuitComponent cirComp;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Battery"))
        {
            battComp = new BatteryController(value);
            text.text = value.ToString();
        } else if (gameObject.CompareTag("Resistor"))
        {
            resComp = new ResistorController(value);
            text.text = value.ToString();
        } else if (gameObject.CompareTag("Bulb"))
        {
            liComp = new LightbulbController(value);
        }
        else
        {
            cirComp = new CircuitComponent();
            text.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Bulb"))
        {
            gameObject.GetComponentInChildren<Light>().intensity = liComp.brightness;
            text.text = liComp.brightness.ToString("F2");
        }
    }

    public void SetNext(GameObject next)
    {
        if (gameObject.CompareTag("Battery"))
        {
            battComp.setNextComponent(next);
        }
        else if (gameObject.CompareTag("Resistor"))
        {
            resComp.setNextComponent(next);
        }
        else if (gameObject.CompareTag("Bulb"))
        {
            liComp.setNextComponent(next);
        }
        else
        {
            cirComp.setNextComponent(next);
        }
    }

    public GameObject GetNext()
    {
        if (gameObject.CompareTag("Battery"))
        {
            return battComp.getNextComponent();
        }
        else if (gameObject.CompareTag("Resistor"))
        {
            return resComp.getNextComponent();
        }
        else if (gameObject.CompareTag("Bulb"))
        {
            return liComp.getNextComponent();
        }
        else
        {
            return cirComp.getNextComponent();
        }
    }
}
