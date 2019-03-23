using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	float speed = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			//transform.Translate(Vector3.forward * Time.deltaTime * speed);
			transform.position += Vector3.forward;
		}
		if(Input.GetKey(KeyCode.D))
		{
			//transform.Translate(Vector3.right * Time.deltaTime * speed);
			transform.position += Vector3.right;
		}
		if(Input.GetKey(KeyCode.S))
		{
			//transform.Translate(Vector3.back * Time.deltaTime * speed);
			transform.position += Vector3.back;
		}
		if(Input.GetKey(KeyCode.A))
		{
			//transform.Translate(Vector3.left * Time.deltaTime * speed);
			transform.position += Vector3.left;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		WaterManager.instance.updateGrid((int)other.transform.position.x, (int)other.transform.position.z, 3);
	}

	private void OnTriggerExit(Collider other)
	{
		WaterManager.instance.updateGrid((int)other.transform.position.x, (int)other.transform.position.z, -3);
	}
}
