using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Cutscenes : MonoBehaviour
{
    public GameObject Monster;

    void Start()
    {
        if (Monster != null)
        {
            StartCoroutine(Wait());
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("scene1");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(12f);

        Monster.SetActive(true);
    }
}
