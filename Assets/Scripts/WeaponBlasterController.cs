using UnityEngine;
using System.Collections;

public class WeaponBlasterController : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer laserLine;
    public float laserDuration = 0.05f;
    public float laserRange = 100f;
    public GameObject muzzleFlashObject;
    public float flashDuration = 0.05f;
    public float muzzleFlashScale = 0.5f;
    public GameObject hitEffectPrefab;
    public float hitEffectSize = 1.0f;
    public float hitEffectDuration = 0.5f;
    public GameObject defaultHitEffectPrefab;
    public GameObject enemyHitEffectPrefab;

    public Transform rotationBase;
    public Transform cameraTransform;

    void Update()
    {
        HandleWeaponVerticalRotation();

        if (Input.GetMouseButtonDown(0))
            ShootLaser();
    }

    void HandleWeaponVerticalRotation()
    {
        if (rotationBase == null || cameraTransform == null) return;
        
        Vector3 targetDirection = cameraTransform.forward;
        
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        float pitch = lookRotation.eulerAngles.x;

        if (pitch > 180f) pitch -= 360f;
        
        rotationBase.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void ShootLaser()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        StartCoroutine(PlayMuzzleFlash());

        if (Physics.Raycast(ray, out hit, laserRange))
        {
            targetPoint = hit.point;
            
            if (hit.collider.CompareTag("Enemy"))
            {
                DummyController dummy = hit.collider.GetComponent<DummyController>();
                if (dummy != null)
                {
                    dummy.ShowHitEffect(hit.point, hit.normal);
                }

                StartCoroutine(ShowHitEffect(hit.point, hit.normal, enemyHitEffectPrefab));
            }
            else
            {
                StartCoroutine(ShowHitEffect(hit.point, hit.normal, defaultHitEffectPrefab));
            }

            StartCoroutine(ShowLaser(hit.point));
        }
        else
        {
            targetPoint = ray.origin + ray.direction * laserRange;
            StartCoroutine(ShowLaser(targetPoint));
        }
    }

    IEnumerator ShowLaser(Vector3 hitPoint)
    {
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, hitPoint);
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    IEnumerator PlayMuzzleFlash()
    {
        if (muzzleFlashObject != null)
        {
            muzzleFlashObject.SetActive(true);
            muzzleFlashObject.transform.position = firePoint.position;
            muzzleFlashObject.transform.localScale = Vector3.one * muzzleFlashScale;
            yield return new WaitForSeconds(flashDuration);
            muzzleFlashObject.SetActive(false);
        }
    }

    IEnumerator ShowHitEffect(Vector3 hitPoint, Vector3 hitNormal, GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            GameObject hitEffect = Instantiate(effectPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            hitEffect.transform.localScale = Vector3.one * hitEffectSize;
            yield return new WaitForSeconds(hitEffectDuration);
            Destroy(hitEffect);
        }
    }
}
