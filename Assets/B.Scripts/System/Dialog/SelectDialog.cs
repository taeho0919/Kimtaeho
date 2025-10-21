using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class SelectDialogLine
{
    public string speakerName;   // 화자 이름 (주인공, 귀신 등)
    [TextArea] public string maintext;
    [TextArea] public string Ytext; // 대사 내용
    [TextArea] public string Ntext; // 대사 내용
}

public class SelectDialog : MonoBehaviour
{
    [Header("UI 관련")]
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public SelectDialogLine[] lines;
    public GameObject Image;
    public float time = 2f;
    public string GameOversceneName;
    public string EndSceneName;
    public FadeInOut BfadeManager;
    public FadeInOut WfadeManager;
    public AudioClip sound;

    private bool isTrigger = false;
    private int currentLine = 0;
    private bool isDialogActive = false;
    private bool isChoiceMade = false; // 선택 완료 여부

    private AudioSource audiosource;
    private BabyToy bt;
    private GhostDialog gd;
    private PlayerMovement pm;
    private PlayerSkill ps;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        bt = FindObjectOfType<BabyToy>(); 
        ps = FindObjectOfType<PlayerSkill>();
        pm = FindObjectOfType<PlayerMovement>();
        gd = FindObjectOfType<GhostDialog>();

        dialogPanel.SetActive(false);
    }

    void Update()
    {
        // 트리거 안에서 E를 눌러 대화 시작
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && gd.isSelect && !isDialogActive)
        {
            StartDialog();
        }

        if (isDialogActive)
        {
            // 아직 선택 안했으면 선택 입력 대기
            if (!isChoiceMade)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (bt.isToyUse == true)
                    {
                        dialogText.text = lines[currentLine].Ytext;
                        isChoiceMade = true;
                        SelectY();
                    }
                    
                    if (bt.isToyUse == false)
                    {
                        dialogText.text = lines[currentLine].Ntext;
                        isChoiceMade = true;
                        StartCoroutine(SelectN());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.N))
                {
                    dialogText.text = lines[currentLine].Ntext;
                    isChoiceMade = true;
                    StartCoroutine(HorrorImage());
                    StartCoroutine(SelectN());
                }
            }
            else
            {
                // 선택 후 Space를 누르면 다음 대사로
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    NextLine();
                }
            }
        }
    }

    private void ShowLine()
    {
        nameText.text = lines[currentLine].speakerName;
        dialogText.text = lines[currentLine].maintext;
        isChoiceMade = false;
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
    }

    private void EndDialog()
    {
        ps.objectOnOff = false;
        pm.objectOnOff = false;

        dialogPanel.SetActive(false);
        isDialogActive = false;
    }

    private void SelectY()
    {
        WfadeManager.fadeDuration = 1f;

        WfadeManager.FadeOut(() =>
        {
            SceneManager.LoadScene(EndSceneName);
        });
    }

    IEnumerator SelectN()
    {
        yield return new WaitForSeconds(2f);
        BfadeManager.fadeDuration = 3f;
        BfadeManager.FadeIn();
        SceneManager.LoadScene(GameOversceneName);
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

    IEnumerator HorrorImage()
    {
        dialogPanel.SetActive(false);
        Image.SetActive(true);         // 이미지 켜기
        audiosource.PlayOneShot(sound, 1);
        yield return new WaitForSeconds(time); // 2초 기다림
        Image.SetActive(false);        // 이미지 끄기
    }
}
