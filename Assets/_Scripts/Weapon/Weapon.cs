using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    Animator anim;

    [Header("Refrences")]
    public GameObject bullet;

    [Header("Settings")]
    public float attackRate;
    public float reloadRate;

    [HideInInspector]
    public float attackCounter, reloadCounter;
    public int currentAmmo, maxAmmo;
    public Sprite weaponSprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }

        if (reloadCounter > 0)
        {
            reloadCounter -= Time.deltaTime;
        }
    }

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        AudioManager.instance.PlaySFX("Reload");
        PlayerController.instance.UpdateAmmo();
        foreach (GameObject bullet in UIController.instance.ammoNum)
        {
            bullet.SetActive(true);
        }
    }

    public void AttackAnim()
    {
        anim.SetTrigger("Shoot");
    }
}