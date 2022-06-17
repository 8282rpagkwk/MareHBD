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
        "마레님의 정확한 분류와 신속한 배송 덕에\n무사히 선물을 받은 용용이들은",
        "6월 8일, 마레님 생일 방송에 찾아와 감사인사를 전했고",
        "마레님은 용용이들과 함께 행복한 생일방송을 이어갔다.",
        "자, 이제 마레님과 용용이들이 함께하는\n첫번째 생일 초를 불어볼까요?",
        "하나, 둘, 셋!",
        "후~",
        "생일축하해요 마레님!"
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
