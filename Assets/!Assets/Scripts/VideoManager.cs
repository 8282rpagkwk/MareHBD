using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] videos;
    [SerializeField] private int index = 0;
    [SerializeField] private GameObject Credit;
    [SerializeField] private AudioSource endingSong;

    void Start()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }

    void FixedUpdate()
    {
        if (videoPlayer.time + 0.05f > videoPlayer.length)
        {
            if (index == videos.Length - 1)
            {
                videoPlayer.gameObject.SetActive(false);
                endingSong.gameObject.GetComponent<EndingManager>().SetBGM();
                endingSong.Play();
                Credit.SetActive(true);
            }
            else
            {
                index++;
                videoPlayer.clip = videos[index];
                videoPlayer.Play();
            }
        }
    }
}
