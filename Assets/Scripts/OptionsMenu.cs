using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; //Allows access to sliders and other UIs
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider; // Allows access to the slider
    [SerializeField] private Slider SFXSlider; // Allows access to the slider
    
    // [SerializeField] private TextMeshProUGUI musicTextUI = null; // The number value next to the slider in the UI
    // [SerializeField] private TextMeshProUGUI SFXTextUI = null; // The number value next to the slider in the UI
    
    [SerializeField] private AudioMixer MyMixer;
    // [SerializeField] private AudioMixMode MixMode;

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        } else
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1.2f);
            SetMusicVolume();
        }
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadSFXVolume();
        } else
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 2.2f);
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        // musicTextUI.text = volume.ToString("N1"); // Accessing the text component and it's equal to the paramanter which is converted to a string as can't put float in textbox. M1 = Shows the float with 1 decimal place
        MyMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        // SFXTextUI.text = volume.ToString("N1"); // Accessing the text component and it's equal to the paramanter which is converted to a string as can't put float in textbox. M1 = Shows the float with 1 decimal place
        MyMixer.SetFloat("Main Engine SFX", Mathf.Log10(volume)*20);
        MyMixer.SetFloat("Crash SFX", Mathf.Log10(volume)*20);
        MyMixer.SetFloat("Level Complete SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    private void LoadSFXVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetSFXVolume();
    }

    public void ResetAudioDefaults()
    {
        PlayerPrefs.DeleteKey("SFXVolume");
        PlayerPrefs.DeleteKey("musicVolume");

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1.2f);
        SetMusicVolume();

        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 2.2f);
        SetSFXVolume();
    }

//     private void Start() 
//     {
//         if (PlayerPrefs.HasKey("VolumeValue"))
//         {
//             LoadValues();
//         } else
//         {    
//             musicSlider.value = 0.5f; // Value of the slider
//         }
//     }


//     public void OnChangeSlider(float volume) // When we move the slider we pass in a value
//     {
//         volumeTextUI.text = volume.ToString("N1"); // Accessing the text component and it's equal to the paramanter which is converted to a string as can't put float in textbox. M1 = Shows the float with 1 decimal place

//         switch (MixMode)
//         {
//             case AudioMixMode.LogrithmicMixerVolume:
//             MyMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
//             break;
//         }

//         SaveVolumeChanges();

//     }

//     public enum AudioMixMode
//     {
//         LogrithmicMixerVolume
//     }

//     public void SaveVolumeChanges()
//     {
//         float volumeValue = musicSlider.value;
//         PlayerPrefs.SetFloat("VolumeValue", volumeValue); // PlayerPrefs.SetFloat(string key, float value);
//     }

//     void LoadValues()
//     {
//         float volumeValue = PlayerPrefs.GetFloat("VolumeValue"); // PlayerPrefs.GetFloat(string key)
//         musicSlider.value = volumeValue;
//         // AudioListener.volume = volumeValue; // Only use this line if want ALL audio linked to this
//     }
}