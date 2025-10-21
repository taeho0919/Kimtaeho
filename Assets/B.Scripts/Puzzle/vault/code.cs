using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class code : MonoBehaviour
{
    [Header("Code Settings")]
    [SerializeField] private string correctCode = "1234"; // 정답 코드
    [SerializeField] private int maxLength = 4;           // 입력 가능한 자릿수
    private string inputCode = "";

    [Header("UI")]
    [SerializeField] private GameObject vaultUI; // 금고 UI
    [SerializeField] private TextMeshProUGUI codeDisplayText;
    [SerializeField] private GameObject Green;   // 성공 표시
    [SerializeField] private GameObject Red;     // 실패 표시

    [Header("플레이어 제어 스크립트")]
    [SerializeField] protected PlayerSkill ps;
    [SerializeField] protected PlayerMovement pm;

    private bool vaultChecker = false; // UI가 켜졌을 때 true

    void Start()
    {
        if (vaultUI != null) vaultUI.SetActive(false);
        if (Green != null) Green.SetActive(false);
        if (Red != null) Red.SetActive(false);
    }

    void Update()
    {
        if (!vaultChecker) return;

        // 숫자 입력
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                if (inputCode.Length < maxLength)
                {
                    inputCode += i.ToString();
                    UpdateCodeDisplay();
                }
            }
        }

        // 지우기
        if (Input.GetKeyDown(KeyCode.Backspace) && inputCode.Length > 0)
        {
            inputCode = inputCode.Substring(0, inputCode.Length - 1);
            UpdateCodeDisplay();
        }

        // 초기화
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            inputCode = "";
            UpdateCodeDisplay();
        }

        // 엔터 → 코드 확인
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputCode == correctCode)
            {
                Debug.Log("비밀번호 정답 → 금고 열림 시작");

                vaultChecker = false;
                inputCode = "";
                UpdateCodeDisplay();

                if (Green != null) Green.SetActive(true);
                if (Red != null) Red.SetActive(false);

                // ✅ vault.cs에 있는 OpenVault 호출
                FindObjectOfType<vault>().OpenVault();
            }
            else
            {
                Debug.Log("비밀번호 틀림!");

                inputCode = "";
                UpdateCodeDisplay();

                if (Green != null) Green.SetActive(false);
                if (Red != null) Red.SetActive(true);

                StartCoroutine(ResetFailVisual());
            }
        }
    }

    private void UpdateCodeDisplay()
    {
        if (codeDisplayText != null)
            codeDisplayText.text = inputCode;
    }

    private IEnumerator ResetFailVisual()
    {
        yield return new WaitForSeconds(1.5f);
        if (Red != null) Red.SetActive(false);
    }

    // 👇 금고 앞에서 호출 (E키 같은 걸로)
    public void OpenVaultUI()
    {
        vaultChecker = true;
        if (vaultUI != null) vaultUI.SetActive(true);
        if (ps != null) ps.objectOnOff = true;
        if (pm != null) pm.objectOnOff = true;
    }

    public void CloseVaultUI()
    {
        vaultChecker = false;
        if (vaultUI != null) vaultUI.SetActive(false);
        if (ps != null) ps.objectOnOff = false;
        if (pm != null) pm.objectOnOff = false;
    }
}

