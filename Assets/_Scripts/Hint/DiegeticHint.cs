using UnityEngine;

public class DiegeticHint : MonoBehaviour, IDamageable
{
    [Header("Item Durability")]
    public float currentDurability;
    public GameObject hint;

    public void TakeDamage(float damage, bool attackPlayer)
    {
        if (!attackPlayer)
        {
            currentDurability -= damage;
            //Debug.Log("Hit");

            if (currentDurability <= 0)
            {
                AudioManager.instance.PlaySFX("Pop");
                Destroy(gameObject);
                Instantiate(hint, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            }
        }
    }
}