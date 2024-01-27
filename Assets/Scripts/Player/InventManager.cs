using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevesls = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
     public List<Image> passiveItemUISlots = new List<Image>(6);

    public void AddWeapon(int slotIndex,WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevesls[slotIndex] = weapon.weaponData.Level;
         weaponUISlots[slotIndex].enabled = true; // 确保图标槽可见
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem item)
    {
        passiveItemSlots[slotIndex] = item;
        passiveItemLevels[slotIndex] = item.passiveItemData.Level;
         passiveItemUISlots[slotIndex].enabled = true; // 确保图标槽可见
        passiveItemUISlots[slotIndex].sprite = item.passiveItemData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
         if (weaponSlots.Count >slotIndex)
         {
              WeaponController weapon = weaponSlots[slotIndex];
              if (!weapon.weaponData.NextLevelPrefab)
              {
                Debug.LogError("This weapon is already at the highest level.");
                    return;
              }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.position = transform.position;
            upgradedWeapon.transform.parent = transform;
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevesls[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;
         }  
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
         if (passiveItemSlots.Count >slotIndex)
         {
              PassiveItem passiveItem = passiveItemSlots[slotIndex];
              if (!passiveItem.passiveItemData.NextLevelPrefab)
              {
                Debug.LogError("This weapon is already at the highest level.");
                    return;
              }
            GameObject upgradedWeapon = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.position = transform.position;
            upgradedWeapon.transform.parent = transform;
            AddPassiveItem(slotIndex, upgradedWeapon.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedWeapon.GetComponent<PassiveItem>().passiveItemData.Level;
         }  
    }
}
