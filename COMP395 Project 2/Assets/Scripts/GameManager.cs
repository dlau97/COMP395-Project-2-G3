using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isDrawing = false;
    public GameObject currentPole;
    public enum LevelType { GreatLightIntensity, LowLightIntensity, SpeakerVolume , Resistance, CompleteCircuit};
    public LevelType[] levelTypeList;

    public GameObject inventory;
    public GameObject textPrefab;

    public float[] goal;
    private GameObject[] text;
    public GameObject button;
    public GameObject reset;
    // Start is called before the first frame update
    void Start()
    {
        text = new GameObject[levelTypeList.Length];
        for (int i = 0; i < levelTypeList.Length; i++)
        {
            if (levelTypeList[i] == LevelType.GreatLightIntensity)
            {
                text[i] = Instantiate(textPrefab, inventory.transform);
                text[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(250 - i * 50, -6, 0);
                text[i].GetComponent<TMP_Text>().text = "• >= " + goal[i] + " Light Value";
                text[i].GetComponent<TMP_Text>().color = new Color(1.0f, 0f, 0f);
            }
            if (levelTypeList[i] == LevelType.LowLightIntensity)
            {
                text[i] = Instantiate(textPrefab, inventory.transform);
                text[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(250 - i * 50, -6, 0);
                text[i].GetComponent<TMP_Text>().text = "• < " + goal[i] + " Light Value";
                text[i].GetComponent<TMP_Text>().color = new Color(1.0f, 0f, 0f);
            }
            else if (levelTypeList[i] == LevelType.Resistance)
            {
                text[i] = Instantiate(textPrefab, inventory.transform);
                text[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(250 - i * 50, -6, 0);
                text[i].GetComponent<TMP_Text>().text = "• " + goal[i] + " Resistance Value";
                text[i].GetComponent<TMP_Text>().color = new Color(1.0f, 0f, 0f);
            }
            else if (levelTypeList[i] == LevelType.CompleteCircuit)
            {
                text[i] = Instantiate(textPrefab, inventory.transform);
                text[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(250 - i * 50, -6, 0);
                text[i].GetComponent<TMP_Text>().text = "• Complete Circuit";
                text[i].GetComponent<TMP_Text>().color = new Color(1.0f, 0f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject == reset)
                {
                    ResetButton();
                }
            }
        }
    }

    private void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public bool CheckCircuit(GameObject obj)
    {
        bool allGoals = true;
        dynamic component = obj.GetComponent<CircuitController>().component;
        GameObject currObj = component.getNextComponent();
        float resistance = 0;
        float resistanceWBulbs = 0;
        float voltage = component.getVoltage();
        List<GameObject> bulbs = new List<GameObject>();
        while (obj != currObj)
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
        for (int i = 0; i < levelTypeList.Length; i++)
        {
            if (levelTypeList[i] == LevelType.GreatLightIntensity)
            {
                bool completedGoal = true;
                foreach (GameObject bulb in bulbs)
                {
                    if (bulb.GetComponent<CircuitController>().component.brightness < goal[i])
                    {
                        completedGoal = false;
                        allGoals = false;
                    }
                }
                if (completedGoal)
                {
                    text[i].GetComponent<TMP_Text>().color = new Color(0, 1, 0);
                }
            }
            if (levelTypeList[i] == LevelType.LowLightIntensity)
            {
                bool completedGoal = true;
                foreach (GameObject bulb in bulbs)
                {
                    if (bulb.GetComponent<CircuitController>().component.brightness >= goal[i])
                    {
                        completedGoal = false;
                        allGoals = false;
                    }
                }
                if (completedGoal)
                {
                    text[i].GetComponent<TMP_Text>().color = new Color(0, 1, 0);
                }
            }
            else if (levelTypeList[i] == LevelType.Resistance)
            {
                if (resistance < goal[i])
                {
                    allGoals = false;
                } else
                {
                    text[i].GetComponent<TMP_Text>().color = new Color(0, 1, 0);
                }
            }
            else if (levelTypeList[i] == LevelType.CompleteCircuit)
            {
                text[i].GetComponent<TMP_Text>().color = new Color(0, 1, 0);
            }
        }
        if (allGoals)
        {
            button.SetActive(true);
        }
        return allGoals;
    }

    public void OnNextLevel_Pressed()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
