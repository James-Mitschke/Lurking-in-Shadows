using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Credits : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(10.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
