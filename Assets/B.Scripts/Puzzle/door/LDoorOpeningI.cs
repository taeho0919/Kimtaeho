using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDoorOpeningI : MonoBehaviour
{
	public Key key;

	private bool InZone = false;
	private bool InOpened = false; 

	void Start()
	{
		key.GetComponent<Key>();
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.E) && InZone&&key.KeyChecker==true)
		{
			InOpened = !InOpened;
			if (InOpened) GetComponent<Animation>().Play("OpenInteriorDoor");
			else GetComponent<Animation>().Play("CloseInteriorDoor");
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			InZone = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			InZone = false;
		}
	}
}
