using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo,
                20f))
        {
            Debug.Log("True");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.Log("False");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
        }
    }
}
