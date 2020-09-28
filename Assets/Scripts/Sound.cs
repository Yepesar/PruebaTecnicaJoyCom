using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;
    [Range(0,1)]
    [SerializeField] private float volume;
    [Range(0.1f,3)]
    [SerializeField] private float pitch;
    [HideInInspector] private AudioSource audioSource;

    public AudioClip Clip { get => clip; set => clip = value; }
    public float Volume { get => volume; set => volume = value; }
    public float Pitch { get => pitch; set => pitch = value; }
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    public string Name { get => name; set => name = value; }
}
