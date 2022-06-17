using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("시간")]
    public static bool isGameOver = false;

    [Header("플레이어")]
    [SerializeField] private int clearScore = 50;
    [SerializeField] private int setSpeed = 3;
    public static int MAX_HP = 100;
    public static float hp = 100;
    public static float score;
    public static float speed = 3;
    public static int combo;
    public static bool isFirst = true;

    [Header("UI")]
    [SerializeField] private GameObject uiGameOver;
    [SerializeField] private GameObject uiGameClear;
    [SerializeField] private int setStage;
    [SerializeField] private int setClearStage;
    public static int stage = 0;
    public static int clearStage = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip soundSuccess;
    [SerializeField] private AudioClip soundMiss;
    [SerializeField] private AudioClip soundFail;
    private AudioSource audioSource;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        speed = setSpeed;
        clearStage = PlayerPrefs.GetInt("clearStage", 0);
        //stage = setStage;
        //clearStage = setClearStage;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void Success()
    {
        combo++;
        hp += 2;
        if (hp > 100) hp = 100;
        //instance.audioSource.PlayOneShot(instance.soundSuccess);
        if (instance.isInfinityScene())
        {
            score += 1 + (combo * 0.1f);
        }
        else
        {
            score++;
        }

        if(!instance.isInfinityScene())
        {
            if(score >= instance.clearScore)
            {
                instance.GameClear();
            }
        }
    }

    public static void Miss()
    {
        instance.audioSource.PlayOneShot(instance.soundMiss);
        combo = 0;
        hp -= 3;
    }

    public static void Fail()
    {
        instance.audioSource.PlayOneShot(instance.soundFail);
        combo = 0;
        hp -= 10;
        if (hp <= 0)
        {
            instance.GameOver();
        }
    }

    public static void ResetGame()
    {
        combo = 0;
        score = 0;
        isGameOver = false;
        hp = MAX_HP;
    }

    void GameOver()
    {
        isGameOver = true;

        clearStage = stage - 1;
        PlayerPrefs.SetInt("clearStage", clearStage);

        uiGameOver.SetActive(true);
    }

    void GameClear()
    {
        isGameOver = true;

        StarUI star = uiGameClear.GetComponent<StarUI>();
        foreach (GameObject g in star.getStars()) g.SetActive(false);
        if (hp > 90)
        {
            star.ShowStar(2);
        }
        else if (hp >= 60)
        {
            star.ShowStar(1);
        }
        else
        {
            star.ShowStar(0);
        }
        if (stage == 3) star.showEndingBtn();

        clearStage = stage;
        PlayerPrefs.SetInt("clearStage", clearStage);

        uiGameClear.SetActive(true);
    }

    public void Restart()
    {
        ResetGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToStageScene()
    {
        ResetGame();
        stage = 0;
        if (clearStage == 3)
        {
            isFirst = true;
            SceneManager.LoadSceneAsync("EndingScene");
        }
        else
        {
            isFirst = false;
            SceneManager.LoadSceneAsync("TitleScene");
        }
    }

    bool isInfinityScene()
    {
        return SceneManager.GetActiveScene().name == "InfinityScene";
    }

    public void setClearUI(GameObject ui)
    {
        uiGameClear = ui;
    }
    public void setOverUI(GameObject ui)
    {
        uiGameOver = ui;
    }
}
