using UnityEngine;

public class RunAwayTrigger : MonoBehaviour
{
    [Header("Character Settings")]
    public GameObject character;               // Rose (already in scene)
    public Transform runTarget;               // Where she runs to
    public float runSpeed = 5f;
    public float removeDistance = 0.1f;

    [Header("Animation")]
    public string runParam = "isRunning";     // Animator bool param for running
    private Animator animator;

    [Header("Dialogue")]
    public GameObject dialogueBox;
    public float dialogueDuration = 2f;

    [Header("Player Reference")]
    [SerializeField] private Transform playerTransform;
    private PlayerMovement playerMovement;

    private bool hasTriggered = false;
    private bool isRunning = false;

    void Start()
    {
        if (character != null)
            animator = character.GetComponent<Animator>();

        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (playerTransform != null)
        {
            playerMovement = playerTransform.GetComponent<PlayerMovement>();
            if (playerMovement == null)
                Debug.LogError("[RunAwayTrigger] PlayerMovement not found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (dialogueBox != null)
                dialogueBox.SetActive(true);

            if (playerMovement != null)
                playerMovement.SetMovementEnabled(false); //  Freeze player

            Invoke(nameof(StartRunning), dialogueDuration); // Wait, then run
        }
    }

    void StartRunning()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        // Flip Rose sprite to face left
        Vector3 scale = character.transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        character.transform.localScale = scale;

        // Run animation
        if (animator != null)
            animator.SetBool(runParam, true);

        isRunning = true;

        //  Re-enable player movement
        if (playerMovement != null)
            playerMovement.SetMovementEnabled(true);
    }

    void Update()
    {
        if (isRunning && character != null && runTarget != null)
        {
            character.transform.position = Vector3.MoveTowards(
                character.transform.position,
                runTarget.position,
                runSpeed * Time.deltaTime
            );

            if (Vector3.Distance(character.transform.position, runTarget.position) <= removeDistance)
            {
                Destroy(character); // Or SetActive(false) if you prefer
                isRunning = false;
            }
        }
    }
}
