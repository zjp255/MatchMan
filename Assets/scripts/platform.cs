using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platform : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossBloodUI;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
            bossBloodUI.SetActive(true);
        }
    }
}
