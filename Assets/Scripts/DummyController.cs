using UnityEngine;
using UnityEngine.UI;

public class DummyController : MonoBehaviour, IHitEffect
{
    public float maxHP = 100f;
    private float currentHP;

    public GameObject customHitEffectPrefab;
    public Slider hpSlider; // Прив’яжи сюди слайдер із Canvas

    void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateHPUI();

        if (currentHP <= 0)
        {
            // Додатково: смерть, зникнення і т.д.
            Destroy(gameObject);
        }
    }

    void UpdateHPUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHP / maxHP;
        }
    }

    public void ShowHitEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (customHitEffectPrefab != null)
        {
            GameObject effect = Instantiate(customHitEffectPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            Destroy(effect, 0.5f);
        }

        TakeDamage(10f); // Умовно — 10 одиниць за постріл
    }
}