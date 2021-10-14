using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MonsterCam : MonoBehaviour
{
    public GameObject O_Monster;
	GameObject MCamera;
	Vector3 Offset;
    float Rotation;
    private GameObject[] List;
    private GameObject Player;

	// Use this for initialization
	void Start () 
	{
		MCamera = gameObject;
		Offset = (gameObject.transform.position - O_Monster.transform.position);

        List = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < List.Length; i++)
        {
            Player = List[i];

            if (Player.name == "O_Player")
            {
                break;
            }
        }
	}

	void FixedUpdate ()
	{
        MCamera.transform.LookAt(Player.transform.position);
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		if(MCamera.transform.position != O_Monster.transform.position + Offset)
		{
			MCamera.transform.position = O_Monster.transform.position + Offset;
		}
	}
}
