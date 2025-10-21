
using UnityEngine; 
using System.Collections; 

    public class WindowOpening : MonoBehaviour { 
	private bool InZone = false; 
	private bool InOpened = false;

	void Start() { 
		
	} 

	void Update() { 
		if(Input.GetKeyDown (KeyCode.E) && InZone){ 
			InOpened = !InOpened;
			if (InOpened) GetComponent<Animation>().Play ("WindowOpen"); 
			else GetComponent<Animation> ().Play ("WindowClose");
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