using UnityEngine;
using System.Collections;

public class Door_sound : MonoBehaviour {
	public AudioClip AudioEvent; 
	[Range(0, 1)] 
	public float VolumeClip1 = 1; 
	public AudioClip AudioEvent_2; 
	[Range(0, 1)] 
	public float VolumeClip2 = 1; 

	// Use this for initialization
	void Start () {}
	
		public void AudioEvantPlay(int i) 
		{ 
		if(i == 0) GetComponent<AudioSource>().PlayOneShot(AudioEvent, VolumeClip1); 
		if(i == 1) GetComponent<AudioSource>().PlayOneShot(AudioEvent_2, VolumeClip2); 
		}
}
