using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
    public static EnemyHealthManager instance;

    Outline outline;

    [Header("Enemy Health")]
    public float currentHealth;
    public float maxHealth;
    public Slider healthBar;
    [Tooltip("Range To Show Health Bar")]public float healthRange;

    [Header("Outline Setting")]
    public float offTime;
    public GameObject scanScreen;
    public GameObject piece;

    private void Awake()
    {
        instance = this;
        outline = GetComponent<Outline>();
    }
    void Update()
    {
        UpdateHealth();

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (distanceToPlayer <= healthRange)
        {
            healthBar.gameObject.SetActive(true);
        }
        else
        {
            healthBar.gameObject.SetActive(false);
            currentHealth = maxHealth;
        }
    }

    void UpdateHealth()
    {
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;
    }

    public void TakeDamage(float damage, bool attackPlayer)
    {
        if (!attackPlayer)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                healthBar.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Instantiate(piece, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
        }
    }


    public void OnXRay()
    {
        StartCoroutine(EnemySilhouette());
    }

    public IEnumerator EnemySilhouette()
    {
        outline.enabled = true;
        scanScreen.SetActive(true);
        yield return new WaitForSeconds(offTime);
        outline.enabled = false;
        scanScreen.SetActive(false);
    }
}