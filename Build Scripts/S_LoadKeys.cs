using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LoadKeys : MonoBehaviour
{
    private GameObject[] List;
    private GameObject Check;

    void Start()
    {
        StartCoroutine(Delay());
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("Servant Quarters"))
        {
            PlayerPrefs.SetInt("Servant Quarters", 0);
        }

        else if (PlayerPrefs.GetInt("Servant Quarters") == 1)
        {
            List = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < List.Length; i++)
            {
                Check = List[i];

                if (Check.name == "Servant Quarters Key")
                {
                    Destroy(Check);
                    break;
                }
            }
        }

        if (!PlayerPrefs.HasKey("Master Bedroom"))
        {
            PlayerPrefs.SetInt("Master Bedroom", 0);
        }

        else if (PlayerPrefs.GetInt("Master Bedroom") == 1)
        {
            List = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < List.Length; i++)
            {
                Check = List[i];

                if (Check.name == "Master Bedroom Key")
                {
                    Destroy(Check);
                    break;
                }
            }
        }

        if (!PlayerPrefs.HasKey("Family Dining Room"))
        {
            PlayerPrefs.SetInt("Family Dining Room", 0);
        }

        else if (PlayerPrefs.GetInt("Family Dining Room") == 1)
        {
            List = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < List.Length; i++)
            {
                Check = List[i];

                if (Check.name == "Family Dining Room Key")
                {
                    Destroy(Check);
                    break;
                }
            }
        }

        if (!PlayerPrefs.HasKey("Bathroom"))
        {
            PlayerPrefs.SetInt("Bathroom", 0);
        }

        else if (PlayerPrefs.GetInt("Bathroom") == 1)
        {
            List = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < List.Length; i++)
            {
                Check = List[i];

                if (Check.name == "Bathroom Key")
                {
                    Destroy(Check);
                    break;
                }
            }
        }

        if (!PlayerPrefs.HasKey("Basement"))
        {
            PlayerPrefs.SetInt("Basement", 0);
        }

        else if (PlayerPrefs.GetInt("Basement") == 1)
        {
            List = GameObject.FindGameObjectsWithTag("Key");

            for (int i = 0; i < List.Length; i++)
            {
                Check = List[i];

                if (Check.name == "Basement Key")
                {
                    Destroy(Check);
                    break;
                }
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);

        Init();
    }
}