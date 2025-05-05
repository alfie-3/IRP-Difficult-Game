using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=lxAWlxAjPew

public class CarAudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource oneShotSource;
    [SerializeField] AudioSource idleSource;
    [SerializeField] AudioSource runningSource;
    [Space]
    [SerializeField] CarSoundEffects effects;
    [Space]
    [SerializeField] float idleMaxVolume;
    [Space]
    [SerializeField] float runningMaxVolume;
    [SerializeField] float runningMaxPitch;
    [SerializeField] float limiter = 1;

    Engine engine;

    private void Awake()
    {
        if (TryGetComponent(out Engine engine))
        {
            engine.OnChangeCarRunnng += PlayIgnitionSound;

            this.engine = engine;
        }
    }

    private void Update()
    {
        HandleEngineAudio();
    }

    public void HandleEngineAudio()
    {
        if (!engine) return;

        float speedRatio = Mathf.Abs(engine.GetSpeedRatio());

        idleSource.volume = Mathf.Lerp(0.1f, idleMaxVolume, speedRatio);

        runningSource.volume = Mathf.Lerp(0.15f, runningMaxVolume, speedRatio);
        runningSource.pitch = Mathf.Lerp(runningSource.pitch, Mathf.Lerp(0.3f, runningMaxPitch, speedRatio), Time.deltaTime);
    }

    private void PlayIgnitionSound(bool obj)
    {
        if (obj == true)
        {
            oneShotSource.PlayOneShot(effects.StartSound);

            idleSource.Play();
            runningSource.Play();
        }
        else
        {
            idleSource.Stop();
            runningSource.Stop();
        }
    }

}

[System.Serializable]
public class CarSoundEffects
{
    public AudioClip StartSound;
}