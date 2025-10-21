using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyToy : MonoBehaviour
{
    public bool isToyUse=false;
    private bool isInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&isInTrigger==true)
        {
                isToyUse=true;
                Destroy(gameObject);
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
