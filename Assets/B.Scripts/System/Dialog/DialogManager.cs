using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogLine
{
    public string speakerName;   // ȭ�� �̸� (���ΰ�, �ͽ� ��)
    [TextArea] public string text; // ��� ����
}
public class DialogManager : MonoBehaviour
{
    [Header("UI ����")]
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
        // ���� �Է� �� ����
        
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
        // ��ȭ ���� �� �� ���� Ǯ��
        ps.objectOnOff = false;
        pm.objectOnOff = false;
        isEnd = true;
        dialogPanel.SetActive(false);

    }
}

