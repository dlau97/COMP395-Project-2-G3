using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawing : MonoBehaviour
{
    private GameManager manager;
    private bool isDrawing = false;
    public GameObject line;
    private GameObject currentLine;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manager.isDrawing = true;
            isDrawing = true;
            currentLine = Instantiate(line);
        }
        if (isDrawing)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3[] vArray = { transform.position,  Camera.main.ScreenToWorldPoint(mousePos)};
            line.GetComponent<LineRenderer>().SetPositions(vArray);
        }
        if (Input.GetMouseButtonUp(0))
        {
            manager.isDrawing = false;
            isDrawing = false;
            Destroy(currentLine);
        }
    }
}