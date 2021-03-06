﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Chris.GR.Wtf
{
    public class MagnetTrigger : MonoBehaviour
    {
        GameObject contact;
        public ParticleSystem smokeSystem;
        public ParticleSystem explosionsSystem;
        bool wasHeld = false;

        // Start is called before the first frame update
        void Start()
        {
            contact = null;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            // If you pick the thing up, remove joint to the other thing
            if (isHeld() && !wasHeld)
            {
                if (transform.parent.gameObject.GetComponent<FixedJoint>() != null)
                {
                    DestroyImmediate(transform.parent.gameObject.GetComponent<FixedJoint>());
                    ObjectProperties boom = gameObject.GetComponentInParent<ObjectProperties>();
                    if (boom != null) boom.enabled = true;
                    else { Debug.Log("No boom"); }
                    contact = null;
                }
                wasHeld = true;
            }

            // Snap to the thing if magnets are touching
            if (wasHeld && !isHeld() && contact != null)
            {
                float angle = Quaternion.Angle(transform.parent.rotation, contact.transform.rotation);
                if (angle > 15f)
                {
                    //GameObject.Find("Nope").GetComponent<AudioSource>().Play();
                    Debug.Log("Angle too high! " + angle);
                }
                else
                {
                    transform.parent.rotation = Quaternion.Euler(contact.transform.rotation.eulerAngles);
                    transform.parent.gameObject.AddComponent<FixedJoint>();
                    GetComponentInParent<FixedJoint>().connectedBody = contact.GetComponentInParent<Rigidbody>();
                    ObjectProperties boom = gameObject.GetComponentInParent<ObjectProperties>();
                    if (boom != null)
                    {
                        boom.enabled = false;
                    }
                    else { Debug.Log("No boom"); }

                    // Do explosion or something here (Depending on if first word of name matches magnet name [eg: "*POE* Variant" -> "*POE*Magnet"] )
                    if (contact.transform.name.Contains(transform.parent.name.Split(' ')[0]))
                    {
                        GameObject.Find("Ding").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        GameObject.Find("Crash").GetComponent<AudioSource>().Play();
                        explosionsSystem.Play(true);
                        smokeSystem.Play(true);
                    }
                }
            }

            if (!isHeld() && wasHeld)
            {
                wasHeld = false;
            }
        }

        void OnTriggerStay(Collider c)
        {
            if (c.name.Contains("Magnet"))
            {
                if (isHeld())
                {
                    contact = c.gameObject;
                }
                else
                {
                    // fart
                }
            }
        }

        void OnTriggerExit(Collider c)
        {
            if (c.name.Contains("Magnet"))
            {
                if (isHeld())
                {

                    contact = null;
                }
                else
                {
                    // ¯\_(?)_/¯
                }
            }
        }

        bool isHeld()
        {
            bool? held = transform.GetComponentInParent<Throwable>()?.attached;
            return held != null ? (bool)held : false;
        }
    }
}