using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class anim_Control : MonoBehaviour
{
    public Animator animator;         // 애니메이터 컴포넌트
    
    private bool isAnimationPlaying = false;  // 애니메이션이 재생 중인지 여부




    void Start()
    {
        // Animator 컴포넌트를 가져옵니다.
        animator = GetComponent<Animator>();

        // 애니메이션을 처음에는 멈춰 놓습니다.
        animator.speed = 0;
    }

    void Update()
    {
        // 마우스 휠 스크롤 다운을 감지
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // 스크롤 다운
        {
            if (!isAnimationPlaying)
            {
                // 애니메이션을 시작합니다.
                animator.speed = 1;  // 애니메이션 속도를 1로 설정하여 재생
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
        // 코루틴 시작
        animator.enabled = false;
        StartCoroutine(WaitForScrollInput());
    }

    // 스크롤 다운 입력을 기다리는 코루틴 함수
    private IEnumerator WaitForScrollInput()
    {
        // 스크롤 다운이 입력될 때까지 기다림
        while (Input.GetAxis("Mouse ScrollWheel") >= 0)
        {
            yield return null;  // 매 프레임 대기
        }

        // 스크롤 다운이 감지되면 애니메이션 재생
        animator.enabled = true;
        isAnimationPlaying = true;

        // 애니메이션이 끝난 후 멈추도록 예약
        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("StopAnimation", animationDuration);
    }

    private void StopAnimation()
    {
        animator.enabled = false;  // 애니메이션 멈춤
        isAnimationPlaying = false;  // 상태 초기화
    }


    public float fadeDuration = 2.0f; // 사라지는 데 걸리는 시간 (초)

    // 점점 흐릿해지며 사라지는 함수
    public void StartFadeOut()
    {
        StartCoroutine(FadeOutSpecificMaterial());
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