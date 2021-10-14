using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TorchLight : MonoBehaviour 
{
	private Light Torch;
	private bool LightOn;
	public GameObject Light;
	// Use this for initialization
	void Start () 
	{
		LightOn = true;
		Torch = gameObject.GetComponent<Light>();
		if(Light.activeInHierarchy == false)
		{
			LightOn = false;
		}
		Torch.enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Light.activeInHierarchy == true) & (LightOn == false))
		{
			LightOn = true;
		}

		else if ((Light.activeInHierarchy == false) & (LightOn == true))
		{
			LightOn = false;
		}

		if (((Input.GetKeyDown(KeyCode.F)) | (Input.GetMouseButtonDown(0))) & (LightOn == true))
		{
			Torch.enabled = !Torch.enabled;
		}

		if(LightOn == false)
		{
			Torch.enabled = false;
		}
	}
}
