
using UnityEngine; 
using System.Collections; 

    public class DoorOpeningInterior : MonoBehaviour
{ 
	private bool InZone = false; 
	private bool InOpened = false;

	void Start() { 
		
	} 

	void Update() { 
		if(Input.GetKeyDown (KeyCode.E) && InZone){ 
			InOpened = !InOpened;
			if (InOpened) GetComponent<Animation>().Play ("OpenInteriorDoor"); 
			else GetComponent<Animation> ().Play ("CloseInteriorDoor");
		} 
	} 
	void OnTriggerEnter(Collider other){ 
		if(other.gameObject.tag == "Player"){ 
			InZone = true; 
		} 
	} 
	void OnTriggerExit(Collider other){ 
		if(other.gameObject.tag == "Player"){ 
			InZone = false; 
  } 
}
}