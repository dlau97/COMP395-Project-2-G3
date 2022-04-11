using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawing : MonoBehaviour
{
    private GameManager manager;
    private bool isDrawing = false;
    public GameObject line;
    private GameObject currentLine;
    public GameObject connected;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.CompareTag("Positive") && transform.parent.gameObject.GetComponent<CircuitController>().GetNext() == null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + 10f));
            if (1 > Vector2.Distance(transform.position, mousePos))
            {
                manager.isDrawing = true;
                manager.currentPole = gameObject;
                isDrawing = true;
                currentLine = Instantiate(line);
                Vector3[] vArray = { transform.position, Camera.main.ScreenToWorldPoint(mousePos) };
                currentLine.GetComponent<LineRenderer>().SetPositions(vArray);
            }
        }
        if (isDrawing)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            currentLine.GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(mousePos));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (gameObject.CompareTag("Negative"))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + 10f));
                if (1 > Vector2.Distance(transform.position, mousePos) && manager.currentPole.transform.parent.gameObject != transform.parent.gameObject)
                {
                    manager.currentPole.transform.parent.gameObject.GetComponent<CircuitController>().SetNext(gameObject.transform.parent.gameObject);
                    manager.currentPole.GetComponent<LineDrawing>().CleanUp(gameObject);
                }
                if (gameObject.transform.parent.gameObject.CompareTag("Battery"))
                {
                    manager.CheckCircuit(gameObject.transform.parent.gameObject);
                }
            } else
            {
                manager.isDrawing = false;
                isDrawing = false;
                Destroy(currentLine);
            }
        }
    }

    public void CleanUp(GameObject next)
    {
        GameObject tempLine = Instantiate(line);
        tempLine.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        tempLine.GetComponent<LineRenderer>().SetPosition(1, next.transform.position);
        manager.currentPole = null;
    }
}
