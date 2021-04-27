using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;

    Slider volumeSlider;
    float volume = 1;

    //Called by load manager when scene is loaded
    public void BindToVolumeSlider() 
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //Assumes volume is the only slider we will have
            volumeSlider = FindObjectOfType<Slider>();
            volumeSlider.onValueChanged.AddListener(delegate { SetVolume(volumeSlider.value); });
            volumeSlider.value = volume;
            volumeSlider.transform.parent.gameObject.SetActive(false);
        }
    }

    void SetVolume(float newVolume) 
    {
        volume = newVolume;
        if (volume == 0.0f) masterMixer.SetFloat("MasterVolume", -80);
        else masterMixer.SetFloat("MasterVolume", -25 + (volume * 25));
    }
}
