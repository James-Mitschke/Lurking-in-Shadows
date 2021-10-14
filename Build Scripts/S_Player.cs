using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Player : MonoBehaviour 
{
	public GameObject Torch;
	public GameObject Lantern;
	public GameObject Match;
	GameObject Player;
	private Rigidbody PB;
	private float WalkSpeed = 0.05f;
	private float SprintSpeed = 0.10f;
	private int TTypeCheck;
	private int TorchType;
	public int SprintLeft = 10;
	private int Timer;
	
	float avgFPS;
	float FPS;
	float oldRestAmount;
	float newRestAmount;
	float totalRestAmount;

	private bool awake;
	private bool resting;

	float yRot = 0.0f;

	Vector3 Direction;

	void Awake ()
	{
		avgFPS = 0;

		Player = gameObject;
		PB = Player.GetComponent<Rigidbody>();
		TorchType = 1;
		TTypeCheck = TorchType;
		Torch.SetActive(true);
		Lantern.SetActive(false);
		Match.SetActive(false);
		Timer = 0;
		resting = true;
	}
	
	void Update () 
	{
		avgFPS += ((Time.deltaTime/Time.timeScale) - avgFPS) * 0.03f;

		FPS = (1F/avgFPS);

		if ((Input.GetKey(KeyCode.LeftShift)) & (SprintLeft >= 1) & (resting == true))		
		{
			resting = false;
			StopAllCoroutines();
			Timer = 0;
			StartCoroutine(Sprint());
		}

		else if ((Input.GetKey(KeyCode.LeftShift) == false) && (Timer > 0) && (resting == true))
		{
			SprintLeft = SprintLeft - Timer;
			Timer = 0;
			WalkSpeed = 0.05f;
			resting = false;
		}

		if(Input.mouseScrollDelta.y > 0)
		{
			if(TorchType > 2)
			{
				TorchType = 1;
			}

			else if (TorchType <= 2)
			{
				TorchType += 1;
			}
		}

		if(Input.mouseScrollDelta.y < 0)
		{
			if(TorchType < 2)
			{
				TorchType = 3;
			}

			else if (TorchType >= 2)
			{
				TorchType -= 1;
			}
		}

		float MouseX = Input.GetAxis("Mouse X") * 5.0f;

		yRot += MouseX;

		transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);
	}

	void FixedUpdate()
	{
		Direction = transform.rotation * Direction;
		Vector3 Movement = new Vector3(0, 0, 0);
		
		if (Input.GetKey(KeyCode.W))
		{
			Movement = Movement + transform.forward;
		}

		if (Input.GetKey(KeyCode.A))
		{
			Movement = Movement - transform.right;
		}

		if (Input.GetKey(KeyCode.D))
		{
			Movement = Movement + transform.right;
		}

		if (Input.GetKey(KeyCode.S))
		{
			Movement = Movement - transform.forward;
		}

		PB.MovePosition(transform.position + (Movement * WalkSpeed));
	}

	void LateUpdate()
	{
		if((TorchType == 1) & (TTypeCheck != TorchType))
		{
			Torch.SetActive(true);
			Lantern.SetActive(false);
			Match.SetActive(false);
			TTypeCheck = TorchType;
		}

		if((TorchType == 2) & (TTypeCheck != TorchType))
		{
			Torch.SetActive(false);
			Lantern.SetActive(true);
			Match.SetActive(false);
			TTypeCheck = TorchType;
		}

		if((TorchType == 3) & (TTypeCheck != TorchType))
		{
			Torch.SetActive(false);
			Lantern.SetActive(false);
			Match.SetActive(true);
			TTypeCheck = TorchType;
		}

		if (Input.GetKey(KeyCode.LeftShift) == false)
		{
			if (SprintLeft < 0)
			{
				SprintLeft = 0;
			}

			if (resting == false)
			{
				resting = true;
				StopAllCoroutines();
				StartCoroutine(Resting());
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Monster")
		{
			SceneManager.LoadScene("scene1");
		}

		if (other.tag == "Exit")
		{
			SceneManager.LoadScene("Mansion_End");
		}
	}

	IEnumerator Sprint()
	{
		while (Timer < SprintLeft)
		{
			Timer = Timer + 1;
			WalkSpeed = SprintSpeed;

			yield return new WaitForSeconds(1);
		}

		SprintLeft = SprintLeft - Timer;

		if(Input.GetKey(KeyCode.LeftShift) != true)
		{
			WalkSpeed = 0.05f;
			StopAllCoroutines();
			yield break;
		}

		if (SprintLeft <= 0)
		{
			WalkSpeed = 0.05f;
			SprintLeft = 0;
			yield break;
		}
	}

	IEnumerator Resting()
	{
		while(SprintLeft < 10)
		{
			if (SprintLeft < 2)
			{
				yield return new WaitForSeconds(1);
			}

			SprintLeft = SprintLeft + 1;

			yield return new WaitForSeconds(1);
		}

		if (SprintLeft >= 10)
		{
			SprintLeft = 10;
			yield break;
		}
	}
}
