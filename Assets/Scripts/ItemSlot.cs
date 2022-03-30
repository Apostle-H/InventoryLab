using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public ItemSO itemSO;
    [SerializeField] private Transform Slot;

    [HideInInspector] public bool hasItem;

    public void SetItem(ItemSO item)
    {
        itemSO = item;
        Slot.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(itemSO.spritePath);
        Slot.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 1);

        hasItem = true;
    }
}
