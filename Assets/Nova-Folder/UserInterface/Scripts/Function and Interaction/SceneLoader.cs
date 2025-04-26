//Elizabeth Tuzhilina
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{

    public VideoPlayer videoPlayer; // VideoPlayer to be assigned in inspector

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();// trying to find
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; //  get unity event
        }
       
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene(); // load the next scene when the video ends
    }

    // loads next scene in the build 
    public void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("Error no more scenes" );
            }
        }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex); // reloads the current scene
    }

    // Loads Bedroom
    public void LoadBedroom()
    {
        SceneManager.LoadScene(1);
    }

    // Loads Opening
    public void LoadCutscen()
    {
        SceneManager.LoadScene(2);
    }
    
    // Loads Rose Garden
    public void LoadGarden()
    {
        SceneManager.LoadScene(3);
    }
    // Loads Flashback
    public void LoadFlashCutscen()
    {
        SceneManager.LoadScene(4);
    }

    // Loads Menu  
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

}
