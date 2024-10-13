using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using UnityEngine.SceneManagement; // Required for scene management functions

public class EndGameOnTrigger : MonoBehaviour
{
    public GameObject winPanel; // Assign a UI panel to show the win message

    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Initially hide the win message
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            if (winPanel != null)
            {
                winPanel.SetActive(true); // Display the win panel
            }
            Invoke("QuitGame", 5.0f); // Quit the game after 5 seconds
        }
    }

    void QuitGame()
    {
        Application.Quit(); // Quit the game
                            // If you're testing in the editor, you may need to stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
