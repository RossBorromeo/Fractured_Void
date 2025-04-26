using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThornSwordTrigger : MonoBehaviour
{
    public static ThornSwordTrigger Instance;
    public Canvas promptCanvas;
    public GameObject thornSword; // The thorn object 

    public string creditsScene = "FinalCredits";// exact name of scredits scene
    public float delayBeforeCredits = 2f; // waiting time before next scene
   
    private bool playerInTrigger = false;
    public bool pickedUpSword = false;

    private void Awake()
    {
        Instance = this; // set the instance because were referenceing itt
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger && !pickedUpSword && Input.GetKeyDown(KeyCode.E))
        {
            pickedUpSword = true;
           
            if (thornSword != null)
            {
                

                thornSword.SetActive(false);

                
                promptCanvas.gameObject.SetActive(false);

            }

            playerInTrigger = false; // preventing repeating activations
             //  COROUTINE TO START END CREDITS
             StartCoroutine(LoadAfterCredits());

        }
    }
    private IEnumerator LoadAfterCredits()
    {
        yield return new WaitForSeconds(delayBeforeCredits);
        SceneManager.LoadScene(creditsScene);
    }
}


