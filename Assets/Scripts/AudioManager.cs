using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance;

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

        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
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
}
