using System;
using System.ComponentModel;
using ConsoleGameFramework.Core;
using ConsoleGameFramework.Models;
using ConsoleGameFramework.UI;

public class InventoryManager
{
    public enum ItemType
    {
        Sword = 1,
        Shield = 2,
        PowerPosion = 3,
        HealthPosion = 4
    }
    //public class ItemInfo
    //{
    //    public ItemType Type;
    //    public int Price;

    //    public ItemInfo(ItemType type, int price)
    //    {
    //        Type = type;
    //        Price = price;
    //    }
    //}
    public Dictionary<ItemType, int> itemPrice = new Dictionary<ItemType, int>()
    {
        {ItemType.Sword, 10 },
        {ItemType.Shield, 30 },
        {ItemType.PowerPosion, 50 },
        {ItemType.HealthPosion, 50 }
    };

    private static InventoryManager instance = null;

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InventoryManager();

            return instance;
        }
    }

    public static int maxEquipSize = 2;
    public static int maxInventorySize = 7;
    public List<ItemType> EquipList = new List<ItemType>(maxEquipSize);
    public List<ItemType> InventoryList = new List<ItemType>(maxInventorySize);
    public List<string> stringInventoryList = new List<string>();
    public List<IReadOnlyList<string>> readOnlyInventoryList = new List<IReadOnlyList<string>>();

    public void Equip(int index)
    {
        EquipList.Add(InventoryList[index]);
        InventoryList.RemoveAt(index);
    }

    public void AddItem(int index)
    {
        if (InventoryList.Count >= maxInventorySize)
        {
            GameManager.Instance.Context.AddLog("3인벤토리가 가득찼습니다.");
            return;
        }
        InventoryList.Add((ItemType)index);
        stringInventoryList.Add(((ItemType)index).ToString());
    }

    public void PrintInventory()
    {
        readOnlyInventoryList.Clear();
        foreach (var item in stringInventoryList)
        {
            readOnlyInventoryList.Add(new[] { item });
        }
        foreach (var list in InventoryList)
        {
            GameManager.Instance.Context.AddLog($"{list}");
        }
        ConsoleUI.WriteTable(
            headers: new[] { "아이템" },
            rows: readOnlyInventoryList
        );
    }
    public void PrintEquip()
    {
        readOnlyInventoryList.Clear();
        foreach (var item in stringInventoryList)
        {
            readOnlyInventoryList.Add(new[] { item });
        }
        foreach (var list in InventoryList)
        {
            GameManager.Instance.Context.AddLog($"{list}");
        }
        ConsoleUI.WriteTable(
            headers: new[] { "아이템" },
            rows: readOnlyInventoryList
        );
    }
}
