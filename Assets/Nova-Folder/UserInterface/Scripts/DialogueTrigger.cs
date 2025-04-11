using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // Oliver /Mabel/ Rose
    public string text;  // the dialogue text
}

public class DialogueTrigger : MonoBehaviour
{
    public string areaName; //  Bedroom, Corridor, Puzzle1.....
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

    private bool hasTriggered = false; // prevents repeating the same dialogue on re-entry
    // contains the lines of the game organised by area name 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !hasTriggered)
        {
            dialogueLines .Clear();// clears lines prevents lines going again
            if (areaName == "Bedroom")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "It was open the whole time you know " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Who are you? Where are we?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Oh... you don’t remember me do you?  " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I have always been with you, ever since that day... " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Mabel?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "See! I knew you’d remember! I knew you were ready. " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "What is this place? I have so many questions. What happened here?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "I feel so… wrong" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I know. This is a lot to take in." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "We are in your bedroom, or at least a version of it." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I promise to answer more questions but for right now lets get you out of here." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Make sure to check your Journal if you are ever confused" });

            }
            else if (areaName == "Corridor")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "WHAT! Mabel Where are we?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "There was an accident and now we are stuck here, in this mind of yours" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "We have to get you back, but we need to gather all of The Others first" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "They don’t think you’re ready, but I know you can do it." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "What Others? Mabel this isn’t making any sense. Why can’t I remember any of that?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "The others have your memories. They don’t want you to go through them again" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "But, if we are going to get out of here, there are things you must confront" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Go through them again? Go through what?!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "They are not my stories to share, but I’m here for you now and always." });


            }
            else if (areaName == "RoseGardenStart")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "A garden? Why does it look so familiar? " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Everything here is part of your memory, we've been here before." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "This is Gran’s. Her garden, I used to come here all the time to get away... " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "It looks odd though, it's overgrown, like no one has been here for a long time." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Gran would never let it get this way. What happened? Where is she? We have to find her" });


            }
            else if (areaName == "RoseSighting")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "That’s Rose, she controls this garden now.  " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Do you remember her?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "No... I dont think so... " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "She has some of your memories too, if anyone knows what happened here its her. " });


            }
            else if (areaName == "PillarSighting")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "I don’t remember this being here before!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "It definitely wasn’t, Rose has been busy. What could these symbols mean?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "They're seasons, I remember…  " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "I remember Gran used to grow different flowers in different seasons." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I never did pay attention to Gran's Botany lessons. Do you see any here?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Tulips. They were her favourite Spring flower.   " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Okay, Let’s find them.  " });
            }

            else if (areaName == "Puzzle1")
            {

                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "That's a long way up! ." });
            }
            else if (areaName == "Puzzle1.2")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Oliver! The vines! CLIMB NOW!" });
            }
            else if (areaName == "Puzzle2")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "This seems easy enough." });
            }
            else if (areaName == "Puzzle2.1")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Is it just me or is this walkway getting longer?" });
            }
            else if (areaName == "Puzzle2.2.1")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Well that did not work..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Maybe the portals follow a pattern, like the seasons..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "But the seasons dont have assigned colours!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "No... But the flowers OF the seasons do." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "So the Tulip is pink" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "And we have just found a Marigold the Summer flower" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "So all I have to do is figure out which one is Autumn and Winter..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "And follow that exact order!" });
            }
            else if (areaName == "Puzzle2.3")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Ehhhh, I don’t remember it looking like this... " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Oh perfect! A portal straight back. What could possibly go wrong" });
            }
            else if (areaName == "Puzzle3")
            {

                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Oh no I'm gonna be sick..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "So dizzyyy! Watch your step Oliver" });

            }

            else if (areaName == "RoseReveal")
            {

                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "" });

            }

            StartCoroutine(StartDialogueAfterDelay(0f));

        }
        
    }
    private IEnumerator StartDialogueAfterDelay(float delay)
    {
       //Debug.Log("Starting dialogue in " + delay + " seconds...");
        yield return new WaitForSeconds(delay);
         // pauses the game
       
        if (!hasTriggered)
        {
            Time.timeScale = 0f;
            Debug.Log("Starting dialogue for area: " + areaName);
            DialogueManager.Instance.StartDialogue(dialogueLines, ResumeGame);// function that resumes game after dialogue
            hasTriggered = true;
        }
        else
        {
            Debug.Log("Dialogue already triggered, skipping...");
        }
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f; // resumes the game
    }

}


