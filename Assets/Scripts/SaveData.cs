using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int currentDay;
    public int money;
    public List<Item> inventory = new List<Item>();
}