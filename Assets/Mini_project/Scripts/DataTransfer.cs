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

    [SerializeField]  private Material fractalNoiseMat1 = null;
    [SerializeField]  private Material fractalNoiseMat2 = null;
    [SerializeField]  private Material fractalNoiseMat3 = null;
    [SerializeField]  private Material fractalNoiseMat4 = null;
    [SerializeField]  private Material fractalNoiseMat5 = null;

    private SpectrumManager _spectrumManager = null;
    [SerializeField] private float sampleMultiplier = 10.0f;

    [SerializeField] private bool useAudioSample = true;
    
    [SerializeField] private float delay = 1.0f;
    private float elapsedTime = 0;

    private float sum;
    private float averageFreq;
    
    
    void Start()
    {
        _spectrumManager = SpectrumManager.Instance;
    }

    void Update()
    {

        if (fractalNoiseMat1 && fractalNoiseMat2 && fractalNoiseMat3 && fractalNoiseMat4 && fractalNoiseMat5 != null)
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
        fractalNoiseMat1.SetFloat("_lacunarity", lacunaritySlider.value);
        fractalNoiseMat1.SetFloat("_gain", gainSlider.value);
        fractalNoiseMat1.SetFloat("_value", valueSlider.value);
        fractalNoiseMat1.SetFloat("_power", powerSlider.value);
        fractalNoiseMat1.SetFloat("_amplitude", amplitudeSlider.value);
    }

    void musicDataUpdate(float input)
    {
        
        frequencySlider.value = input;
        fractalNoiseMat1.SetFloat("_frequency", frequencySlider.value);
        fractalNoiseMat2.SetFloat("_frequency", frequencySlider.value);
        fractalNoiseMat3.SetFloat("_frequency", frequencySlider.value);
        fractalNoiseMat4.SetFloat("_frequency", frequencySlider.value);
        fractalNoiseMat5.SetFloat("_frequency", frequencySlider.value);
    }
}
