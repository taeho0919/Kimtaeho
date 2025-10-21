using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class TriggerDialogLine
{
    public string speakerName;   // ȭ�� �̸� (���ΰ�, �ͽ� ��)
    [TextArea] public string Lifetext; // ��� ����
    [TextArea] public string Dietext; // ��� ����
}
public class TriggerDialog : MonoBehaviour
{
    [Header("UI ����")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public TriggerDialogLine[] lines;

    private int currentLine = 0;
    private bool isDialogActive = false; // ��ȭ ���� ��
    private bool hasTriggered = false;   // �̹� Ʈ���� �ߵ��ߴ��� üũ

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

        // PlayerSkill�� ������Ʈ ���¿� ���� �ؽ�Ʈ ����
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
        hasTriggered = true; // �� �� �ߵ������� ���
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
        // �� �� �ߵ��� Ʈ���Ŵ� �ٽ� �ߵ����� ����
        if (!hasTriggered && other.CompareTag("Player"))
        {
            StartDialog();
        }
    }
}
