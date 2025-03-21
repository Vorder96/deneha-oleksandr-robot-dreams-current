using System.Collections;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 2f;
    public float grenadeLifetime = 3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        grenade.GetComponent<Rigidbody>().AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
        StartCoroutine(ExplodeAfterDelay(grenade));
    }

    IEnumerator ExplodeAfterDelay(GameObject grenade)
    {
        yield return new WaitForSeconds(grenadeLifetime);
        grenade.GetComponent<Grenade>().Explode();
    }
}