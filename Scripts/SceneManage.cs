using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject spawnLocation;
    public ColliderPart colPart;
    private Collider correct;
    public Audio sound;

    private int slotIndex = 0;

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject Slot5;
    public GameObject Slot6;
    public GameObject Slot7;
    public GameObject Side1;
    public GameObject Side2;
    public GameObject Side3;
    public GameObject Side4;
    public GameObject Side5;

    public GameObject Zone1;
    public GameObject Zone2;
    public GameObject Zone3;
    public GameObject Zone4;
    public GameObject Zone5;
    public GameObject Zone6;
    public GameObject Zone7;
    public GameObject Zone8;
    public GameObject Zone9;
    public GameObject Zone10;
    public GameObject Zone11;
    public GameObject Zone12;

    public Text priceWeight;
    private float weightTotal = 107.7f;
    private float priceTotal = 1800f;
    private float powerTotal = 0f;
    private float tempMinCurrent = -1000f;
    private float tempMinTemp = -1000f;
    private float tempMaxCurrent = 1000f;
    private float tempMaxTemp = 1000f;
    private bool removed = false;
    public PartController[] partsUI;


    void Start()
    {
        correct = null;
    }

    void Update()
    {

    }

    public void OnReleaseCheck(PartSO part, PartController col) // When user releases a part
    {
        //Debug.Log("RECIEVE: part = " + part.partName);
        correct = null;
        slotIndex = 0;
        foreach (var collider in col.current) // Find collider in array with correct slot
        {
            for (int i = 0; i < part.slot.Length; i++)
            {
                if (collider.gameObject.name == part.slot[i])
                {
                    correct = collider;
                    slotIndex = i;
                }
            }
        }

        if (correct == null) // If part is released with no collider present (taken off the frame)
        {
            //Debug.Log("correct = null");
            if (col.isOn)
            {
                removed = true;
                col.isOn = false;
                UpdateUI(-(part.price), -(part.weight), -(part.power), part.tempMin, part.tempMax);
                col.wasOn.gameObject.SetActive(true); // Set the slot it was on back to active
                col.wasOn = null;
            }
            col.transform.SetParent(null);
        }       
        else if (part.slot[slotIndex] == correct.gameObject.name) // Check if part is colliding with the correct slot
        {
            //Debug.Log("correct = " + correct);
            if (!col.isOn)
            {
                removed = false;
                col.isOn = true;
                UpdateUI(part.price, part.weight, part.power, part.tempMin, part.tempMax); // Update price and weight
                col.wasOn = correct.gameObject;
            }
            sound.PlaySound("snapping");
            //Debug.Log("move");
            MovePart(col, correct); // Move the part (snapping feature)
            correct.gameObject.SetActive(false); // Slot is occupied so set to not active
        }
    }

    public void MovePart(PartController part, Collider correct)
    {
        //Move part to the corresponding collider position and set parent
        part.transform.position = new Vector3(correct.transform.position.x, correct.transform.position.y, correct.transform.position.z);
        part.transform.rotation = correct.transform.rotation;
        part.transform.SetParent(gameObject.transform);
        correct = null;
    }

    public void UpdateUI(float price, float weight, float power, float tempMin, float tempMax)
    {
        // Update price, weight, and power totals
        priceTotal += price;
        weightTotal += weight;
        powerTotal += power;

        powerTotal = Mathf.Round(powerTotal * 10f) / 10f;

        // If a part has just been put on, check if temp range needs update
        if (tempMin > tempMinCurrent && !removed)
        {
            tempMinCurrent = tempMin;
        }
        if (tempMax < tempMaxCurrent && !removed)
        {
            tempMaxCurrent = tempMax;
        }

        // If a part has been taken off and was part of the range, then find new min/max value from list of parts on the cubesat
        if (tempMin == tempMinCurrent && removed)
        {
            tempMinTemp = -1000f;
            partsUI = GameObject.FindObjectsOfType<PartController>();
            foreach (PartController part in partsUI)
            {
                if (part.isOn)
                {
                    if (part.part.tempMin > tempMinTemp)
                    {
                        tempMinTemp = part.part.tempMin;
                    }
                }
            }
            tempMinCurrent = tempMinTemp;
        }
        if (tempMax == tempMaxCurrent && removed)
        {
            tempMaxTemp = 1000f;
            partsUI = GameObject.FindObjectsOfType<PartController>();
            foreach (PartController part in partsUI)
            {
                if (part.isOn)
                {
                    if (part.part.tempMax < tempMaxTemp)
                    {
                        tempMaxTemp = part.part.tempMax;
                    }
                }
            }
            tempMaxCurrent = tempMaxTemp;
        }

        // Update UI text
        if (tempMaxCurrent == 1000f)
        {
            priceWeight.text = "Price: £" + priceTotal + "\n\nWeight: " + weightTotal + "g" + "\n\nPower Consumption: " + powerTotal + "W" + "\n\nOptimal Temperature Range: N/A";
        }
        else
        {
            priceWeight.text = "Price: £" + priceTotal + "\n\nWeight: " + weightTotal + "g" + "\n\nPower Consumption: " + powerTotal + "W" + "\n\nOptimal Temperature Range: " + tempMinCurrent + "°C - " + tempMaxCurrent + "°C";
        }
    }

    // Spawn part at spawn location with same scale as this game object
    public void SpawnPart(PartSO part)
    {
        GameObject newPart = Instantiate(part.gameObj, spawnLocation.transform.position, new Quaternion(0, 0, 0, 0)); // Instantiate new part
        newPart.transform.localScale = transform.localScale; // Set the scale of the new part to the current cubesatframe
    }

    public void EnableZone(int zone)
    {
        switch (zone)
        {
            case 1:
                Zone1.SetActive(!Zone1.activeSelf);
                break;
            case 2:
                Zone2.SetActive(!Zone2.activeSelf);
                break;
            case 3:
                Zone3.SetActive(!Zone3.activeSelf);
                break;
            case 4:
                Zone4.SetActive(!Zone4.activeSelf);
                break;
            case 5:
                Zone5.SetActive(!Zone5.activeSelf);
                break;
            case 6:
                Zone6.SetActive(!Zone6.activeSelf);
                Zone7.SetActive(!Zone7.activeSelf);
                Zone8.SetActive(!Zone8.activeSelf);
                Zone9.SetActive(!Zone9.activeSelf);
                Zone10.SetActive(!Zone10.activeSelf);
                break;
            case 7:
                Zone11.SetActive(!Zone11.activeSelf);
                break;
            case 8:
                Zone12.SetActive(!Zone12.activeSelf);
                break;
            default:
                break;
        }
    }

    public void ResetParts() 
    {
        // Delete all current parts in scene
        PartController[] parts;
        parts = GameObject.FindObjectsOfType<PartController>();
        foreach (PartController part in parts)
        {
            Destroy(part.gameObject);
        }

        // Reset active states of slots
        Slot1.SetActive(true);
        Slot2.SetActive(true);
        Slot3.SetActive(true);
        Slot4.SetActive(true);
        Slot5.SetActive(true);
        Slot6.SetActive(true);
        Slot7.SetActive(true);
        Side1.SetActive(true);
        Side2.SetActive(true);
        Side3.SetActive(true);
        Side4.SetActive(true);
        Side5.SetActive(true);

        // Reset UI values
        tempMaxCurrent = 1000f;
        tempMaxTemp = 1000f;
        tempMinCurrent = -1000f;
        tempMinTemp = -1000f;
        priceTotal = 1800f;
        weightTotal = 107.7f;
        powerTotal = 0f;
        UpdateUI(0, 0, 0, -1000f, 1000f);
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
