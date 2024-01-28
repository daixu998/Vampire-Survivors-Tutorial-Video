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

  PlayerStats player;
  void Start()
  {
    player = GetComponent<PlayerStats>();
  }

  [System.Serializable]
  public class WeaponUpgrade
  {
    public GameObject initialWeapons;
    public WeaponScriptabObject weaponData;

  }
  [System.Serializable]
  public class PassiveItemUpgrade
  {
    public GameObject initialPassiveItem;
    public PassiveItemScriptableObject passiveItemData;

  }
  [System.Serializable]
  public class UpgradeUI
  {
    public Text upgradeNameDisplay;
    public Text upgradeDescriptionDisplay;
    public Image upgradeIcon;
    public Button upgradeButton;
  }
  public List<WeaponUpgrade> weaponUpgradeoptions = new List<WeaponUpgrade>();
  public List<PassiveItemUpgrade> passiveItemUpgradeoptions = new List<PassiveItemUpgrade>();
  public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();
  public void AddWeapon(int slotIndex, WeaponController weapon)
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
    if (weaponSlots.Count > slotIndex)
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
    if (passiveItemSlots.Count > slotIndex)
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

  void ApplyUpgradeOptions()
  {
    foreach (var upgradeOption in upgradeUIOptions)
    {
      int upgradeType = Random.Range(1, 3);
      if (upgradeType == 1)
      {
        WeaponUpgrade weaponUpgrade = weaponUpgradeoptions[Random.Range(0, weaponUpgradeoptions.Count)];
        if (weaponUpgrade != null)
        {
          bool newWeapon = false;
          for (int i = 0; i < weaponSlots.Count; i++)
          {
            if (weaponSlots[i] != null && weaponSlots[i].weaponData == weaponUpgrade.weaponData)
            {
              newWeapon = false;
              if (!newWeapon)
              {
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i));
                upgradeOption.upgradeDescriptionDisplay.text = weaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                upgradeOption.upgradeNameDisplay.text = weaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
              }
              break;
            }
            else
            {
              newWeapon = true;
            }
          }
          if (newWeapon)
          {
            upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(weaponUpgrade.initialWeapons));
            upgradeOption.upgradeDescriptionDisplay.text = weaponUpgrade.weaponData.Description;
            upgradeOption.upgradeNameDisplay.text = weaponUpgrade.weaponData.Name;
          }

          upgradeOption.upgradeIcon.sprite = weaponUpgrade.weaponData.Icon;
        }

      }
      else if (upgradeType == 2)
      {
        PassiveItemUpgrade chosenPassiveItemUpgrade = passiveItemUpgradeoptions[Random.Range(0, passiveItemUpgradeoptions.Count)];
        if (chosenPassiveItemUpgrade != null)
        {
          bool newPassiveItem = false;
          for (int i = 0; i < passiveItemSlots.Count; i++)
          {
            if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
            {
              newPassiveItem = false;
              if (!newPassiveItem)
              {
                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i));
                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
              }
              break;
            }
            else
            {
              newPassiveItem = true;
            }
          }
          if (newPassiveItem)
          {
            upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));
            upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;
            upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
          }
        }



      }

    }
  }

  void RemoveUpgradeOptions()
  {
    foreach (var upgradeOption in upgradeUIOptions)
    {
      upgradeOption.upgradeButton.onClick.RemoveAllListeners();
    }
  }
}
