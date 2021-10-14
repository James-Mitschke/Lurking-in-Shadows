using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class S_DoorOpen : MonoBehaviour 
{
	public GameObject o_DoorHinge;
	private GameObject Player;
	private GameObject Monster;
	public GameObject DoorOpen;
	public GameObject DoorClose;
	private NavMeshObstacle Obs;

	private TMP_Text CanvasText;

	private LayerMask Mask;
	private bool DState;
	private bool CoCheck;
	private bool Locked;

	private string NameText;
	private RaycastHit other;

	private GameObject[] List; 

	// Use this for initialization
	void Start () 
	{
		CanvasText = GameObject.FindGameObjectWithTag("CanvasText").GetComponent<TMP_Text>();

		Mask = LayerMask.GetMask("Doors");
		Locked = false;

		DState = false;
		CoCheck = false;

		if (o_DoorHinge.transform.rotation != DoorClose.transform.rotation)
		{
			o_DoorHinge.transform.rotation = Quaternion.RotateTowards(o_DoorHinge.transform.rotation, DoorClose.transform.rotation, 100.0f);
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

		NameText = name;
		NameText = NameText.Replace(" Door", "");

		StartCoroutine(LoadLocked());

		if (GetComponent<NavMeshObstacle>())
		{
			GetComponent<NavMeshObstacle>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(Player.transform.position, transform.position) < 2f)
		{
			if (Physics.Raycast(Player.transform.position, Player.transform.forward, out other, Mask))
			{
				if(Input.GetKeyDown(KeyCode.E)) 
				{
					if(Locked == false)
					{
						if (CoCheck == false)
						{
							StopCoroutine(OpenDoor());
							StartCoroutine(OpenDoor());
							CoCheck = true;
						}
					}

					else
					{
						if(PlayerPrefs.GetInt(NameText) == 1)
						{
							Locked = false;
							StartCoroutine(OpenText());
						}

						else
						{
							StartCoroutine(LockedText());
							Obs.enabled = false;
						}
					}
				}
			}
		}

		if (Vector3.Distance(Monster.transform.position, transform.position) < 2f)
		{
			if (Locked == false)
			{
				if (DState == false)
				{
					if (CoCheck == false)
					{
						StartCoroutine(OpenDoor());
					}
				}
			}
		}
	}
	
	IEnumerator OpenDoor()
	{
		if (DState == false)
		{
			while(DState == false)
			{
				o_DoorHinge.transform.rotation = Quaternion.RotateTowards(o_DoorHinge.transform.rotation, DoorOpen.transform.rotation, 100.0f * Time.deltaTime);
				yield return new WaitForEndOfFrame();

				if ((o_DoorHinge.transform.rotation == DoorOpen.transform.rotation))
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
				o_DoorHinge.transform.rotation = Quaternion.RotateTowards(o_DoorHinge.transform.rotation, DoorClose.transform.rotation, 100.0f * Time.deltaTime);
				yield return new WaitForEndOfFrame();

				if ((o_DoorHinge.transform.rotation == DoorClose.transform.rotation))
				{
					DState = false;
					CoCheck = false;
				}
			}
		}
	}

	IEnumerator OpenText()
	{
		CanvasText.text = "Unlocked " +name +"!";

		yield return new WaitForSeconds(2.5f);

		if (CanvasText.text == ("Unlocked " +name +"!"))
		{
			CanvasText.text = "";
		}
	}

	IEnumerator LockedText()
	{
		CanvasText.text = NameText +" is locked!";

		yield return new WaitForSeconds(2.5f);

		if (CanvasText.text == (NameText +" is locked!"))
		{
			CanvasText.text = "";
		}
	}

	IEnumerator LoadLocked()
	{
		yield return new WaitForSeconds(1);

		if(PlayerPrefs.HasKey(NameText))
		{
			if (PlayerPrefs.GetInt(NameText) == 0)
			{
				Locked = true;
			}

			else if (PlayerPrefs.GetInt(NameText) == 1)
			{
				Locked = false;
			}
		}
	}
}
