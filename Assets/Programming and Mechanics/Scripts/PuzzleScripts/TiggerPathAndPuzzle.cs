using UnityEngine;

public class TriggerPathAndPuzzle : MonoBehaviour
{
    public GameObject PathToSunFlower;  // Assign in Inspector
    public GameObject Puzzle2;          // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            if (PathToSunFlower != null)
                PathToSunFlower.SetActive(false);

            if (Puzzle2 != null)
                Puzzle2.SetActive(true);
        }
    }
}
