using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    // Reference to the dialog panel that will display the text.
    public GameObject dialogPanel;
    // Reference to the Text UI element that will show "Hello world".
    public TextMeshProUGUI dialogText;

    // When the game starts, ensure the dialog is hidden.
    void Start()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    // When another collider enters the trigger.
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player.
        if (other.CompareTag("Player"))
        {
            if (dialogPanel != null && dialogText != null)
            {
                // Activate the dialog panel and set its text.
                dialogPanel.SetActive(true);
                dialogText.text = "Hello world";
            }
        }
    }

    // When the player leaves the trigger, hide the dialog.
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogPanel != null)
            {
                dialogPanel.SetActive(false);
            }
        }
    }
}
