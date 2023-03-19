using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestControl : MonoBehaviour
{
    GameObject player;
    GameObject arm;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject chestUI;
    public GameObject getUI;
    public item army;
    public bool isBeGet = false;
    Animator animator;

    bool isUse = false;
    // Start is called before the first frame update
    void Start()
    {
        arm = getUI.transform.Find("arm").gameObject;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < this.transform.position.x + 0.5 &&
            player.transform.position.x > this.transform.position.x - 0.5 &&
            player.transform.position.y < this.transform.position.y + 0.5 &&
            player.transform.position.y > this.transform.position.y - 0.5 &&
           (monster1 == null ? true : monster1.activeInHierarchy == false) &&
           (monster2 == null ? true : monster2.activeInHierarchy == false) &&
           (monster3 == null ? true : monster3.activeInHierarchy == false)&&
           isBeGet ==false
           )
           
        {
            chestUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("IsOpen", true);
              
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("open"))
            {
                getUI.SetActive(true);
                AudioManager.PlayGetItemAudio();
                arm.GetComponent<Image>().sprite = army.itemImage;
                arm.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                arm.GetComponent<Image>().SetNativeSize();
                arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                player.GetComponent<Control>().myBeg.itemList.Add(army);
                isBeGet = true;
            }
            isUse = true;
        }
        else if (isUse == true)
        {
            animator.SetBool("IsOpen", false);
            chestUI.SetActive(false);
            isUse = false;
        }
    }
}
