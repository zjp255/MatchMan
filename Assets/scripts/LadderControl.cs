using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderControl : MonoBehaviour
{
    GameObject obj;
    [Header("����")]
    public int speed;
    bool isUse = false;
    bool speedControl = false;//��������ʹ������ʱ���ٶ�
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = this.transform.childCount + 1;//�õ����ӽ���
        obj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (((this.transform.GetChild(count - 2).position.x - 0.4) < obj.transform.position.x
            && (this.transform.GetChild(count - 2).position.x + 0.4) > obj.transform.position.x)
            && (this.transform.GetChild(count - 2).position.y + count + 0.3 > obj.transform.position.y
            && this.transform.GetChild(count - 2).position.y - 0.2 < obj.transform.position.y))//�ж��Ƿ������ӵķ�Χ��
        {
            isUse = true;
            float vertical;
            vertical = Input.GetAxis("Vertical");
            obj.GetComponent<Rigidbody2D>().gravityScale = 0;//�����ﲻ������Ӱ��
            obj.GetComponent<Control>().canJump = false;//�ý�ɫ��������ʱ������Ծ
            if (vertical != 0 && speedControl == false)//���ý�������ʱ���ٶ�
            {
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(obj.GetComponent<Rigidbody2D>().velocity.x, 0);
                speedControl = true;
            }
           
            if (obj.transform.position.y > this.transform.position.y - 1 && obj.transform.position.y < this.transform.position.y + 1.5)//����ɫ��Ϊtriggerʹ���ܴ�ǽ
            {
                obj.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                obj.GetComponent<Collider2D>().isTrigger = false;
            }

            if (vertical != 0)
            {
                
               
                obj.transform.Translate(Vector2.up* speed * vertical * Time.deltaTime, Space.World);
            }
           

        }
        else if(isUse == true)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
            obj.GetComponent<Collider2D>().isTrigger = false;
            obj.GetComponent<Control>().canJump = true;
            speedControl = false;
            isUse = false;
        }
    }
}
