using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_Control : MonoBehaviour
{
    private Animator animator;
    private bool isPlaying = false;
    private float animationTime = 0f;
    private bool isReversing = false;
    public string aniname;
    MeshRenderer meshRenderer;
    public MeshCollider[] colliders;

    void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
        animator.speed = 0f; // �ִϸ��̼��� ������ �� ���� ���·� ����
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    public void col_active()
    {
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    void cd_ef_start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    void Update()
    {
        // ��ũ�� �ٿ� (������ ���)
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (!isPlaying || isReversing) // ���������� ��� ����
            {
                animator.speed = 1f; // �ִϸ��̼� �ӵ��� ���������� ����
                animator.Play(aniname, 0, animationTime); // ����� �ð����� �ִϸ��̼� ����
                isPlaying = true;
                isReversing = false; // ������� �ƴ� ������ ���
                
            }
        }

        // ��ũ�� �� (������ ���)
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (isPlaying && !isReversing) // ���������� ��� ����
            {
                // ���� �ִϸ��̼� ���� ���¿��� normalizedTime�� ������
                animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                isReversing = true;
            }
        }

        // ������� ���� �ð� ����
        if (isReversing)
        {
            // normalizedTime�� ���ҽ��� ������� ����
            animationTime -= Time.deltaTime / animator.GetCurrentAnimatorStateInfo(0).length;
            if (animationTime <= 0f)
            {
                animator.speed = 0f; // �ִϸ��̼��� ���� �� ����
                isPlaying = false; // ������� ������ ����
            }
            else
            {
                animator.Play(aniname, 0, animationTime); // �����
                CheckForReverseEvent(animationTime);
            }
        }
    }
    private void CheckForReverseEvent(float currentTime)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (currentTime < 0.25f && particleSystem != null)
        {
            particleSystem.Stop();
        }
        if (meshRenderer == null)
        {
            Debug.LogWarning("�� ������Ʈ���� MeshRenderer�� �����ϴ�.");
            return; // ���� ����
        }
        // �ִϸ��̼��� Ư�� ������ ���� �̺�Ʈ �Լ� ȣ��
        if (currentTime <= 0.34f && currentTime > 0.33f)
        {
            
            if (meshRenderer != null && gameObject.CompareTag("second"))
            {
                meshRenderer.enabled = false;
                Debug.Log("1111");
                foreach (var collider in colliders)
                {
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }
                }
            }
        }
        Material[] materials = meshRenderer.materials;
        if (currentTime <= 0.26f && currentTime > 0.25f&& materials.Length > 1 && materials[1] != null)
        {
            StartFadeIn();
        }
        
    }

  
    



    public void Activate() 
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }


    public float fadeDuration = 2.0f; // ������� �� �ɸ��� �ð� (��)

    // ���� �帴������ ������� �Լ�
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutSpecificMaterial());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeInSpecificMaterial());
    }

    private IEnumerator FadeInSpecificMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        if (materials.Length < 3)
        {
            Debug.LogWarning("�� ������Ʈ�� 3���� ��Ƽ������ ������ ���� �ʽ��ϴ�.");
            yield break;
        }

        Material targetMaterial = materials[1];
        
        targetMaterial.SetFloat("_Mode", 2); // 2 = Fade
        targetMaterial.SetOverrideTag("RenderType", "Transparent");
        targetMaterial.EnableKeyword("_ALPHABLEND_ON");
        targetMaterial.renderQueue = 3000;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            Color color = targetMaterial.color;
            color.a = alpha;
            targetMaterial.color = color;

            yield return null;
        }

        // ���������� ������ �������ϰ� ����
        Color finalColor = targetMaterial.color;
        finalColor.a = 1f;
        targetMaterial.color = finalColor;
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