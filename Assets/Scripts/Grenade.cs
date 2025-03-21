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
            Vector3 directionToEnemy = (hitCollider.transform.position - transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToEnemy, out hit, explosionRadius, obstacleLayer))
            {
                Debug.Log("ПЕРЕШКОДА МІШАЄ ЛУЧУ :(");
                continue;
            }

            if (hit.collider.gameObject == hitCollider.gameObject)
            {
                Debug.Log("Ворог поразка: " + hitCollider.gameObject.name);
                onEnemyHit.Invoke(hitCollider.gameObject);
            }

            Rigidbody enemyRigidbody = hitCollider.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}