using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CupboardDoor : MonoBehaviour
{
    public GameObject DoorHinge1;
    public GameObject DoorHinge2;
	private GameObject Player;
	private GameObject Monster;
	public GameObject DoorOpen1;
	public GameObject DoorClose1;
    public GameObject DoorOpen2;
    public GameObject DoorClose2;
	private bool DState;
	private bool CoCheck;

	private LayerMask Mask;
	private RaycastHit other;
	private GameObject[] List; 

	// Use this for initialization
	void Start () 
	{
		DState = false;
		CoCheck = false;

		Mask = LayerMask.GetMask("Cupboard");

		if (DoorHinge1.transform.rotation != DoorClose1.transform.rotation)
		{
			DoorHinge1.transform.rotation = Quaternion.RotateTowards(DoorHinge1.transform.rotation, DoorClose1.transform.rotation, 100.0f);
		}

        if (DoorHinge2.transform.rotation != DoorClose2.transform.rotation)
		{
			DoorHinge2.transform.rotation = Quaternion.RotateTowards(DoorHinge2.transform.rotation, DoorClose2.transform.rotation, 100.0f);
		}

		List = GameObject.FindGameObjectsWithTag("Player");

		for (int i = 0; i < List.Length; i++)
		{
			Player = List[i];

			if (Player.name == "O_Player")
			{
				break;
			}
		}

		List = GameObject.FindGameObjectsWithTag("Monster");

		for (int i = 0; i < List.Length; i++)
		{
			Monster = List[i];

			if (Monster.name == "O_Monster")
			{
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(Player.transform.position, transform.position) < 1.75f)
		{
			if (Physics.Raycast(Player.transform.position, Player.transform.forward, out other, Mask))
			{
				if(Input.GetKeyDown(KeyCode.E)) 
				{
					if(CoCheck == false)
					{
						StopCoroutine(OpenDoors());
						StartCoroutine(OpenDoors());
						CoCheck = true;
					}
				}
			}
		}

		if (Vector3.Distance(Monster.transform.position, transform.position) < 1.75f)
		{
			if(DState == false)
			{
				if (CoCheck == false)
				{
					StartCoroutine(OpenDoors());
				}
			}
		}
	}
	
	IEnumerator OpenDoors()
	{
		if (DState == false)
		{
			while(DState == false)
			{
				DoorHinge1.transform.rotation = Quaternion.RotateTowards(DoorHinge1.transform.rotation, DoorOpen1.transform.rotation, 100.0f * Time.deltaTime);
                DoorHinge2.transform.rotation = Quaternion.RotateTowards(DoorHinge2.transform.rotation, DoorOpen2.transform.rotation, 100.0f * Time.deltaTime);
				yield return new WaitForEndOfFrame();

				if ((DoorHinge1.transform.rotation == DoorOpen1.transform.rotation) && (DoorHinge2.transform.rotation == DoorOpen2.transform.rotation))
				{
					DState = true;
					CoCheck = false;
				}
			}
		}
		
		else
		{
			while(DState == true)
			{
				DoorHinge1.transform.rotation = Quaternion.RotateTowards(DoorHinge1.transform.rotation, DoorClose1.transform.rotation, 100.0f * Time.deltaTime);
                DoorHinge2.transform.rotation = Quaternion.RotateTowards(DoorHinge2.transform.rotation, DoorClose2.transform.rotation, 100.0f * Time.deltaTime);
				yield return new WaitForEndOfFrame();

				if ((DoorHinge1.transform.rotation == DoorClose1.transform.rotation) && (DoorHinge2.transform.rotation == DoorClose2.transform.rotation))
				{
					DState = false;
					CoCheck = false;
				}
			}
		}
	}
}
