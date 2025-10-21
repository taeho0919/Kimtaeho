using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    public GameObject memo;

    private PlayerSkill ps;
    private PlayerMovement pm;

    private bool isInTrigger = false; // �÷��̾ Ʈ���� �ȿ� �ִ���
    private bool isMemoOpen = false;  // �޸� ���� ����

    void Start()
    {
        ps = FindObjectOfType<PlayerSkill>();
        pm = FindObjectOfType<PlayerMovement>();
        memo.SetActive(false);
    }

    void Update()
    {
        // Ʈ���� �ȿ��� Ŭ�� ��
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

            // Ʈ���� ������ �޸� �ڵ� �ݱ�
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

        Debug.Log("�޸� ���� �����մϴ�!");

        yield return new WaitForSeconds(2f); // �ʿ�� ���� �ð�
     
    }

    private IEnumerator NoSeeMemo()
    {
    
        isMemoOpen = false;

        memo.SetActive(false);
        ps.objectOnOff = false;
        pm.objectOnOff = false;

        Debug.Log("�޸� �׸����ϴ�");

        yield return new WaitForSeconds(2f); // �ʿ�� ���� �ð�
      
    }
}
