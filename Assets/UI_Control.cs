using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    string cyk_url = "https://www.instagram.com/ykpf2018_23";
    string hsm_url = "https://www.youtube.com/@solmi19";
    string ksh_url = "https://www.youtube.com/@%EB%AA%BD%EC%8B%A4-m6h";
    string ksj_url = "https://savory-fish-bb8.notion.site/f4653d55d56c48e4a2e7b2c2bc969f70?pvs=4";


    public GameObject before_ani;
    public GameObject after_ani;
    public GameObject about_us;
    public GameObject cyk_pro;
    public GameObject hsm_pro;
    public GameObject ksh_pro;
    public GameObject ksj_pro;

    public GameObject cyk_port;
    public GameObject hsm_port;
    public GameObject ksh_port;




    public RectTransform targetUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 Ray 생성
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Ray가 Collider에 맞았는지 확인
            {
                // 클릭된 오브젝트
                after_ani.GetComponent<Animator>().SetBool("Start", true);
            }
        }


        Vector2 mousePos = Input.mousePosition;

        // RectTransform의 로컬 좌표계로 변환
        Vector2 localMousePos = targetUI.InverseTransformPoint(mousePos);

        // UI 요소의 RectTransform 영역 안에 마우스가 있는지 확인
        if (targetUI.rect.Contains(localMousePos))
        {
            after_ani.GetComponent<Animator>().SetBool("Up", true);
            after_ani.GetComponent<Animator>().SetBool("Down", false);
        }
        else
        {
            after_ani.GetComponent<Animator>().SetBool("Down", true);
            after_ani.GetComponent<Animator>().SetBool("Up", false);
        }


    }

    public void UI_Start()
    {
        before_ani.GetComponent<Animator>().SetBool("Start", true);
        StartCoroutine(DisableObjectAfterDelay());
    }


    IEnumerator DisableObjectAfterDelay()
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(2f);

        // 오브젝트 비활성화
        if (before_ani != null)
        {
            before_ani.SetActive(false);
        }
    }

    public void Go_Left_about()
    {
        about_us.GetComponent<Animator>().SetBool("left", true);
        about_us.GetComponent<Animator>().SetBool("right", false);
    }

    public void Go_Right_about()
    {
        about_us.GetComponent<Animator>().SetBool("right", true);
        about_us.GetComponent<Animator>().SetBool("left", false);
    }

    public void Go_Left_cyk()
    {
        cyk_port.GetComponent<Animator>().SetBool("left", true);
        cyk_port.GetComponent<Animator>().SetBool("right", false);
    }

    public void Go_Right_cyk()
    {
        cyk_port.GetComponent<Animator>().SetBool("right", true);
        cyk_port.GetComponent<Animator>().SetBool("left", false);
    }
    public void Go_Left_hsm()
    {
        hsm_port.GetComponent<Animator>().SetBool("left", true);
        hsm_port.GetComponent<Animator>().SetBool("right", false);
    }

    public void Go_Right_hsm()
    {
        hsm_port.GetComponent<Animator>().SetBool("right", true);
        hsm_port.GetComponent<Animator>().SetBool("left", false);
    }
    public void Go_Left_ksh()
    {
        ksh_port.GetComponent<Animator>().SetBool("left", true);
        ksh_port.GetComponent<Animator>().SetBool("right", false);
    }

    public void Go_Right_ksh()
    {
        ksh_port.GetComponent<Animator>().SetBool("right", true);
        ksh_port.GetComponent<Animator>().SetBool("left", false);
    }

    public void Go_Up_proflie()
    {
        cyk_pro.GetComponent<Animator>().SetBool("up", true);
        cyk_pro.GetComponent<Animator>().SetBool("down", false);
    }
    public void Go_Down_proflie()
    {
        cyk_pro.GetComponent<Animator>().SetBool("down", true);
        cyk_pro.GetComponent<Animator>().SetBool("up", false);
    }

    public void Go_Up_proflie_hsm()
    {
        hsm_pro.GetComponent<Animator>().SetBool("up", true);
        hsm_pro.GetComponent<Animator>().SetBool("down", false);
    }
    public void Go_Down_proflie_hsm()
    {
        hsm_pro.GetComponent<Animator>().SetBool("down", true);
        hsm_pro.GetComponent<Animator>().SetBool("up", false);
    }
    public void Go_Up_proflie_ksh()
    {
        ksh_pro.GetComponent<Animator>().SetBool("up", true);
        ksh_pro.GetComponent<Animator>().SetBool("down", false);
    }
    public void Go_Down_proflie_ksh()
    {
        ksh_pro.GetComponent<Animator>().SetBool("down", true);
        ksh_pro.GetComponent<Animator>().SetBool("up", false);
    }
    public void Go_Up_proflie_ksj()
    {
        ksj_pro.GetComponent<Animator>().SetBool("up", true);
        ksj_pro.GetComponent<Animator>().SetBool("down", false);
    }
    public void Go_Down_proflie_ksj()
    {
        ksj_pro.GetComponent<Animator>().SetBool("down", true);
        ksj_pro.GetComponent<Animator>().SetBool("up", false);
    }

    public void opne_cyk() 
    {
        Application.OpenURL(cyk_url);
    }

    public void opne_hsm()
    {
        Application.OpenURL(hsm_url);
    }

    public void opne_ksh()
    {
        Application.OpenURL(ksh_url);
    }

    public void opne_ksj()
    {
        Application.OpenURL(ksj_url);
    }


}
