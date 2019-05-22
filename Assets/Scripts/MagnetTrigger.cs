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
            if (isHeld())
            {
                DestroyImmediate(transform.parent.gameObject.GetComponent<FixedJoint>());
                wasHeld = true;
            }

            Debug.Log($"wasHeld: {wasHeld}; isHeld: {isHeld()}; contact: {contact};");
            if (wasHeld && !isHeld() && contact != null)
            {
                transform.parent.position = contact.transform.position - transform.localPosition;
                transform.parent.rotation = Quaternion.Euler(contact.transform.rotation.eulerAngles + new Vector3(-90, 0, 0));
                transform.parent.gameObject.AddComponent<FixedJoint>();
                GetComponentInParent<FixedJoint>().connectedBody = contact.GetComponentInParent<Rigidbody>();
            }

            if (!isHeld() && wasHeld)
            {
                wasHeld = false;
            }
        }

        void FixedUpdate()
        {
        }

        void OnTriggerStay(Collider c)
        {
            if (c.name.Contains("Magnet"))
            {
                if (isHeld())
                {
                    Debug.Log($"Trigger Enter: {c.name}, {c.transform.position}");
                    contact = c.gameObject;
                }
                else
                {
                    //Debug.Log("Wasn't held, doesn't count!");
                }
            }
        }

        void OnTriggerExit(Collider c)
        {
            if (c.name.Contains("Magnet"))
            {
                if (isHeld())
                {
                    Debug.Log("Trigger Exit: " + c.name);
                    contact = null;
                }
                else
                {
                    //Debug.Log("Wasn't held, doesn't count!");
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