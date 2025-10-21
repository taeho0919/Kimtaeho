using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class TriggerDialogLine
{
    public string speakerName;   // 화자 이름 (주인공, 귀신 등)
    [TextArea] public string Lifetext; // 대사 내용
    [TextArea] public string Dietext; // 대사 내용
}
public class TriggerDialog : MonoBehaviour
{
    [Header("UI 관련")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public TriggerDialogLine[] lines;

    private int currentLine = 0;
    private bool isDialogActive = false; // 대화 진행 중
    private bool hasTriggered = false;   // 이미 트리거 발동했는지 체크

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
        if (isDialogActive)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                NextLine();
            }
        }
    }

    private void ShowLine()
    {
        nameText.text = lines[currentLine].speakerName;

        // PlayerSkill의 오브젝트 상태에 따라 텍스트 선택
        if (ps.lifeObject.activeSelf)
        {
            dialogText.text = lines[currentLine].Lifetext;
        }    
        else if (ps.dieObject.activeSelf)
        {
            dialogText.text = lines[currentLine].Dietext;
        }
            
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

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        currentLine = 0;
        ShowLine();

        ps.objectOnOff = true;
        pm.objectOnOff = true;

        isDialogActive = true;
        hasTriggered = true; // 한 번 발동했음을 기록
    }

    private void EndDialog()
    {
        ps.objectOnOff = false;
        pm.objectOnOff = false;

        dialogPanel.SetActive(false);
        isDialogActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 한 번 발동한 트리거는 다시 발동하지 않음
        if (!hasTriggered && other.CompareTag("Player"))
        {
            StartDialog();
        }
    }
}
