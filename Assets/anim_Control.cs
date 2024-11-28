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
        animator.speed = 0f; // 애니메이션을 시작할 때 멈춘 상태로 설정
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
        // 스크롤 다운 (정방향 재생)
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (!isPlaying || isReversing) // 정방향으로 재생 시작
            {
                animator.speed = 1f; // 애니메이션 속도를 정방향으로 설정
                animator.Play(aniname, 0, animationTime); // 저장된 시간부터 애니메이션 시작
                isPlaying = true;
                isReversing = false; // 역재생이 아닌 정방향 재생
                
            }
        }

        // 스크롤 업 (역방향 재생)
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (isPlaying && !isReversing) // 역방향으로 재생 시작
            {
                // 현재 애니메이션 진행 상태에서 normalizedTime을 가져옴
                animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                isReversing = true;
            }
        }

        // 역재생을 위한 시간 제어
        if (isReversing)
        {
            // normalizedTime을 감소시켜 역재생을 구현
            animationTime -= Time.deltaTime / animator.GetCurrentAnimatorStateInfo(0).length;
            if (animationTime <= 0f)
            {
                animator.speed = 0f; // 애니메이션이 끝날 때 멈춤
                isPlaying = false; // 역재생이 끝나면 멈춤
            }
            else
            {
                animator.Play(aniname, 0, animationTime); // 역재생
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
            Debug.LogWarning("이 오브젝트에는 MeshRenderer가 없습니다.");
            return; // 실행 종료
        }
        // 애니메이션의 특정 시점에 맞춰 이벤트 함수 호출
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


    public float fadeDuration = 2.0f; // 사라지는 데 걸리는 시간 (초)

    // 점점 흐릿해지며 사라지는 함수
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
            Debug.LogWarning("이 오브젝트는 3개의 머티리얼을 가지고 있지 않습니다.");
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

        // 최종적으로 완전히 불투명하게 설정
        Color finalColor = targetMaterial.color;
        finalColor.a = 1f;
        targetMaterial.color = finalColor;
    }

    private IEnumerator FadeOutSpecificMaterial()
    {
        // 이 오브젝트의 모든 머티리얼을 가져오고 2번 매터리얼만 참조
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        if (materials.Length < 3)
        {
            Debug.LogWarning("이 오브젝트는 3개의 머티리얼을 가지고 있지 않습니다.");
            yield break;
        }

        // 2번(세 번째) 머티리얼을 Fade 모드로 설정
        Material targetMaterial = materials[1];
        targetMaterial.SetFloat("_Mode", 2); // 2 = Fade
        targetMaterial.SetOverrideTag("RenderType", "Transparent");
        targetMaterial.EnableKeyword("_ALPHABLEND_ON");
        targetMaterial.renderQueue = 3000;

        // 페이드 아웃 시작
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // Alpha 값을 1에서 0으로 줄임

            // 2번 머티리얼의 투명도 적용
            Color color = targetMaterial.color;
            color.a = alpha;
            targetMaterial.color = color;

            yield return null; // 다음 프레임까지 대기
        }

        // 최종적으로 완전히 투명하게 설정
        Color finalColor = targetMaterial.color;
        finalColor.a = 0f;
        targetMaterial.color = finalColor;
    }
}