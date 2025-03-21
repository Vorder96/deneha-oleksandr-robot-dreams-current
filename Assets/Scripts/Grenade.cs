using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public LayerMask enemyMask;
    public LayerMask obstacleLayer;

    public UnityEvent onExplode;
    public UnityEvent<GameObject> onEnemyHit;

    public void Explode()
    {
        onExplode.Invoke();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyMask);
        foreach (var hitCollider in hitColliders)
        {
            if (!Physics.Raycast(transform.position, (hitCollider.transform.position - transform.position).normalized, explosionRadius, obstacleLayer))
            {
                onEnemyHit.Invoke(hitCollider.gameObject);
                hitCollider.GetComponent<Rigidbody>()?.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}