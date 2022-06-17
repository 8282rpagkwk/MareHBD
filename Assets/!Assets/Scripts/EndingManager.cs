using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject Aguide;
    [SerializeField] private GameObject VideoManager;
    [SerializeField] private GameObject EndingObject;
    [SerializeField] private GameObject TestCredit;
    [SerializeField] private Animator lightAnim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip Love;
    [SerializeField] private AudioClip Ending;
    [SerializeField] private int index = 0;
    [SerializeField] private bool isTest = false;
    private string[] endingText = {
        "�������� ��Ȯ�� �з��� �ż��� ��� ����\n������ ������ ���� ����̵���",
        "6�� 8��, ������ ���� ��ۿ� ã�ƿ� �����λ縦 ���߰�",
        "�������� ����̵�� �Բ� �ູ�� ���Ϲ���� �̾��.",
        "��, ���� �����԰� ����̵��� �Բ��ϴ�\nù��° ���� �ʸ� �Ҿ���?",
        "�ϳ�, ��, ��!",
        "��~",
        "���������ؿ� ������!"
    };



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        text.text = "";
        cursor.SetActive(false);
        Aguide.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);
        audioSource.Play();
        yield return new WaitForSecondsRealtime(3.0f);
        text.text = endingText[0];
    }

    public void next()
    {
        if(!Aguide.activeSelf) Aguide.SetActive(true);
        cursor.SetActive(true);
        StartCoroutine(checkNext());
    }

    public void SetBGM()
    {
        audioSource.clip = Ending;
        audioSource.loop = true;
    }

    IEnumerator checkNext()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (index >= endingText.Length - 1)
                {
                    text.gameObject.SetActive(false);
                    cursor.SetActive(false);
                    Aguide.SetActive(false);
                    EndingObject.SetActive(false);
                    if (isTest)
                    {
                        TestCredit.SetActive(true);
                    }
                    else
                    {
                        audioSource.Stop();
                        VideoManager.SetActive(true);
                    }
                    break;
                }
                else
                {
                    index++;
                    text.text = endingText[index];
                    cursor.SetActive(false);
                    if (index == 5)
                    {
                        lightAnim.SetTrigger("LightOff");
                    }
                    else if (index == 6)
                    {
                        audioSource.clip = Love;
                        audioSource.loop = false;
                        audioSource.Play();
                    }
                    break;
                }
            }
            yield return null;
        }
    }
}
