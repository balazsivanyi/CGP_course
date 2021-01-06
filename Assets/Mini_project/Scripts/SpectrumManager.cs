using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mini_project.Scripts.MonoBehaviourSingleton;

public class SpectrumManager : MonoBehaviourSingleton<SpectrumManager>
{

    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private float[] samples = new float[128];

    [SerializeField] private FFTWindow FftWindowType = FFTWindow.Blackman;

    public float[] Samples
    {
        get { return this.samples; }
    }

    void Update()
    {
        _audioSource.GetSpectrumData(samples, 0, FftWindowType);
    }
}
