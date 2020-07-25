using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class WeaponManager : MonoBehaviour
{
    PlayerManager playerManager;
    public InventoryObject inventory;
    public Item currentSelectedItem;

    public Image ammoFill;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        inventory = playerManager.inventory;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UseItem();
        }
        ammoText.text = GetAvailableAmmo(currentSelectedItem).ToString();
    }

    private Gun GetGunFromItem(Item item)
    {
        Gun gun = (Gun)item;
        return gun;
    }

    private int GetAvailableAmmo(Item item)
    {
        if (item is Gun)
        {
            Gun gun = GetGunFromItem(item);
            switch (gun.ammoRequired)
            {
                case AmmoType.Light:
                    return playerManager.lightAmmo;
                case AmmoType.Medium:
                    return playerManager.mediumAmmo;
                case AmmoType.Heavy:
                    return playerManager.heavyAmmo;
                case AmmoType.Shell:
                    return playerManager.shellAmmo;
                case AmmoType.Special:
                    return playerManager.specialAmmo;
                default:
                    return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    private void UseItem()
    {
        if (currentSelectedItem is Gun)
        {

        }
    }
}
