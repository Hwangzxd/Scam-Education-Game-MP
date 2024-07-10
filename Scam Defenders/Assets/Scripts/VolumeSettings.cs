using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider;

    public void SetVolume()
    {
        float volume = musicSlider.value;
        mainMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }
}
