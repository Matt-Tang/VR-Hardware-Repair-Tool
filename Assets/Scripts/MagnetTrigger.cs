using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Chris.GR.Wtf
{
    public class MagnetTrigger : MonoBehaviour
    {
        GameObject contact;
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
                    contact = null;
                }
                wasHeld = true;
            }
            
            // Snap to the thing if magnets are touching
            if (wasHeld && !isHeld() && contact != null)
            {
                float x, y, z;
                x = contact.transform.position.x;
                y = contact.transform.position.y;
                z = contact.transform.position.z;
                // Man, everything's got a different rotation, this ain't worth it yo. Just add some "You did it" particle or somethin'
                //transform.parent.position = contact.transform.position;
                //transform.parent.rotation = Quaternion.Euler(contact.transform.rotation.eulerAngles + new Vector3(-90, 0, 0));
                transform.parent.gameObject.AddComponent<FixedJoint>();
                GetComponentInParent<FixedJoint>().connectedBody = contact.GetComponentInParent<Rigidbody>();

                // Do explosion or something here (Depending on if first word of name matches magnet name [eg: "*POE* Variant" -> "*POE*Magnet"] )
                if (contact.transform.name.Contains(transform.parent.name.Split(' ')[0]))
                {
                    GameObject.Find("Ding").GetComponent<AudioSource>().Play();
                }
                else
                {
                    GameObject.Find("Crash").GetComponent<AudioSource>().Play();
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
                    // ¯\_(ツ)_/¯
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