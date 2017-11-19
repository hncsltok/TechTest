using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory :IEnumerable {

    private int ownerID;

    private List<Item> itemList=new List<Item>();


    public int OwnerID { get { return ownerID; } set { ownerID = value; } }
    public List<Item> ItemList { get { return itemList; } set { itemList = value; } }


    public Inventory() { }
    public Inventory(int ownid)
    {
        ownerID = ownid;
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    public void Clear()
    {
        itemList.Clear();
    }

    public void ShowItems()
    {
        foreach (var item in itemList)
        {
            Logger.Write(item.ItemName);
        }
    }

    private List<Item> GetList()
    {
        return itemList;
    }

 

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < itemList.Count-1; i++)
        {
            yield return itemList[i];
        }
        
    }
}
