using UnityEngine;
using UnityEngine.UI;
public class HealthUIEnemy : MonoBehaviour
{
    Image healthBar;
    private void Start()
    {
        healthBar = GetComponent<Image>();
    }
    public void updateHealth(float newHealth, float maxHealth)
    {
        healthBar.fillAmount = newHealth / maxHealth;
    }
  


}
