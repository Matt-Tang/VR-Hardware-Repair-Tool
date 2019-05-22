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
            if (!isHeld() && contact != null)
            {
                transform.parent.position = contact.transform.position - transform.localPosition;
                transform.parent.rotation = contact.transform.rotation;
                Debug.Log(transform.position.x + ", " + transform.localPosition.x);
                GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //Debug.Log($"Teleport parent ({transform.parent.position}) to contact ({contact.transform.position})");
            }
        }

        void FixedUpdate()
        {
            //Debug.Log($"isHeld: {isHeld()}; contact: {contact}");
            //Debug.Log($"Throwable active: {isHeld()}");
        }

        void OnTriggerEnter(Collider c)
        {
            if (isHeld())
            {
                Debug.Log($"Trigger Enter: {c.name}, {c.transform.position}");
                contact = c.gameObject;
            }
            else
            {
                Debug.Log("Wasn't held, doesn't count!");
            }
        }

        void OnTriggerExit(Collider c)
        {
            if (isHeld())
            {
                Debug.Log("Trigger Exit: " + c.name);
                contact = null;
            }
            else
            {
                Debug.Log("Wasn't held, doesn't count!");
            }
        }

        bool isHeld()
        {
            bool? held = transform.GetComponentInParent<Throwable>()?.attached;
            return held != null ? (bool)held : false;
        }
    }
}