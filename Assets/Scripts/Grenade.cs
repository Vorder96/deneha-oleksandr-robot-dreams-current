using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 3000f;
    public LayerMask enemyMask;

    public UnityEvent onExplode;
    public UnityEvent<GameObject> onEnemyHit;

    private bool hasExploded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;
        Explode();
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        onExplode.Invoke();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyMask);

        foreach (var hitCollider in hitColliders)
        {
            Rigidbody hitRb = hitCollider.GetComponent<Rigidbody>();

            if (hitRb != null)
            {
                hitRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0f, ForceMode.Impulse);
            }

            onEnemyHit.Invoke(hitCollider.gameObject);
        }

        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}