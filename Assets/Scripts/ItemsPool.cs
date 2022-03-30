using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemsPool : MonoBehaviour
{
    public static ItemsPool Instance;

    [SerializeField] private List<ItemSO> itemsList;
    [HideInInspector] public Queue<ItemSO> items = new Queue<ItemSO>();

    private void Awake()
    {
        itemsList = itemsList.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < itemsList.Count; i++)
        {
            items.Enqueue(itemsList[i]);
        }

        Instance = this;
    }
}
