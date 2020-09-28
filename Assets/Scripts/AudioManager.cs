using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;   

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.AudioSource =  gameObject.AddComponent<AudioSource>();
           s.AudioSource.clip = s.Clip;
           s.AudioSource.volume = s.Volume;
           s.AudioSource.pitch = s.Pitch;
            s.AudioSource.spatialBlend = 1;
            s.AudioSource.minDistance = 5;
            s.AudioSource.maxDistance = 12;
        }
    }

    public void Play(string name, bool loop)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            return;
        }

        if (loop)
        {
            s.AudioSource.loop = true;
        }
        else
        {
            s.AudioSource.loop = false;
        }

        
        s.AudioSource.Play();
        Debug.Log(name + " " + loop);
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            return;
        }       
        s.AudioSource.Stop();
    }


}
