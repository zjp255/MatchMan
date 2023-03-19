using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class fire : MonoBehaviour
{
    public GameObject TipsUI;
    public GameObject begUI;
    public GameObject saveSuccefulUI;
    public inventory beg;
    public inventory allItem;
    public GameObject chest1;
    public GameObject chest2;
    public GameObject chest3;
    public GameObject chest4;
    GameObject player;
    GameObject[] monster;
    bool isUse = false;
    bool isUse2 = false;
    float saveSuccefulTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (beg.itemList.Count == 0)
        {
            LodeGame();
        }
       
        monster = GameObject.FindGameObjectsWithTag("monster");
    }
    private void FixedUpdate()
    {
        if (saveSuccefulUI.activeInHierarchy == true && isUse2 == true)
        {
            saveSuccefulTime += Time.deltaTime;
            if (saveSuccefulTime > 1f)
            {
                saveSuccefulUI.SetActive(false);
                player.gameObject.GetComponent<life>().lifePoint = player.GetComponent<life>().originLP;
                saveSuccefulTime = 0f;
                isUse2 = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        if (
         player.transform.position.x < this.transform.position.x + 1f &&
         player.transform.position.x > this.transform.position.x - 1f &&
         player.transform.position.y < this.transform.position.y + 2.5f &&
         player.transform.position.y > this.transform.position.y )
        {
            TipsUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                SaveManager.save(playerdata());
              
                for(int i = 0; i < monster.Length; i++)
                {
                    monster[i].SetActive(true);                     
                }
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载当前场景
                saveSuccefulUI.SetActive(true);
                isUse2 = true;

            }
            isUse = true;
        }
        else if(isUse == true)
        {
            
            TipsUI.SetActive(false);
            isUse = false;
        }
    }

    SaveData playerdata()
    {
        var savedata = new SaveData();

        for (int i = 0; i < beg.itemList.Count; i++)
        {
            savedata.allPlayerItem.Add(beg.itemList[i].itemId);
        }

        savedata.pos = player.transform.position;
        savedata.armid = BegControl.armId;
        savedata.isBeGet1 = chest1.GetComponent<ChestControl>().isBeGet;
        savedata.isBeGet2 = chest2.GetComponent<ChestControl>().isBeGet;
        savedata.isBeGet3 = chest3.GetComponent<ChestControl>().isBeGet;
        savedata.isBeGet4 = chest4.GetComponent<ChestControl>().isBeGet;
        return savedata;

    }

    public void LodeGame()
    {
        SaveData saveData = SaveManager.LodeGame();

        player.transform.position = new Vector3(saveData.pos.x, saveData.pos.y, saveData.pos.z);

        for (int i = 0; i < saveData.allPlayerItem.Count; i++)
        {
            for (int x = 0; x < allItem.itemList.Count; x++)
            {
                if (allItem.itemList[x].itemId == saveData.allPlayerItem[i])
                {
                    beg.itemList.Add(allItem.itemList[x]);
                    break;
                }
            }
        }
        
        for (int i = 0; i < beg.itemList.Count; i++)
        {
            if (beg.itemList[i].itemId == saveData.armid)
            {
                GameObject usingarm = begUI.transform.Find("renwu").Find("Image").Find("Image").gameObject;
                usingarm.GetComponent<Image>().sprite = beg.itemList[i].itemImage;
                usingarm.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                usingarm.GetComponent<Image>().SetNativeSize();
                usingarm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                usingarm.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
                BegControl.armId = beg.itemList[i].itemId;
                player.GetComponent<Control>().AddAtk = beg.itemList[i].itemATk;
                player.transform.Find("body").Find("Weapon").Find("Weapon-Sword").GetComponent<SpriteRenderer>().sprite = beg.itemList[i].itemImage;
                player.transform.Find("body").Find("Weapon").Find("Weapon-Sword").transform.localPosition = new Vector3(0f, 0.4f, -0.5f);
            }

        }

        chest1.GetComponent<ChestControl>().isBeGet = saveData.isBeGet1;
        chest2.GetComponent<ChestControl>().isBeGet = saveData.isBeGet2;
        chest3.GetComponent<ChestControl>().isBeGet = saveData.isBeGet3;
        chest4.GetComponent<ChestControl>().isBeGet = saveData.isBeGet4;

    }
}
