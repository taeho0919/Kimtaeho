using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public bool objectOnOff;

    [Header("오브젝트 관련")]
    public GameObject lifeObject;
    public GameObject dieObject;

    [Header("페이드 관련")]
    public FadeInOut fadeManager;

    [Header("쿨타임 관련")]
    public float skillCooldown = 5f;   // 스킬 쿨타임 (초)
    private float cooldownTimer = 0f;  // 현재 쿨타임 진행도
    private bool isCooldown = false;
    public GameObject isimage;
    public Image cooldownImage; // 쿨타임을 표시할 이미지 (fillAmount 1 → 0)

    public bool isOnskill;
    private int state = 0;

    void Start()
    {
         
        isimage.SetActive(false);
        lifeObject.SetActive(true);
        dieObject.SetActive(false);

        if (cooldownImage != null)
        {
             cooldownImage.fillAmount = 0f; // 시작 시 쿨타임 없음
        }
           
    }

    void Update()
    {
        // 스킬 발동
        if (Input.GetKeyDown(KeyCode.C) && !isCooldown && objectOnOff==false)
        {

            fadeManager.fadeDuration = 3f;
            fadeManager.FadeIn();
            StartCoroutine(SkillManager());

            // 쿨타임 시작
            StartCoroutine(StartCooldown());
        }

        // 쿨타임 UI 업데이트
        if (isCooldown && cooldownImage != null)
        {
            cooldownImage.fillAmount = cooldownTimer / skillCooldown;
        }
    }

    IEnumerator SkillManager()
    {
        
        state = (state + 1) % 2; // 0 또는 1로 순환

        if (state == 0)
        {
            isOnskill = false;
            lifeObject.SetActive(true);
            dieObject.SetActive(false);
        }
        else
        {
            isOnskill = true;
            lifeObject.SetActive(false);
            dieObject.SetActive(true);
        }

        yield return new WaitForSeconds(fadeManager.fadeDuration);
    }

    IEnumerator StartCooldown()
    {
        isimage.SetActive(true);
        isCooldown = true;
        cooldownTimer = skillCooldown;

        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        cooldownTimer = 0f;
        isCooldown = false;
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f; // 다 끝나면 게이지 초기화
        }
            
    }
}
