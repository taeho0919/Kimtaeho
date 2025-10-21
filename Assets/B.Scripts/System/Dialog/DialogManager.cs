using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogLine
{
    public string speakerName;   // 화자 이름 (주인공, 귀신 등)
    [TextArea] public string text; // 대사 내용
}
public class DialogManager : MonoBehaviour
{
    [Header("UI 관련")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public DialogLine[] lines;

    private bool isEnd=false;
    private int currentLine = 0;

    private PlayerMovement pm;
    private PlayerSkill ps;

    void Start()
    {
        ps = FindObjectOfType<PlayerSkill>();
        pm = FindObjectOfType<PlayerMovement>();
        StartDialog();
    }

    void Update()
    {
        if (isEnd == false)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                 NextLine();
               }
        }
        // 엔터 입력 시 진행
        
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

    private void StartDialog()
    {
        dialogPanel.SetActive(true);
        currentLine = 0;
        ShowLine();
        ps.objectOnOff = true;
        pm.objectOnOff = true;
    }

    private void EndDialog()
    {
        // 대화 종료 시 한 번만 풀기
        ps.objectOnOff = false;
        pm.objectOnOff = false;
        isEnd = true;
        dialogPanel.SetActive(false);

    }
}

