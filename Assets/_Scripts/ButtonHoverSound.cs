using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AYellowpaper.SerializedCollections; // If you're using SerializedDictionary

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string hoverSoundName; // Name of the hover sound in AudioManager
    private AudioManager audioManager; // Reference to AudioManager

    void Start()
    {
        audioManager = AudioManager.instance; // Get reference to AudioManager
    }

    // Called when pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.Play(hoverSoundName); // Play the hover sound
    }

    // Called when pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        audioManager.Stop(hoverSoundName); // Stop the hover sound
    }
}
