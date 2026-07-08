using System;

public class InventoryManager
{
    public enum Item
    {
        Sword,
        Shield,
        PowerPosion,
        HealthPosion
    }

    private static InventoryManager instance = null;

    public List<Item> EquipList = new List<Item>();
    public List<Item> PosionList = new List<Item>();

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InventoryManager();

            return instance;
        }
    }

    public void Test()
    {

    }
}
