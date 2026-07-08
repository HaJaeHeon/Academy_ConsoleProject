using System;
using System.ComponentModel;
using ConsoleGameFramework.Core;
using ConsoleGameFramework.Models;
using ConsoleGameFramework.UI;

public class InventoryManager
{
    public enum Item
    {
        Sword = 1,
        Shield = 2,
        PowerPosion = 3,
        HealthPosion = 4
    }

    private static InventoryManager instance = null;

    public static int maxEquipSize = 2;
    public static int maxInventorySize = 10;
    public List<Item> EquipList = new List<Item>(maxEquipSize);
    public List<Item> InventoryList = new List<Item>(maxInventorySize);

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InventoryManager();

            return instance;
        }
    }

    public void Equip(int index)
    {
        EquipList.Add(InventoryList[index]);
        InventoryList.RemoveAt(index);
    }

    public void PurchaseItem(int index)
    {
        if (InventoryList.Count >= maxInventorySize)
        {
            GameManager.Instance.Context.AddLog("3인벤토리가 가득찼습니다.");
            return;
        }
        InventoryList.Add((Item)index+1);
    }

    public void PrintInventory()
    {
        string boxString = "";
        

        foreach (Item item in InventoryList)
        {
            boxString += $"{item}\n";
            //ConsoleUI.Write($"{item}");
            //GameManager.Instance.Context.AddLog($">> {item} <<");
        }
        ConsoleUI.WriteBox(new[]
        {
            $"",
            $"{boxString}"
        }, "", ConsoleColor.DarkCyan);
    }
    public void PrintEquip()
    {
        
    }
}
