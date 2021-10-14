using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Key : MonoBehaviour
{
    private GameObject Player;
    public GameObject RayOrigin;
    private TMP_Text CanvasText;
    private LayerMask Mask;

    private RaycastHit other;

    private bool destroy;

    private string NameText;

    private GameObject[] List;


    // Start is called before the first frame update
    void Start()
    {
        List = GameObject.FindGameObjectsWithTag("Player");

		for (int i = 0; i < List.Length; i++)
		{
			Player = List[i];

			if (Player.name == "O_Player")
			{
				break;
			}
		}

        Mask = LayerMask.GetMask("Player");
        CanvasText = GameObject.FindGameObjectWithTag("CanvasText").GetComponent<TMP_Text>();
        destroy = false;

        NameText = name;
        NameText = NameText.Replace(" Key", "");
        
        if(!PlayerPrefs.HasKey(NameText))
        {
            PlayerPrefs.SetInt(NameText, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < 1.5f)
            {
                if(Physics.Raycast(RayOrigin.transform.position, (RayOrigin.transform.forward + RayOrigin.transform.up), out other, Mask))
                {
                    if ((other.transform.tag == "Player") && destroy == false)
                    {
                        destroy = true;
                        StartCoroutine(UpdateText());
                    }
                }
            }
        }
    }

    IEnumerator UpdateText()
    {
        CanvasText.text = ("Picked up " +name);
        PlayerPrefs.SetInt(NameText, 1);

        yield return new WaitForSeconds(2.5f);

        if(CanvasText.text == ("Picked up " +name))
        {
            CanvasText.text = "";
        }

        Destroy(gameObject, 0.1f);
    }
}
