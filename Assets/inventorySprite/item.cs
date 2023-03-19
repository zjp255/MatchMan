using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item",menuName ="inventorySprite/item")]
public class item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemATk;
    public string itemInfo;
    public string itemId;
}
