using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fireGun;
    
    private Transform myTransform;

    public float propulsionForce;
    void Start()
    {
        SetInitialReferences();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnGrenade();
        }
    }

    void SpawnGrenade()
    {
        GameObject grenade = (GameObject) Instantiate(fireGun, myTransform.transform.TransformPoint(0, 0, 2f), myTransform.rotation);
        grenade.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsionForce, ForceMode.Impulse);
        Destroy(grenade, 3);
    }

    void SetInitialReferences()
    {
        myTransform = transform;
    }
}
