using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Lock;
    public bool KeyChecker = false;

    private bool isInTrigger = false; // 플레이어가 키 트리거 안에 있는지

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.Mouse0) && !KeyChecker)
        {
            KeyChecker = true;
            Lock.SetActive(false);
            Destroy(gameObject);
            Debug.Log("열쇠를 획득하고 자물쇠를 열었습니다!");
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
