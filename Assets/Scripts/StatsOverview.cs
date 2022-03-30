using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StatsOverview : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemSpecial;
    
    private int UILayer;
    private Transform UIOver;
    private ItemSlot itemSlotOver;
    private bool isShowing;

    private Sprite tempSprite;
    private string tempName;
    private string tempType;
    private string tempPrice;
    private string tempSpecial;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    private void Update()
    {
        UIOver = IsOverUI(GetEventSystemRaycastResults());

        if (!isShowing && UIOver)
        {
            if ((UIOver.TryGetComponent(out itemSlotOver) || UIOver.parent.TryGetComponent(out itemSlotOver)) && itemSlotOver.itemSO)
            {
                ShowStats(itemSlotOver.itemSO);
                isShowing = true;
            }
        }
        else if (isShowing && UIOver != itemSlotOver.transform)
        {
            for (int i = 0; i < itemSlotOver.transform.childCount; i++)
            {
                if (UIOver == itemSlotOver.transform.GetChild(i))
                {
                    return;
                }
            }

            StopShowingStats();
            isShowing = false;
        }
    }

    private Transform IsOverUI(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int i = 0; i < eventSystemRaycastResults.Count; i++)
        {
            Debug.Log(eventSystemRaycastResults[i].gameObject.layer.ToString());
            Debug.Log(eventSystemRaycastResults[i].gameObject.layer == UILayer);
            if (eventSystemRaycastResults[i].gameObject.layer == UILayer)
            {
                return eventSystemRaycastResults[i].gameObject.transform;
            }
        }

        return null;
    }

    private List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }

    private void ShowStats(ItemSO item)
    {
        tempSprite = itemImage.sprite;
        tempName = itemName.text;
        tempType = itemType.text;
        tempPrice = itemPrice.text;
        tempSpecial = itemSpecial.text;

        itemImage.sprite = Resources.Load<Sprite>(item.spritePath);
        itemName.text = item.itemName;
        itemType.text = item.type.ToString();
        itemPrice.text = item.price.ToString();
        switch (item.type)
        {
            case ItemType.armor:
                itemSpecial.text = ((ArmorSO)item).defence.ToString();
                break;
            case ItemType.weapon:
                itemSpecial.text = ((WeaponSO)item).damage.ToString();
                break;
            case ItemType.consumable:
                itemSpecial.text = ((ConsumableSO)item).effect.ToString();
                break;
            case ItemType.material:
                itemSpecial.text = ((MaterialSO)item).durablity.ToString();
                break;
        }
    }

    private void StopShowingStats()
    {
        itemImage.sprite = tempSprite;
        itemName.text = tempName;
        itemType.text = tempType;
        itemPrice.text = tempPrice;
        itemSpecial.text = tempSpecial;
    }
}
