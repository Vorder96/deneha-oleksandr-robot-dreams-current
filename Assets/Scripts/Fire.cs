using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fireGun;
    
    private Transform myTransform;

    public float propulsionForce;
    // Start is called before the first frame update
    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
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
