using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] BegSection;
    public GameObject[] IntSection;
    public GameObject[] ExpSection;
    public int zPos = 50;
    public bool creatingSection = false;
    public int secNum;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false)
        {
            // Check if the player has reached the z position to switch to intermediate sections
            if (player.transform.position.z >= 350)
            {
                ExpSectionGenerator();
            }
            else if (player.transform.position.z >= 150 && player.transform.position.z <= 349)
            {
                IntSectionGenerator();
            }
            else
            {
                BegSectionGenerator();
            }
        }
    }

    void BegSectionGenerator()
    {
        creatingSection = true;
        secNum = Random.Range(0, BegSection.Length);
        Instantiate(BegSection[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;
        StartCoroutine(WaitForSection());
    }

    void IntSectionGenerator()
    {
        creatingSection = true;
        secNum = Random.Range(0, IntSection.Length);
        Instantiate(IntSection[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;
        StartCoroutine(WaitForSection());
    }

    void ExpSectionGenerator()
    {
        creatingSection = true;
        secNum = Random.Range(0, ExpSection.Length);
        Instantiate(ExpSection[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;
        StartCoroutine(WaitForSection());
    }

    IEnumerator WaitForSection()
    {
        yield return new WaitForSeconds(5);
        creatingSection = false;
    }
}
