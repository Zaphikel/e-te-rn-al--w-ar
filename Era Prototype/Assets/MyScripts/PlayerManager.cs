using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats currentStatsClass;
    public PlayerLocomotion playerLocomotion;
    public InventoryObject inventory;
    public int health;
    public int MaxHealth { get => currentStatsClass.health; }
    public int mana;
    public int MaxMana { get => currentStatsClass.mana; }

    public SpriteRenderer weaponHand;
    public Transform bulletPoint;
    public Gun currentGun;

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
        bulletPoint = weaponHand.GetComponentInChildren<Transform>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        currentGun = null;
        canShoot = true;
    }

    private void Update()
    {
        Debug.Log(inventory);
        Debug.Log(inventory.Container);
        Debug.Log(inventory.Container.Items[0]);
        Debug.Log(inventory.Container.Items[0].item);

        if (inventory.Container.Items[0].item is Gun)
        {
            Gun gun = (Gun)inventory.Container.Items[0].item;
            currentGun = gun;
            weaponHand.sprite = gun.topView;
            hasGun = true;
        }
        else
        {
            weaponHand.sprite = null;
            currentGun = null;
            hasGun = false;
        }

        if (Input.GetMouseButton(0) & hasGun)
        {
            Shoot();
        }
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

    private void Shoot()
    {
        if (CanShoot())
        {
            SpawnBullet();
            DecreaseAmmo(1);
        }
    }

    private void DecreaseAmmo(int amount)
    {
        switch (currentGun.ammoRequired)
        {
            case AmmoType.Light:
                lightAmmo -= amount;
                break;
            case AmmoType.Medium:
                mediumAmmo -= amount;
                break;
            case AmmoType.Heavy:
                heavyAmmo -= amount;
                break;
            case AmmoType.Shell:
                shellAmmo -= amount;
                break;
            case AmmoType.Special:
                specialAmmo -= amount;
                break;
            default:
                break;
        }
    }

    private void SpawnBullet()
    {
        Projectile bullet = Instantiate(currentGun.bulletFired, bulletPoint.position, Quaternion.AngleAxis(playerLocomotion.angle, Vector3.forward)).GetComponent<Projectile>();
        //Vector2 rot = bullet.transform.eulerAngles;
        //rot.y = 0;
        //bullet.transform.eulerAngles = rot;
        //bullet.direction = Vector3.up;
        //bullet.direction.z = 0;
    }

    private bool CanShoot()
    {
        if (canShoot)
        {
            StartCoroutine(ResetCanShoot());
            return true;
        }
        return false;
    }

    IEnumerator ResetCanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(currentGun.fireRate);
        canShoot = true;
    }


}
