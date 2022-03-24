using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isDrawing = false;
    public GameObject currentPole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //CircuitComplete(GameObject.FindGameObjectWithTag("Battery"));
        }
    }

    bool CircuitComplete(GameObject batteryPos)
    {
        GameObject currObject = batteryPos.GetComponent<LineDrawing>().connected;
        while (currObject != batteryPos) {
            if (currObject == null) return false;
            currObject = currObject.GetComponent<LineDrawing>().connected;
        }
        return true;
    }
}
