using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrueEnding : MonoBehaviour
{
    public Transform Mask;
    public GameObject Canvas;

    void Start()
    {
        StartCoroutine(StartAnims());
    }

    IEnumerator StartAnims()
    {
        Canvas.SetActive(false);
        yield return new WaitForSecondsRealtime(1.0f);
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(ShowCanvas());
    }

    IEnumerator ShowCanvas()
    {
        while (Mask.localScale.x < 29)
        {
            yield return new WaitForFixedUpdate();
        }
        Canvas.SetActive(true);
        StartCoroutine(CheckPressBtn());
    }

    IEnumerator CheckPressBtn()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadSceneAsync("TitleScene");
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
