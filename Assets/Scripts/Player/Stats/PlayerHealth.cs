using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Singleton

    public static PlayerHealth PlayerHealthInstance;

    private void Awake()
    {
        if (PlayerHealthInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        PlayerHealthInstance = this;
    }
    #endregion
    public float maxHealth = 100;
    public float ActualmaxHealth;
    public float currentHealth;
    public TextMeshProUGUI HealthT;
    private void Start()
    {
        float modifier = PlayerStats.PlayerStatsInstance.Constitution.GetValue() / 10f;
        ActualmaxHealth = maxHealth + maxHealth * modifier; 
        currentHealth = ActualmaxHealth;
        HealthT.text = "Health" + currentHealth;
    }
    public void PlayerTakeDamage(float damage)
    {
        HealthT.text = "Health" + currentHealth;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }
    public void PlayerDeath()
    {
        Debug.Log("Death");
    }
    public void UpdateUI()
    {
        HealthT.text = "Health" + currentHealth;
    }
}
