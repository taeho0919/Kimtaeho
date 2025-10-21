using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorFantasy : MonoBehaviour
{
    public AudioClip Sound;
    public GameObject Image;
    public float time=2f;
    private bool isOnlyOne=false;
    private AudioSource audiosource;
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&isOnlyOne==false)
        {
            StartCoroutine(HorrorImage());
        }
    }

    IEnumerator HorrorImage()
    {
        Image.SetActive(true);         // 이미지 켜기

            audiosource.PlayOneShot(Sound, 1);


        yield return new WaitForSeconds(time); // 2초 기다림
        Image.SetActive(false);        // 이미지 끄기
        isOnlyOne = true;
    }
}
