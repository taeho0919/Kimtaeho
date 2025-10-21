using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    public GameObject memo;

    private PlayerSkill ps;
    private PlayerMovement pm;

    private bool isInTrigger = false; // 플레이어가 트리거 안에 있는지
    private bool isMemoOpen = false;  // 메모 열림 상태

    void Start()
    {
        ps = FindObjectOfType<PlayerSkill>();
        pm = FindObjectOfType<PlayerMovement>();
        memo.SetActive(false);
    }

    void Update()
    {
        // 트리거 안에서 클릭 시
        if (isInTrigger && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isMemoOpen)
            {
                StartCoroutine(SeeMemo());
            }
            else
            {
                StartCoroutine(NoSeeMemo());
            }
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

            // 트리거 나가면 메모 자동 닫기
            if (isMemoOpen)
            {
                StartCoroutine(NoSeeMemo());
            }
        }
    }

    private IEnumerator SeeMemo()
    {
       
        isMemoOpen = true;

        memo.SetActive(true);
        ps.objectOnOff = true;
        pm.objectOnOff = true;

        Debug.Log("메모를 보기 시작합니다!");

        yield return new WaitForSeconds(2f); // 필요시 유지 시간
     
    }

    private IEnumerator NoSeeMemo()
    {
    
        isMemoOpen = false;

        memo.SetActive(false);
        ps.objectOnOff = false;
        pm.objectOnOff = false;

        Debug.Log("메모를 그만봅니다");

        yield return new WaitForSeconds(2f); // 필요시 유지 시간
      
    }
}
