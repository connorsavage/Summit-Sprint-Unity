using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndRunSequence : MonoBehaviour
{
    public GameObject liveDis;
    public GameObject liveHighScore;
    public GameObject endScreen;
    public GameObject fadeOut;

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(2);
        liveDis.SetActive(false);
        liveHighScore.SetActive(false);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
