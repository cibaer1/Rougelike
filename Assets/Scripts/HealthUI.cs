using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image regenBar;
    public void updateHealth(float newHealth, float maxHealth)
    {
        healthBar.fillAmount = newHealth / maxHealth;
    }
    public void updateRegenBar(float regenBarFloat, float maxRegenBar)
    {
        regenBar.fillAmount = regenBarFloat / maxRegenBar;
    }
    
    
}
