//Elizabeth Tuzhilina
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

    // Loads Bedroom
    public void LoadBedroom()
    {
        SceneManager.LoadScene(1);
    }

    // Loads Rose Garden
    public void LoadGarden()
    {
        SceneManager.LoadScene(2);
    }

    // Loads Menu  
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
