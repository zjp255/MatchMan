using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class life : MonoBehaviour
{
    [Header("ÉúÃüÓë×´Ì¬")]
    public int lifePoint = 50;

    public GameObject playerLPUI;
    public GameObject monsterLPUI;
    public GameObject bossLPUI;
    public Camera cam;

    public int originLP;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        originLP = lifePoint;
        if (playerLPUI != null)
            width = playerLPUI.transform.Find("blood").Find("Image").GetComponent<RectTransform>().rect.width;
        if (monsterLPUI != null)
        {
            width = monsterLPUI.GetComponent<RectTransform>().rect.width;
            monsterLPUI.SetActive(true);
        }
        if (bossLPUI != null)
        {
            width = bossLPUI.transform.Find("Image").GetComponent<RectTransform>().rect.width;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gameObject.CompareTag("monster"))
        {
            Vector2 pos = cam.WorldToScreenPoint(transform.position);
            if (this.gameObject.name == "Skeleton")
            {
                monsterLPUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x - 25f, pos.y + 44f);
            }
            else
            {
                monsterLPUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x - 25f, pos.y + 20f);
            }
            //if (lifePoint <= 0)
            //    monsterLPUI.SetActive(false);

        }
       
           
            if (gameObject.CompareTag("Player"))
            {
                //playerLPUI.transform.Find("blood").Find("Text").GetComponent<Text>().text = this.GetComponent<Control>().nowLP + "/" + originLP;
                playerLPUI.transform.Find("blood").Find("Image").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((float)lifePoint/ (float)originLP) * width);
            }
            if (gameObject.CompareTag("monster"))
            {
                monsterLPUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((float)lifePoint / (float)originLP) * width);
            }
            if (gameObject.CompareTag("boss"))
            {
            bossLPUI.transform.Find("Image").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((float)lifePoint / (float)originLP) * width);

        }

        
    }
}
