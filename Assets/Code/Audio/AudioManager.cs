using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    private Slider volumeSlider;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }          

        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    private void Start()
    {
        //TODO: Add Music?
    }

    public void Play(string name)
    {
        Sounds s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetVolume(string name,float myVolume)
    {
        Sounds s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.volume = myVolume;
    }

    public void SetPitch(string name,float pitch)
    {
        Sounds s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitch;
    }
}