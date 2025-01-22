using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the dialogue box
    public TextMeshProUGUI dialogueText; // Reference to the text component of the dialogue box
    public string[] dialogueLines; // Array of dialogue lines
    public int currentLine = 0; // Index of the current dialogue line

    public GameObject contbutton;
    public float wordSpeed;
    public bool PlayerIsClose;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerIsClose)
        {
            if(dialogueBox.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialogueBox.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogueLines[currentLine])
        {
            contbutton.SetActive(true);
        }
        else
        {
            contbutton.SetActive(false);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        currentLine = 0;
        dialogueBox.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contbutton.SetActive(false);
        if (currentLine < dialogueLines.Length - 1)
        {
            currentLine++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }


        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = true;
           
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsClose = false;
            zeroText();
        }


    }
}
