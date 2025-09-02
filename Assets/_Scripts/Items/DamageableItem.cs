using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableItem : MonoBehaviour, IDamageable
{
    [Header("Item Durability")]
    public float currentDurability;

    public void TakeDamage(float damage, bool attackPlayer)
    {
        if (!attackPlayer)
        {
            currentDurability -= damage;

            if (currentDurability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}