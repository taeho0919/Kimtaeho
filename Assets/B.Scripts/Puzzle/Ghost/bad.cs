using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad : MonoBehaviour
{
    public bool istouchbad = false;
    public GameObject Ghost;
    public Transform ghostTF;
    public AudioClip sound;
    private AudioSource audioSource;
    private bool isOnlyone=false;
    private bool isInTrigger=false;
    public bool isSpwon=false;
    private PlayerMovement pm;
    private PlayerSkill ps;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pm = FindObjectOfType<PlayerMovement>();
        ps = FindObjectOfType<PlayerSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnlyone == false&&isInTrigger==true)
        {
                isOnlyone = true;
                istouchbad = true;
            audioSource.PlayOneShot(sound, 1);
                Instantiate(Ghost, ghostTF.transform.position, Quaternion.Euler(0, 90, 0));
                 isSpwon = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }

}
