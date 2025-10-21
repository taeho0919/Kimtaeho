using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        // UI가 클릭 등을 막을 수 있도록 설정
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // 시작 시 페이드 인 실행 (검정 화면 → 투명 화면)
        StartCoroutine(FadeInRoutine());
    }

    // 외부에서 호출: 페이드 아웃 실행 후 콜백(Action)으로 다음 작업(예: 씬 전환) 수행
    public void FadeOut(Action onComplete)
    {
        StartCoroutine(FadeOutRoutine(onComplete));
    }

    // 외부 또는 내부에서 호출: 페이드 인 실행 (검정 화면 → 투명 화면)
    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    // 페이드 아웃 코루틴 (투명 → 불투명)
    private IEnumerator FadeOutRoutine(Action onComplete)
    {
        float time = 0f;

        canvasGroup.blocksRaycasts = true;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;

        onComplete?.Invoke();
    }

    // 페이드 인 코루틴 (불투명 → 투명)
    private IEnumerator FadeInRoutine()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;

        canvasGroup.blocksRaycasts = false;
    }


}
