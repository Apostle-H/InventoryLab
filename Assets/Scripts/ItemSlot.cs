using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private Transform Slot;

    private void Start()
    {
        InitItem(itemSO);
    }

    public void InitItem(ItemSO item)
    {
        itemSO = item;

        Slot.GetChild(0).GetComponent<Image>().sprite = (Sprite)Resources.Load(itemSO.spritePath);
    }
}
