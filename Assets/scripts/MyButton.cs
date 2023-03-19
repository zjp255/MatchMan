using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
    GameObject beg;
    private void Start()
    {
        beg = GameObject.Find("beg");

    }

    public void GetInfo()
    {

        beg.GetComponent<BegControl>().GetDetail(gameObject);
    }
}
