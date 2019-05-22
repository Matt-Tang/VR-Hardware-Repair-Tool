using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakEffects : MonoBehaviour
{
    AudioSource audioSource;
    public bool explode;
    public float breakMagnitude;
    public float explosionMagnitude;

    public ParticleSystem ps;

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
        if (collision.relativeVelocity.magnitude > breakMagnitude)
        {
            if (!audioSource.isPlaying) { 
                audioSource.Play();
                Debug.Log("Magnitude: " + collision.relativeVelocity.magnitude);
            }

            if (explode && collision.relativeVelocity.magnitude > explosionMagnitude)
            {
                
                if (ps != null)
                {
                    ps.Play();
                }
            }
        }
    }
}
