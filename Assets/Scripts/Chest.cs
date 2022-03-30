using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform inventory;

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemSpecial;

    [SerializeField] private Button openChestBtn;
    [SerializeField] private Button AddBtn;

    private ItemSO currentItem;

    public void OpenChest()
    {
        if (currentItem)
            ItemsPool.Instance.items.Enqueue(currentItem);

        if (ItemsPool.Instance.items.Count <= 0)
        {
            ResetChestUI();
            return;
        }

        currentItem = ItemsPool.Instance.items.Dequeue();

        itemImage.sprite = Resources.Load<Sprite>(currentItem.spritePath);
        itemName.text = currentItem.itemName;
        itemType.text = currentItem.type.ToString();
        itemPrice.text = currentItem.price.ToString();

        switch (currentItem.type)
        {
            case ItemType.armor:
                itemSpecial.text = ((ArmorSO)currentItem).defence.ToString();
                break;
            case ItemType.weapon:
                itemSpecial.text = ((WeaponSO)currentItem).damage.ToString();
                break;
            case ItemType.consumable:
                itemSpecial.text = ((ConsumableSO)currentItem).effect.ToString();
                break;
            case ItemType.material:
                itemSpecial.text = ((MaterialSO)currentItem).durablity.ToString();
                break;
        }

        if (ItemsPool.Instance.items.Count <= 0)
            openChestBtn.interactable = false;
    }

    public void AddToInventory()
    {
        if (!currentItem)
            return;

        for (int i = 0; i < inventory.childCount; i++)
        {
            if (!inventory.GetChild(i).GetComponent<ItemSlot>().hasItem)
            {
                inventory.GetChild(i).GetComponent<ItemSlot>().SetItem(currentItem);

                currentItem = null;
                OpenChest();

                if (i == inventory.childCount - 1)
                {
                    AddBtn.interactable = false;
                }
                return;
            }
        }
    }

    private void ResetChestUI()
    {
        itemImage.sprite = null;
        itemName.text = "NONE";
        itemType.text = "NONE";
        itemPrice.text = "NONE";
        itemSpecial.text = "NONE";
    }
}
