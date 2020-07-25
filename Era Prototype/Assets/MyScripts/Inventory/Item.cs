using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Item", menuName = "Prototype/Inventory System/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int Id;
    public Rarity rarity;
    public bool stackable;
    public GameObject groundItem;
    public Sprite frontView;
    public Sprite topView;
    public int chestRecieveableValue = 3;
    public int maxStack = 9;
}

[CreateAssetMenu(fileName = "Gun", menuName = "Prototype/Inventory System/Gun")]
public class Gun : Item
{
    public AmmoType ammoRequired;
    public int baseDamage;
    public float reloadTime;
    public float fireRate;
    public string animationName = "Fire";
}

[CreateAssetMenu(fileName = "Consumable", menuName = "Prototype/Inventory System/Consumable")]
public class Consumable : Item
{
    public int recoveredHealth;
    public float timeToConsume;
}

[CreateAssetMenu(fileName = "Throwable", menuName = "Prototype/Inventory System/Throwable")]
public class Throwable : Item
{
    public float throwDistance;
    public float fireRate;
    public int throwDamage;
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic,
    God_Forged
}