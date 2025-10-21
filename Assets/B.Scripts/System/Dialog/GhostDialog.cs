using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostDialog : MonoBehaviour
{
    [Header("UI 관련")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public DialogLine[] lines;
    public GameObject Image;
    public float time = 2f;

    private int currentLine = 0;

    public bool isSelect;
    private bool isDialogActive = false;
    private bool isOnlyOne = false;
    private bool isTrigger = false;

    private PlayerMovement pm;
    private PlayerSkill ps;

    void Start()
    {
        ps = FindObjectOfType<PlayerSkill>();
        pm = FindObjectOfType<PlayerMovement>();

        dialogPanel.SetActive(false);
    }

    void Update()
    {
        // E 키로 대화 시작
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && isOnlyOne == false && !isDialogActive)
        {
            StartDialog();
        }

        // 엔터 입력 시 대사 진행
        if (isDialogActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            NextLine();
        }
    }

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        currentLine = 0;
        isDialogActive = true;

        // 플레이어 조작 막기
        ps.objectOnOff = true;
        pm.objectOnOff = true;

        ShowLine();
    }

    private void ShowLine()
    {
        nameText.text = lines[currentLine].speakerName;
        dialogText.text = lines[currentLine].text;
    }

    private void NextLine()
    {
        currentLine++;
        if (currentLine < lines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        isDialogActive = false;
        isOnlyOne = true;
        isSelect = true;

        // 플레이어 조작 다시 켜주기
        ps.objectOnOff = false;
        pm.objectOnOff = false;
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
