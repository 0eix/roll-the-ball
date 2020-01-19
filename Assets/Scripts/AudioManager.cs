using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;

    public static AudioManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in _sounds)
        {
            sound._source = gameObject.AddComponent<AudioSource>();
            sound._source.clip = sound._clip;
            sound._source.volume = sound._volume;
            sound._source.pitch = sound._pitch;
            sound._source.loop = sound._loop;
        }
    }

    public void Start()
    {
        Play("Theme");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s._source.Play();
    }
}
