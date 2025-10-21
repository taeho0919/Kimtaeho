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
        // UI�� Ŭ�� ���� ���� �� �ֵ��� ����
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // ���� �� ���̵� �� ���� (���� ȭ�� �� ���� ȭ��)
        StartCoroutine(FadeInRoutine());
    }

    // �ܺο��� ȣ��: ���̵� �ƿ� ���� �� �ݹ�(Action)���� ���� �۾�(��: �� ��ȯ) ����
    public void FadeOut(Action onComplete)
    {
        StartCoroutine(FadeOutRoutine(onComplete));
    }

    // �ܺ� �Ǵ� ���ο��� ȣ��: ���̵� �� ���� (���� ȭ�� �� ���� ȭ��)
    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    // ���̵� �ƿ� �ڷ�ƾ (���� �� ������)
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

    // ���̵� �� �ڷ�ƾ (������ �� ����)
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
