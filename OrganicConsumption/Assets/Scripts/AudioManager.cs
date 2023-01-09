using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

// This class's code largely learned from Brackeys tutorial: https://www.youtube.com/watch?v=6OT43pvUyfY
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private VolumeSettingsSO audioSO;
    private float masterVol;
    private float sfxVol;
    private float musicVol;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Awake()
    {
        audioSO = Resources.Load<VolumeSettingsSO>("SO/Volume Settings");
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    public void setMaster(float amount)
    {
        audioMixer.SetFloat("volume", amount);
    }

    public void setSfx(float amount)
    {
        sfxVol = Mathf.Clamp(amount, 0f, 1f);
    }

    public void setMusic(float amount)
    {
        musicVol = Mathf.Clamp(amount, 0f, 1f);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}
