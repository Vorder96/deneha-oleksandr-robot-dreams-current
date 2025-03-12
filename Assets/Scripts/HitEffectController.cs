using UnityEngine;

public class HitEffectController : MonoBehaviour, IHitEffect
{
    public GameObject hitEffectPrefab;
    public Color hitEffectColor = Color.red;
    public float hitEffectSize = 1.0f;
    public float hitEffectDuration = 0.5f;
    public void ShowHitEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        GameObject hitEffect = Instantiate(hitEffectPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
        
        MeshRenderer hitEffectRenderer = hitEffect.GetComponent<MeshRenderer>();
        if (hitEffectRenderer != null)
        {
            hitEffectRenderer.material.color = hitEffectColor; 
        }
        
        hitEffect.transform.localScale = Vector3.one * hitEffectSize;
        
        Destroy(hitEffect, hitEffectDuration);
    }
}