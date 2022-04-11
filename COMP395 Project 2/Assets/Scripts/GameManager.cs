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
        Screen.SetResolution(1920, 1080, 0);
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

    public bool CheckCircuit(GameObject obj)
    {
        bool allGoals = true;
        BatteryController component = obj.GetComponent<CircuitController>().battComp;
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
                voltage += currObj.GetComponent<CircuitController>().battComp.getVoltage();
            }
            else if (currObj.CompareTag("Resistor"))
            {
                resistance += currObj.GetComponent<CircuitController>().resComp.getResistance();
            }
            else if (currObj.CompareTag("Bulb"))
            {
                LightbulbController battComp = currObj.GetComponent<CircuitController>().liComp;
                bulbs.Add(currObj);
            }
            currObj = currObj.GetComponent<CircuitController>().GetNext();
        }
        resistanceWBulbs = resistance;
        foreach (GameObject bulb in bulbs)
        {
            resistanceWBulbs += bulb.GetComponent<CircuitController>().liComp.getResistance(voltage);
        }
        foreach (GameObject bulb in bulbs)
        {
            bulb.GetComponent<CircuitController>().liComp.setBrightness(Mathf.Pow(voltage / resistanceWBulbs, 2) * (resistance + bulb.GetComponent<CircuitController>().liComp.getResistance(voltage)));
        }
        for (int i = 0; i < levelTypeList.Length; i++)
        {
            if (levelTypeList[i] == LevelType.GreatLightIntensity)
            {
                bool completedGoal = true;
                foreach (GameObject bulb in bulbs)
                {
                    if (bulb.GetComponent<CircuitController>().liComp.brightness < goal[i])
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
                    if (bulb.GetComponent<CircuitController>().liComp.brightness >= goal[i])
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
