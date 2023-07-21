using UnityEngine.Audio;
using System; // Allows for using Array.Find
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // Makes a list/array using the Sound Class
    
    public static AudioManager instance;

    // Awake is called when the game is booted up so before it "starts"
    void Awake()
    {
        // to ensure a new audiomanager is not created in the next scene i.e. duplicates.
        if (instance == null) // if we don't have an instance of an audio manager in our scene
            instance = this; // instance = thisObject
        else // if we already have an instance in our scene
        {
            Destroy(gameObject); // destroy this object
            return; // makes sure no more code is called before we destroy the object. 
        }
        DontDestroyOnLoad(gameObject); // Prevents audio manager from being destroyed between scenes.

        // The following links our custom inspector to AudioSource:
        foreach (Sound s in sounds) // each sound in our sound array
        {
            s.source = gameObject.AddComponent<AudioSource>(); //Adds an audiosource component which is stored in the source variable (found in Sound)
            s.source.outputAudioMixerGroup = s.mixerGroup; 
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme"); // String must match the name given in the Inspector
    }

    void Update()
    {
        PauseAndResume("Crash SFX");
        PauseAndResume("Level Complete SFX");
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName); // Finds the name of the song in the list
        if (s == null)
        {
            SongNotFound(soundName);
            return;
        }
        s.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName); // Search the sound array and find the sound where sound.name == soundName variable
        if (s == null)
        {
            SongNotFound(soundName);
            return;
        }
        s.source.Stop();
    }

    // For pausing any specific sounds on pause menu, will resume when out of pause:
    public void PauseAndResume(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            SongNotFound(soundName);
            return;
        }
        if (PauseMenu.gameIsPaused == true)
        {s.source.Pause();}
        else if(PauseMenu.gameIsPaused == false)
        {s.source.UnPause();}
    }

    void SongNotFound(string soundName)
    {
        Debug.LogWarning($"Sound '{soundName}' not found. Check argument string name matches in script and Unity."); // LOGWARNING - Own warning message so that the game doesn't completely break
    }

}
