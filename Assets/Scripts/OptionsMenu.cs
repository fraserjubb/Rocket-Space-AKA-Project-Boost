using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    // [SerializeField] private AudioSource AudioSource;
    [SerializeField] private TextMeshProUGUI ValueText;

    [SerializeField] private AudioMixMode MixMode;

    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{Value.ToString("N4")}"); // This will show the float value up to four decimal places

        switch (MixMode)
        {
            case AudioMixMode.LogrithmicMixerVolume:
            Mixer.SetFloat("Volume", Mathf.Log10(Value)*20);
            break;
        }

    }

    public enum AudioMixMode
    {
        LogrithmicMixerVolume
    }
}
