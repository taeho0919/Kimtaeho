using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vault : MonoBehaviour
{
    [Header("금고 관련 오브젝트")]
    public Transform pivot;             // 회전축
    public float angle = -90f;          // 열릴 각도
    public float speed = 1f;            // 회전 속도
    public GameObject vaultUI;          // 키패드 UI
    public GameObject Green;            // 성공 시 UI
    public GameObject vaultinObject;    // 금고 내부 오브젝트

    private PlayerSkill ps;
    private PlayerMovement pm;
    private code c;

    private bool isTrigger=false;
    private bool isOnlyOne=false;
    private bool isOpen = false;        // 현재 금고가 열렸는지 체크

    private void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        ps = FindObjectOfType<PlayerSkill>();
        c = FindObjectOfType<code>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&isOnlyOne==false&&isTrigger&&true)
        {
              c.OpenVaultUI(); // code.cs UI 열기 호출
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<code>().CloseVaultUI();
        }
    }

    // 금고 열림 루틴
    public IEnumerator vaultSuccessRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        // UI 닫기
        if (Green != null) Green.SetActive(false);
        if (vaultUI != null) vaultUI.SetActive(false);
        vaultinObject.SetActive(true);

        Quaternion startRotation = pivot.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            pivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        pivot.localRotation = targetRotation;

        Debug.Log("금고 열림 완료 → 내부 오브젝트 활성화");
        
        // 퍼즐 관련 상태 초기화
        if (ps != null) ps.objectOnOff = false;
        if (pm != null) pm.objectOnOff = false;
        isOnlyOne = true;
        isOpen = true;
    }

    // 외부에서 금고 열기 호출
    public void OpenVault()
    {
        if (!isOpen) 
        {
            // 안 열렸을 때만 실행
            StartCoroutine(vaultSuccessRoutine());
        }   

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            
        }
    }

}
