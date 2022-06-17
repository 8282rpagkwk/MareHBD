using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject railW;
    [SerializeField] private GameObject Aguide;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject story;
    [SerializeField] private GameObject GameClearUI;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject[] guideImg;
    [SerializeField] private Gift sample;
    [SerializeField] private GiftSpawner Spawner;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI AguideText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip welcome;
    [SerializeField] private AudioClip[] BGM;
    [SerializeField] private int index;


    private string[] stage1 = {
        "해룡택배 물류센터에 어서와요. 반가워용!",
        "저번에 용용이들이 생일에 맞춰 선물을 받을 수 있도록 생일 룰렛을 돌렸었는데용.",
        "지금 막 생일 룰렛에 당첨된 용용이들에게 보낼 선물이 도착했어용.",
        "이제, 선물이 용용이들에게 무사히 도착할 수 있도록 분류작업을 진행할 거에용.",
        "분류방법은 A S D로 선물에 표시된 버튼을 빨간 바에 맞춰 누르면 자동으로 분류돼용.",
        "하지만! 분류에 실패하면 체력이 줄어든다는 점 잊지마세용.",
        "한번 해볼까용?",
        "어때요? 어렵지 않죠?",
        "이렇게 총 50개를 분류하면 오늘 업무는 끝이에용.",
        "이제 시작해볼까용?"
    };

    private string[] stage2 = {
        "미안해용.. 선물 중에 늦게 도착한 선물이 있어서 더 분류해야 될 것 같아용..",
        "선물을 더 빨리 분류할 수 있게 레일을 하나 더 추가했어용.",
        "이제는 A S D 말고도 W로도 선물을 분류해야해용.",
        "마저 분류해볼까용?"
    };

    private string[] stage3 = {
        "우리가 분류한 선물들을 확인 해보니까 불량품이 많다는 얘기가 있었어용.",
        "불량품을 발견하면 Space를 눌러 천마펀치로 불량품을 날려버릴 수 있어용.",
        "이제 분류를 끝내러 가볼까용?"
    };

    private List<string[]> storyList = new List<string[]>();

    private void Awake()
    {
        Spawner.enabled = false;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        init();
    }

    void init()
    {
        storyList.Add(stage1);
        storyList.Add(stage2);
        storyList.Add(stage3);

        railW.SetActive(false);
        if (GameManager.stage != 1)
        {
            railW.SetActive(true);
        }
        text.text = (storyList[GameManager.stage - 1])[0];
        Aguide.SetActive(false);
        AguideText.gameObject.SetActive(false);
        cursor.SetActive(false);
        story.SetActive(true);
        if (GameManager.stage == 1) audioSource.PlayOneShot(welcome);
        audioSource.clip = BGM[GameManager.stage - 1];
        audioSource.Play();
        foreach (GameObject g in guideImg) g.SetActive(false);
        GameManager.Instance.setClearUI(GameClearUI);
        GameManager.Instance.setOverUI(GameOverUI);
    }

    public void next()
    {
        if(index >= (storyList[GameManager.stage - 1]).Length - 1)
        {
            AguideText.text = "   를 눌러 게임시작";
        }
        Aguide.SetActive(true);
        AguideText.gameObject.SetActive(true);
        cursor.SetActive(true);
        StartCoroutine(checkNext());
    }

    IEnumerator checkNext()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(index >= (storyList[GameManager.stage - 1]).Length - 1)
                {
                    story.SetActive(false);
                    Spawner.enabled = true;
                }
                else
                {
                    index++;
                    Aguide.SetActive(false);
                    AguideText.gameObject.SetActive(false);
                    cursor.SetActive(false);
                    foreach (GameObject g in guideImg)
                        if(g.activeSelf)
                            g.SetActive(false);
                    text.text = (storyList[GameManager.stage - 1])[index];
                    switch (GameManager.stage)
                    {
                        case 1:
                            if(index == 4)
                            {
                                guideImg[GameManager.stage - 1].SetActive(true);
                            }
                            else if(index == 7)
                            {
                                story.SetActive(false);
                                sample.gameObject.SetActive(true);
                                StartCoroutine(checkSample());
                            }
                            break;
                        case 2:
                            if (index == 2)
                            {
                                guideImg[GameManager.stage - 1].SetActive(true);
                            }
                            break;
                        case 3:
                            if (index == 1)
                            {
                                guideImg[GameManager.stage - 1].SetActive(true);
                            }
                            break;
                    }
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator checkSample()
    {
        while (true)
        {
            if (!sample.gameObject.activeSelf)
            {
                if (sample.getSuccess())
                {
                    story.SetActive(true);
                    break;
                }
                sample.gameObject.SetActive(true);
            }
            yield return null;
        }
    }

    public void restart()
    {
        GameManager.Instance.Restart();
    }

    public void toStageScene()
    {
        GameManager.Instance.ToStageScene();
    }
}
