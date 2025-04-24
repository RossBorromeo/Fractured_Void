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
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "So the Spring flower, the Tulip is pink" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "And we have just found a Marigold the Summer flower" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "So, Spring, Summer, Autumn, Winter... Pink, Orange.." });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "And all I have to do is figure out which one is Autumn and Winter..." });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "And follow that exact order!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I'll write a hint in the Journal if you get stuck" });

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
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Oh wow, this is how I remember Gran's Garden, She's gone... How could I forget?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Sometimes things hurt too much to remember, It doesn't hurt as much now, right Ollie?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "No... Not like it used to, but why was this something that was kept from me?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I think I know someone who can answer that, right Rose?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I'm so sorry Oliver! You aren't hurt are you? " });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "He's right as rain ! Actually he's looking a little better than how he started" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "You went a little overboard, no?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I-I guess so, I did it to protect him though, We all are... " });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I didn't know it would get that way, I guess I just got carried away" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Who are you Rose? What do you mean you got carried away? " });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Why do I feel like I know you but can’t remember how?" });
                
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "You made me long before that day, but when we came back to Ma's House after the funeral, I had to take over" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "She was hurting you, she LET other people hurt you!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I had to protect you!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "You did what any of us would have done" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "You're like Mabel, right?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "Yes, we are part of you, but we are all, Um, different... " });
              
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "You look kind of like Gran's favourite porcelain doll" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "Yes, Hehe, You used to call her Rose Princess. " });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I...Uh I know it was wrong of me to keep you from your memory of Gran" });
               
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "Maybe you were ready to know about everything for a long time, and I just didn't want to believe it." });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "Listen Ol, this place isn't right, it's all broken, and there are some things I, we, can't control anymore." });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "Some of the Others may be a little harder to convince, but I believe you're ready now." });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "You have to find all of them and get them to show you everything, to get out of here" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I wont run away from you again Ol, I promise, you'll be able to open all the doors now" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Thank you Rose, I understand why you did it" });
                dialogueLines.Add(new DialogueLine { speakerName = "Rose", text = "I'll always be here for you" });
            }

            else if (areaName == "AfterRoseReveal")
            {
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "I'm proud of you Ollie!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "How many? How many of us are there?" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "There are many rooms in the house Ollie and each one must be filled" });
                dialogueLines.Add(new DialogueLine { speakerName = "Oliver", text = "Hmmph...Mabel!" });
                dialogueLines.Add(new DialogueLine { speakerName = "Mabel", text = "Anyways, you're ready to find the rest of us, OOOH, and Rose left you a little something look!" });
                
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


