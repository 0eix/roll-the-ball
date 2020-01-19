using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public AudioClip _clip;
    public string _name;

    [Range(0f, 1f)]
    public float _volume;
    [Range(.1f, 3f)]
    public float _pitch;

    public bool _loop;

    [HideInInspector]
    public AudioSource _source;
}
