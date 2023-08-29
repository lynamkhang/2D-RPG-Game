using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float maxHealth = 100f;
  public float currentHealth;
  public float maxStamina = 100f;
  public float currentStamina;
  public HealthBar healthBar;
  public StaminaBar staminaBar;
  public float staminaRegenRate = 10f;
  private bool isRegeneratingSta = false;
  public bool isUsingSta = false;

  private void Start ()
  {
    //player health
    currentHealth = maxHealth;
    healthBar.SetMaxHealth(maxHealth);

    //player stamina
    currentStamina = maxStamina;
    staminaBar.SetMaxStamina(maxStamina);
  }

   private void Update ()
  {
    if (isRegeneratingSta)
    {
      RegenerateStamina();
    }
  }

  public void TakeDamage(float damage)
  {
    currentHealth -= damage;

    healthBar.SetHealth(currentHealth);
  }

  public void UseStamina(float amount)
  {
    currentStamina -= amount;

    staminaBar.SetStamina(currentStamina);

    isUsingSta = true;
  }

  public void StartStaRegen()
  {
    isRegeneratingSta = true;
  }

  public void StopStaRegen()
  {
    isRegeneratingSta = false;
  }

  private void RegenerateStamina()
  {
    currentStamina = Mathf.Min(currentStamina + (staminaRegenRate * Time.deltaTime), maxStamina);
    staminaBar.SetStamina(currentStamina);
  }
}
