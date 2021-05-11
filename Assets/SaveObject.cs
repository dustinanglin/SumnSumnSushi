using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public Hashtable sushiObjects = new Hashtable();
    public Hashtable sauceObjects = new Hashtable();
    public Dish dish = new Dish();
}
