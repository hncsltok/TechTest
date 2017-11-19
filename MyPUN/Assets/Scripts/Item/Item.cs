using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    private string itemName = "";
    public string ItemName { get { return itemName; } set { itemName = value; } }

 
    public Item() { }

    public Item(string name)
    {
        itemName = name;
    }

}
