﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour
{
    AudioSource audioSource;
    public bool triggerParticleEffect;
    public float triggerMagnitude;

    public string friendlyName;
    public string description;

    public ParticleSystem particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!enabled) return;
        if (collision.relativeVelocity.magnitude > triggerMagnitude)
        {
            if (!audioSource.isPlaying) { 
                audioSource.Play();
                Debug.Log("Magnitude: " + collision.relativeVelocity.magnitude);
            }

            if (triggerParticleEffect)
            {
                if (particleEffect != null)
                {
                    particleEffect.Play();
                }
            }
        }
    }
}
