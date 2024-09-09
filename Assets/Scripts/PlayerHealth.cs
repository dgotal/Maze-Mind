using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;    // Maksimalno zdravlje
    public int currentHealth;      // Trenutno zdravlje
    public Text healthText;        // UI Text za prikaz zdravlja

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        // Ponovo učitaj trenutnu scenu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
