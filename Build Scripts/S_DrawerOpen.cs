using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DrawerOpen : MonoBehaviour
{
	private GameObject Player;
    public GameObject Drawer1;
    public GameObject Drawer2;
	public GameObject DrawerOpen1;
	public GameObject DrawerClose1;
    public GameObject DrawerOpen2;
    public GameObject DrawerClose2;
	private bool DState;
	private bool CoCheck;

	private GameObject[] List;

	// Use this for initialization
	void Start () 
	{
		DState = false;
		CoCheck = false;

		if (Drawer1.transform.position != DrawerClose1.transform.position)
		{
			Drawer1.transform.position = Vector3.MoveTowards(Drawer1.transform.position, DrawerClose1.transform.position, 100);
		}

        if (Drawer2.transform.position != DrawerClose2.transform.position)
		{
			Drawer2.transform.position = Vector3.MoveTowards(Drawer2.transform.position, DrawerClose2.transform.position, 100);
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
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(transform.position, Player.transform.position) < 2)
		{
			if(Input.GetKeyDown(KeyCode.E)) 
			{
				if(CoCheck == false)
				{
					StopCoroutine(OpenDrawers());
					StartCoroutine(OpenDrawers());
					CoCheck = true;
				}
            }
		}
	}
	
	IEnumerator OpenDrawers()
	{
		if (DState == false)
		{
			while(DState == false)
			{
				Drawer1.transform.position = Vector3.MoveTowards(Drawer1.transform.position, DrawerOpen1.transform.position, 0.005f);
                Drawer2.transform.position = Vector3.MoveTowards(Drawer2.transform.position, DrawerOpen2.transform.position, 0.005f);
				yield return new WaitForEndOfFrame();

				if ((Drawer1.transform.position == DrawerOpen1.transform.position) && (Drawer2.transform.position == DrawerOpen2.transform.position))
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
				Drawer1.transform.position = Vector3.MoveTowards(Drawer1.transform.position, DrawerClose1.transform.position, 0.005f);
                Drawer2.transform.position = Vector3.MoveTowards(Drawer2.transform.position, DrawerClose2.transform.position, 0.005f);
				yield return new WaitForEndOfFrame();

				if ((Drawer1.transform.position == DrawerClose1.transform.position) && (Drawer2.transform.position == DrawerClose2.transform.position))
				{
					DState = false;
					CoCheck = false;
				}
			}
		}
	}
}
