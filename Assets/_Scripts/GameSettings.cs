using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public AudioManager audioManager;
    //public Slider vfxVolumeSlider;

    public Toggle fullscreenToggle;

    private void Start()
    {
        // Carica le impostazioni salvate
        //LoadSettings();
    }

    public void SaveSettings()
    {
        // Salva le impostazioni
        PlayerPrefs.SetFloat("MusicVolume", masterVolumeSlider.value);
        //PlayerPrefs.SetFloat("VFXVolume", vfxVolumeSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        audioManager.SetMasterVolume(masterVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        // Carica le impostazioni salvate o impostazioni predefinite se non esistono
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioManager.SetMasterVolume(masterVolumeSlider.value);
        //vfxVolumeSlider.value = PlayerPrefs.GetFloat("VFXVolume", 0.5f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
    }

    public void SetScreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void ResetSettings()
    {
        // Reimposta le impostazioni predefinite
        masterVolumeSlider.value = 0.5f;
        //vfxVolumeSlider.value = 0.5f;
        fullscreenToggle.isOn = true;
        SaveSettings();
    }
}
