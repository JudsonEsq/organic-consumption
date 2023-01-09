using UnityEngine;

[CreateAssetMenu(fileName = "Volume Settings")]
public class VolumeSettingsSO : ScriptableObject
{
    [Range(0, 1), SerializeField] float masterValue = 0f; public float masterVolume => masterValue;
    [Range(0, 1), SerializeField] float sfxValue = 0f; public float sfxVolume => sfxValue;
    [Range(0, 1), SerializeField] float musicValue = 0f; public float musicVolume => musicValue;

    public void ChangeMasterVolume(float newMasterVolume) => masterValue = newMasterVolume;
    public void ChangeSFXVolume(float newSfxVolume) => sfxValue = newSfxVolume;
    public void ChangeMusicVolume(float newMusicVolume) => musicValue = newMusicVolume;
}
