using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
 
public class VideoScene : MonoBehaviour
{
    VideoPlayer video;
    public string sceneName;
 
   void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }
 
     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);//the scene that you want to load after the video has ended.
    }
}