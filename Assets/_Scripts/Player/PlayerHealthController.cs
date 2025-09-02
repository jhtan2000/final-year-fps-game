using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public static PlayerHealthController instance;

    [Header("UI")]
    public Slider healthBar;
    [SerializeField] private Image redSplatter;
    [SerializeField] private Image hurtImage;


    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    public float hurtTimer = 0.1f;

    [Header("Stamina")]
    public float currentStamina;
    public float maxStamina = 5;
    public float updateSpeed = 0.8f;

    private float invLength = 1f;
    private float invCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateStatus();
    }
    
    void Update()
    {
        if (invCounter > 0)
        {
            invCounter -= Time.deltaTime;
        }
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;

        UIController.instance.spatialHealth.text = currentHealth.ToString();

        //Health Image
        Color splatterAlpha = redSplatter.color;
        splatterAlpha.a = 1 - (currentHealth / maxHealth);
        redSplatter.color = splatterAlpha;

        // Stamina
        if (PlayerController.instance.IsRunning == true)
        {
            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }
            else
            {
                currentStamina -= updateSpeed * Time.deltaTime * 2;
            }
        }
        else
        {
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
            else
            {
                currentStamina += updateSpeed * Time.deltaTime;
            }
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateStatus();
    }

    public void HealPlayer(float healAmount)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += healAmount;
            UpdateStatus();
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage, bool attackPlayer)
    {
        if (attackPlayer)
        {
            if (invCounter <= 0)
            {
                currentHealth -= damage;
                StartCoroutine(HurtFlash());
                UpdateStatus();

                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);

                    currentHealth = 0;
                    GameManager.instance.Respawn();
                }

                invCounter = invLength;
            }
        }
    }

    IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        yield return new WaitForSeconds(hurtTimer);
        hurtImage.enabled = false;
    }
}
