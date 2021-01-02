using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTransfer : MonoBehaviour
{
    [SerializeField] private Slider lacunaritySlider = null;
    [SerializeField] private Slider gainSlider = null;
    [SerializeField] private Slider valueSlider = null;
    [SerializeField] private Slider powerSlider = null;
    
    [SerializeField] private Slider amplitudeSlider = null;
    [SerializeField] private Slider frequencySlider = null;

    [SerializeField]  private Material fractalNoiseMat = null;

    private SpectrumManager _spectrumManager = null;
    [SerializeField] private float sampleMultiplier = 100.0f;

    [SerializeField] private bool useAudioSample = true;
    
    [SerializeField] private float delay = 1.0f;
    private float elapsedTime = 0;
    
    void Start()
    {
        _spectrumManager = SpectrumManager.Instance;
    }

    void Update()
    {

        if (fractalNoiseMat != null)
        {
            if (useAudioSample)
            {
                float value = _spectrumManager.Samples[0] * sampleMultiplier;
                float clampValue = Mathf.Clamp(value, 0.1f, 100.0f);

                if (elapsedTime >= delay)
                {
                    musicDataUpdate(clampValue);
                    elapsedTime = 0;
                }
                else
                {
                    elapsedTime += Time.deltaTime * 1.0f;
                    Debug.Log(elapsedTime);
                }
                
            }
        }
        fractalNoiseMat.SetFloat("_lacunarity", lacunaritySlider.value);
        fractalNoiseMat.SetFloat("_gain", gainSlider.value);
        fractalNoiseMat.SetFloat("_value", valueSlider.value);
        fractalNoiseMat.SetFloat("_power", powerSlider.value);
    }

    void musicDataUpdate(float input)
    {
        amplitudeSlider.value = input;
        frequencySlider.value = input;
        fractalNoiseMat.SetFloat("_amplitude", amplitudeSlider.value);
        fractalNoiseMat.SetFloat("_frequency", frequencySlider.value);
    }
}
