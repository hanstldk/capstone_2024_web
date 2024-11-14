using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class anim_Control : MonoBehaviour
{
    public Animator animator;         // �ִϸ����� ������Ʈ
    
    private bool isAnimationPlaying = false;  // �ִϸ��̼��� ��� ������ ����




    void Start()
    {
        // Animator ������Ʈ�� �����ɴϴ�.
        animator = GetComponent<Animator>();

        // �ִϸ��̼��� ó������ ���� �����ϴ�.
        animator.speed = 0;
    }

    void Update()
    {
        // ���콺 �� ��ũ�� �ٿ��� ����
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // ��ũ�� �ٿ�
        {
            if (!isAnimationPlaying)
            {
                // �ִϸ��̼��� �����մϴ�.
                animator.speed = 1;  // �ִϸ��̼� �ӵ��� 1�� �����Ͽ� ���
                isAnimationPlaying = true;
                
            }
  
        }
    }

    public void Activate() 
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }

    public void StartScrollAnimation()
    {
        // �ڷ�ƾ ����
        animator.enabled = false;
        StartCoroutine(WaitForScrollInput());
    }

    // ��ũ�� �ٿ� �Է��� ��ٸ��� �ڷ�ƾ �Լ�
    private IEnumerator WaitForScrollInput()
    {
        // ��ũ�� �ٿ��� �Էµ� ������ ��ٸ�
        while (Input.GetAxis("Mouse ScrollWheel") >= 0)
        {
            yield return null;  // �� ������ ���
        }

        // ��ũ�� �ٿ��� �����Ǹ� �ִϸ��̼� ���
        animator.enabled = true;
        isAnimationPlaying = true;

        // �ִϸ��̼��� ���� �� ���ߵ��� ����
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("StopAnimation", animationDuration);
    }

    private void StopAnimation()
    {
        animator.enabled = false;  // �ִϸ��̼� ����
        isAnimationPlaying = false;  // ���� �ʱ�ȭ
    }


    public float fadeDuration = 2.0f; // ������� �� �ɸ��� �ð� (��)

    // ���� �帴������ ������� �Լ�
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutSpecificMaterial());
    }

    private IEnumerator FadeOutSpecificMaterial()
    {
        // �� ������Ʈ�� ��� ��Ƽ������ �������� 2�� ���͸��� ����
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        if (materials.Length < 3)
        {
            Debug.LogWarning("�� ������Ʈ�� 3���� ��Ƽ������ ������ ���� �ʽ��ϴ�.");
            yield break;
        }

        // 2��(�� ��°) ��Ƽ������ Fade ���� ����
        Material targetMaterial = materials[1];
        targetMaterial.SetFloat("_Mode", 2); // 2 = Fade
        targetMaterial.SetOverrideTag("RenderType", "Transparent");
        targetMaterial.EnableKeyword("_ALPHABLEND_ON");
        targetMaterial.renderQueue = 3000;

        // ���̵� �ƿ� ����
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // Alpha ���� 1���� 0���� ����

            // 2�� ��Ƽ������ ���� ����
            Color color = targetMaterial.color;
            color.a = alpha;
            targetMaterial.color = color;

            yield return null; // ���� �����ӱ��� ���
        }

        // ���������� ������ �����ϰ� ����
        Color finalColor = targetMaterial.color;
        finalColor.a = 0f;
        targetMaterial.color = finalColor;
    }
}