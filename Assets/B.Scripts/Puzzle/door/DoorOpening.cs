
using UnityEngine; 
using System.Collections; 

    public class DoorOpening : MonoBehaviour
{ 
	private bool InZone = false; 
	private bool InOpened = false;

	void Start() { 
		
	} 

	void Update() { 
		if(Input.GetKeyDown (KeyCode.E) && InZone){ 
			InOpened = !InOpened;
			if (InOpened) GetComponent<Animation>().Play ("Open"); 
			else GetComponent<Animation> ().Play ("Close");
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