using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerSwapEffect : MonoBehaviour
{
    [Header("Objects")]
    public GameObject rose;
    public GameObject thornSword;

    [Header("Flash")]
    public Image flashImage;
    public float flashInTime = 0.1f;
    public float flashOutTime = 0.2f;

    [Header("Delay")]
    public float delayBeforeFlash = 1f;

    [Header("Player")]
    public GameObject player;  // Assign your player here
    private MonoBehaviour playerMovementScript;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (player == null)
                player = other.gameObject;

            // Automatically find the movement script (replace "PlayerMovement" with your script name if needed)
            playerMovementScript = player.GetComponent<PlayerMovement>();

            StartCoroutine(HandleEffectSequence());
        }
    }

    private IEnumerator HandleEffectSequence()
    {
        yield return new WaitForSeconds(delayBeforeFlash);

        if (flashImage == null)
        {
            Debug.LogWarning("Flash image not assigned!");
            yield break;
        }

        // Disable player movement
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        flashImage.color = new Color(1, 1, 1, 0);
        flashImage.gameObject.SetActive(true);

        // Fade in to white
        float timer = 0f;
        while (timer < flashInTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / flashInTime);
            flashImage.color = new Color(1, 1, 1, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        flashImage.color = new Color(1, 1, 1, 1);

        // Swap objects while screen is white
        if (rose != null) rose.SetActive(false);
        if (thornSword != null) thornSword.SetActive(true);

        // Fade out
        timer = 0f;
        while (timer < flashOutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / flashOutTime);
            flashImage.color = new Color(1, 1, 1, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        flashImage.color = new Color(1, 1, 1, 0);
        flashImage.gameObject.SetActive(false);

        // Re-enable player movement
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }
}
