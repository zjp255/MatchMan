using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BegControl : MonoBehaviour
{
    
    public  inventory mybeg;
    public  GameObject begUI;
    public static string armId;
    GameObject player;
    int myBegLength = 0;
    int infoNum = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (mybeg.itemList.Count > myBegLength)
        {
         
            PutItemInbeg();
        
        }
    }

    void PutItemInbeg()
    {
        for (int i = 0; i < mybeg.itemList.Count; i++)
        {
            if (i == 0)
            {
                begUI.transform.Find("Grid").Find("Image").Find("Button").GetComponent<Image>().sprite = mybeg.itemList[i].itemImage;
                AdjustSprite(begUI.transform.Find("Grid").Find("Image").Find("Button").gameObject);
            }
            else
            {
                string imageCode = "Image (" + i + ")";

                begUI.transform.Find("Grid").Find(imageCode).Find("Button").GetComponent<Image>().sprite = mybeg.itemList[i].itemImage;
                AdjustSprite(begUI.transform.Find("Grid").Find(imageCode).Find("Button").gameObject);
            }
            myBegLength++;
        }
    }

    void AdjustSprite(GameObject arm)
    {
        arm.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        arm.GetComponent<Image>().SetNativeSize();
        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
    }

    public  void GetDetail(GameObject button)
    {



        for (int i = 0; i < mybeg.itemList.Count; i++)
        {
            if (button.GetComponent<Image>().sprite == mybeg.itemList[i].itemImage)
            {
                string gameinfo = "Name:" + mybeg.itemList[i].itemName + "\n" + "ATK:+" + mybeg.itemList[i].itemATk + "\n" + "Detail:" + mybeg.itemList[i].itemInfo;

                infoNum = i + 1;
                begUI.transform.Find("Grid").Find("info").GetComponent<Text>().text = gameinfo;
                
            }
        }
    }

    public void UseArm()
    {
        if (infoNum != 0)
        {
            transform.Find("renwu").Find("Image").Find("Image").GetComponent<Image>().sprite = mybeg.itemList[infoNum - 1].itemImage;
            AdjustSprite(transform.Find("renwu").Find("Image").Find("Image").gameObject);
            transform.Find("renwu").Find("Image").Find("Image").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Control>().AddAtk = mybeg.itemList[infoNum - 1].itemATk;
            armId = mybeg.itemList[infoNum - 1].itemId;
            GameObject.FindGameObjectWithTag("Player").transform.Find("body").Find("Weapon").Find("Weapon-Sword").GetComponent<SpriteRenderer>().sprite = mybeg.itemList[infoNum - 1].itemImage;
            GameObject.FindGameObjectWithTag("Player").transform.Find("body").Find("Weapon").Find("Weapon-Sword").transform.localPosition = new Vector3(0f  , 0.4f, -0.5f);
        }
    
    }
}
