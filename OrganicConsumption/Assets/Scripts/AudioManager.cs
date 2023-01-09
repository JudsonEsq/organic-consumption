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

    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Awake()
    {
        audioSO = Resources.Load<VolumeSettingsSO>("SO/Volume Settings");
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.Mixer;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    public void setMaster(float amount)
    {
        if (amount < -29)
        {
            masterMixer.SetFloat("volume", -100f);
            return;
        }
        masterMixer.SetFloat("volume", amount);
    }

    public void setSFX(float amount)
    {
        if(amount <= -29f)
        {
            masterMixer.SetFloat("sfxVolume", -100f);
            return;
        }

        masterMixer.SetFloat("sfxVolume", amount);
    }

    public void setMusic(float amount)
    {
        if (amount < -29)
        {
            masterMixer.SetFloat("musicVolume", -100f);
            return;
        }
        masterMixer.SetFloat("musicVolume", amount);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
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
