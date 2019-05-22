using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class Overlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Overlay start");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Overlay fixedUpdate");
        string heldObject = "";
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("projectile"))
        {
            if (go.GetComponent<Throwable>().attached)
            {
                heldObject = go.GetComponent<ObjectProperties>().friendlyName + ":\n  " + go.GetComponent<ObjectProperties>().description;
                break;
            }
        }
        if (string.IsNullOrEmpty(heldObject))
        {
            GetComponentInChildren<Text>().text = "";
            gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            GetComponentInChildren<Text>().text = heldObject;
            GetComponentInChildren<Text>().color = new Color(0, 0, 0);
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
