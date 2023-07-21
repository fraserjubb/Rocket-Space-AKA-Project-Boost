using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // Allows the entire class to appear in Unity inspector
public class Sound
{
    public string name;
    
    public AudioClip clip;

    public AudioMixerGroup mixerGroup;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] // Even though it's public it won't show in the inspector
    public AudioSource source; // so that we can use the AudioSource methods on the sound we create .i.e. .Play()
}
