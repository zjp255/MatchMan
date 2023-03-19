using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SaveData 
{
    public List<string> allPlayerItem = new List<string>();
    public Vector3 pos = new Vector3(-0.5f,3f,-1f);
    public string armid;
    public bool isBeGet1 = false;
    public bool isBeGet2 = false;
    public bool isBeGet3 = false;
    public bool isBeGet4 = false;
}
