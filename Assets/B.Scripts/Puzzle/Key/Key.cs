using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Lock;
    public bool KeyChecker = false;

    private bool isInTrigger = false; // �÷��̾ Ű Ʈ���� �ȿ� �ִ���

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.Mouse0) && !KeyChecker)
        {
            KeyChecker = true;
            Lock.SetActive(false);
            Destroy(gameObject);
            Debug.Log("���踦 ȹ���ϰ� �ڹ��踦 �������ϴ�!");
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
