using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public GameObject disEndDisplay;
    public static int disRun;
    private bool addingDis = false;

    void Update()
    {
        // Check if we are already incrementing the distance counter
        if (addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

    IEnumerator AddingDis()
    {
        // Increment the distance value and update the UI text
        disRun += 1;
        disDisplay.GetComponent<TextMeshProUGUI>().text = "" + disRun;
        disEndDisplay.GetComponent<TextMeshProUGUI>().text = "" + disRun;
        // Wait for the specified time interval
        yield return new WaitForSeconds(0.25f);

        // Reset the flag to allow the distance counter to be incremented again
        addingDis = false;
    }
}
