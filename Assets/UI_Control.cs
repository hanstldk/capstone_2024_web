using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject before_ani;
    public GameObject after_ani;
    public GameObject about_us;
    public GameObject cyk_pro;
    public GameObject hsm_pro;
    public GameObject ksh_pro;
    public GameObject ksj_pro;

    public RectTransform targetUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ���� Ray ����
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Ray�� Collider�� �¾Ҵ��� Ȯ��
            {
                // Ŭ���� ������Ʈ
                after_ani.GetComponent<Animator>().SetBool("Start", true);
            }
        }


        Vector2 mousePos = Input.mousePosition;

        // RectTransform�� ���� ��ǥ��� ��ȯ
        Vector2 localMousePos = targetUI.InverseTransformPoint(mousePos);

        // UI ����� RectTransform ���� �ȿ� ���콺�� �ִ��� Ȯ��
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
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(2f);

        // ������Ʈ ��Ȱ��ȭ
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


}
