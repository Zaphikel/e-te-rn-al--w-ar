using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats currentStatsClass;
    public InventoryObject inventory;
    public int health;
    public int MaxHealth { get => currentStatsClass.health; }
    public int mana;
    public int MaxMana { get => currentStatsClass.mana; }

    [Header("Player States")]
    public bool hasGun;
    public bool canMove; 
    public bool canShoot;
    public bool isDead;
    public bool isReloading;
    public bool isConsuming;
    public bool hasWonGame;

    [Header("Player Ammo")]
    public int lightAmmo = 0;
    public int mediumAmmo = 0;
    public int heavyAmmo = 0;
    public int shellAmmo = 0;
    public int specialAmmo = 0;

    [Header("Misc.")]
    public int kills = 0;
    
    void Start()
    {
        health = MaxHealth;
        mana = MaxMana;
        currentStatsClass = new PlayerStats();
        inventory = new InventoryObject();
        canMove = true;
    }

    private void TakeDamage(int damage)
    {
        //damage -= currentStatsClass.defense * Mathf.CeilToInt(damage / Mathf.Log(currentStatsClass.defense));
        damage -= (int) Mathf.SmoothStep(0, damage, damage / currentStatsClass.defense);
        health -= damage;
        if (health < 1)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        canMove = false;
        canShoot = false;
        isReloading = false;
        isConsuming = false;
        hasWonGame = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damager")
        {
            TakeDamage(10);
        }
    }

}
