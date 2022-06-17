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
        "�ط��ù� �������Ϳ� ��Ϳ�. �ݰ�����!",
        "������ ����̵��� ���Ͽ� ���� ������ ���� �� �ֵ��� ���� �귿�� ���Ⱦ��µ���.",
        "���� �� ���� �귿�� ��÷�� ����̵鿡�� ���� ������ �����߾��.",
        "����, ������ ����̵鿡�� ������ ������ �� �ֵ��� �з��۾��� ������ �ſ���.",
        "�з������ A S D�� ������ ǥ�õ� ��ư�� ���� �ٿ� ���� ������ �ڵ����� �з��ſ�.",
        "������! �з��� �����ϸ� ü���� �پ��ٴ� �� ����������.",
        "�ѹ� �غ����?",
        "���? ����� ����?",
        "�̷��� �� 50���� �з��ϸ� ���� ������ ���̿���.",
        "���� �����غ����?"
    };

    private string[] stage2 = {
        "�̾��ؿ�.. ���� �߿� �ʰ� ������ ������ �־ �� �з��ؾ� �� �� ���ƿ�..",
        "������ �� ���� �з��� �� �ְ� ������ �ϳ� �� �߰��߾��.",
        "������ A S D ���� W�ε� ������ �з��ؾ��ؿ�.",
        "���� �з��غ����?"
    };

    private string[] stage3 = {
        "�츮�� �з��� �������� Ȯ�� �غ��ϱ� �ҷ�ǰ�� ���ٴ� ��Ⱑ �־����.",
        "�ҷ�ǰ�� �߰��ϸ� Space�� ���� õ����ġ�� �ҷ�ǰ�� �������� �� �־��.",
        "���� �з��� ������ �������?"
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
            AguideText.text = "   �� ���� ���ӽ���";
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
