using UnityEngine;
using UnityEngine.UI;

public class DummyController : MonoBehaviour, IHitEffect
{
    public float maxHP = 100f;
    private float currentHP;
    public GameObject customHitEffectPrefab;
    public Image healthBar;
    public float damageAmount = 10f;
    public ScoreManager scoreManager;
    public GameObject headObject; 

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        if (headObject == null)
        {
            headObject = transform.Find("Head")?.gameObject;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHP / maxHP;
        }
    }

    public void ShowHitEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (customHitEffectPrefab != null)
        {
            GameObject effect = Instantiate(customHitEffectPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            Destroy(effect, 0.5f);
        }

        bool isHeadshot = IsHeadshot(hitPoint);

        if (isHeadshot)
        {
            TakeDamage(damageAmount * 2f);

            if (scoreManager != null)
            {
                scoreManager.AddScore(40);
            }
        }
        else
        {
            TakeDamage(damageAmount);

            if (scoreManager != null)
            {
                scoreManager.AddScore(20);
            }
        }
    }

    bool IsHeadshot(Vector3 hitPoint)
    {
        if (headObject != null)
        {
            Collider headCollider = headObject.GetComponent<Collider>();
            if (headCollider != null)
            {
                if (headCollider.bounds.Contains(hitPoint))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
