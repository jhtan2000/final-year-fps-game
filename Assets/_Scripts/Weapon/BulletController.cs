using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public Rigidbody rb;
    public GameObject BulletImpact;
    public int damage;

    public bool attackPlayer;

    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime; //counting down the time given

        if (lifeTime <= 0) // when the time is 0, destroy bullet
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, attackPlayer);
        }

        Destroy(gameObject);
        Instantiate(BulletImpact, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}