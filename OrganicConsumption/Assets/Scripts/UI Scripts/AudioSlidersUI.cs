using UnityEngine;
using UnityEngine.UI;

public class AudioSlidersUI : MonoBehaviour
{
    VolumeSettingsSO volumeSettings;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    private void Awake()
    {
        volumeSettings = Resources.Load<VolumeSettingsSO>("SO/Volume Settings");

        masterSlider.value = volumeSettings.masterVolume;
        sfxSlider.value = volumeSettings.sfxVolume;
        musicSlider.value = volumeSettings.musicVolume;
    }

    public void VolumeChanged()
    {
         volumeSettings.UpdateVolumes(masterSlider.value, sfxSlider.value, musicSlider.value);
    }
}
