using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new inventory", menuName = "inventorySprite/iventory")]
public class inventory : ScriptableObject
{
    public List<item> itemList = new List<item>();
}
