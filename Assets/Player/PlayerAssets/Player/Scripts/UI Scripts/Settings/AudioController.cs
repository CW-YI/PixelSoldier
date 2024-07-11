using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {

        // Set the initial slider value based on the current AudioListener volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to the slider to update the AudioListener volume when the slider changes
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float volume)
    {
        // Update the AudioListener volume based on the slider value
        AudioListener.volume = volume;
    }
}
