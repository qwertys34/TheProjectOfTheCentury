using Game.scripts.interfaces;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    private IDamagable damagableObject;

    private void Awake()
    {
        damagableObject = GetComponentInParent<IDamagable>();
        if (damagableObject == null)
            Debug.LogWarning("null in healthBarUI");
    }

    private void Start() => damagableObject.OnDamageTaken += UpdateHealthUI;

    private void OnDestroy() => damagableObject.OnDamageTaken -= UpdateHealthUI;

    private void UpdateHealthUI()
    {
        Debug.Log(damagableObject.GetCurrentHealth());
        Debug.Log(damagableObject.GetMaxHealth());
        float healthImageFillAmount = (float)damagableObject.GetCurrentHealth() / damagableObject.GetMaxHealth();
        healthImageFillAmount = Mathf.Clamp01(healthImageFillAmount);
        healthImage.fillAmount = healthImageFillAmount;
    }
}
