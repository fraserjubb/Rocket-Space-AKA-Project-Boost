using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;


    // Start is called before the first frame update
    void Awake()
    {
        // to ensure a new audiomanager is not created in the next scene i.e. duplicates.
        if (instance == null) // we don't have an audio manager in our scene
            instance = this; // instance = thisObject
        else // if we already have an instance in our scene
        {
            Destroy(gameObject); // destroy this object
            return; // makes sure no more code is called before we call the object. 
        }
        DontDestroyOnLoad(gameObject);

        // links our custom inspector to AudioSource
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mixerGroup;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    void Update()
    {
        PauseAndResume("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound '{name}' not found. Check argument string name matches in script and Unity.");
            return;
        }
        s.source.Play();
    }

    public void PauseAndResume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound '{name}' not found. Check argument string name matches in script and Unity.");
            return;
        } else if(PauseMenu.gameIsPaused == true)
        {s.source.Pause();}
        else if(PauseMenu.gameIsPaused == false)
        {s.source.UnPause();}
    }
}
