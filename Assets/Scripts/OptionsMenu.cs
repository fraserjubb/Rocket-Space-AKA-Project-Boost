using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    
    
    [SerializeField] private AudioMixer Mixer;
    // [SerializeField] private AudioSource AudioSource;
    [SerializeField] private TextMeshProUGUI volumeTextUI = null;

    [SerializeField] private AudioMixMode MixMode;

    private void Start() 
    {
        LoadValues();
    }


    public void OnChangeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("0.0");
        // volumeTextUI.SetText($"{Value.ToString("N4")}"); // This will show the float value up to four decimal places

        switch (MixMode)
        {
            case AudioMixMode.LogrithmicMixerVolume:
            Mixer.SetFloat("Volume", Mathf.Log10(volume)*20);
            break;
        }

    }

    public enum AudioMixMode
    {
        LogrithmicMixerVolume
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
