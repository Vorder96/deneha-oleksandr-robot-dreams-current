using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float blastRadius;

    public float explosionForse;
    
    private Collider[] hitColliders;

    void OnCollisionEnter(Collision col)
    {
        DoExplosion(col.contacts[0].point);
        Destroy(gameObject);
    }
    void DoExplosion(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);

        foreach (Collider hitcol in hitColliders)
        { 
            if (hitcol.GetComponent<Rigidbody>() != null)
            {
                hitcol.GetComponent<Rigidbody>().isKinematic = false;
                hitcol.GetComponent<Rigidbody>().AddExplosionForce(explosionForse, explosionPoint, blastRadius, 0.2f, ForceMode.Impulse);   
            }
        }
    }
}
