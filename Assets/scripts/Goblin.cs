using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    Animator animator;
    GameObject player;
    Rigidbody2D rigidBody;
    AnimatorStateInfo animatorInfo;

    [Header("属性")]
    public int atk = 5;
    public int speed = 3;

    [Header("攻击1")]
    public float atk1Range = 1.0f;
    //float atk2Range = 1.5f;
    float offeset = 0.1f;

    bool isAttack1 = false;
    bool canMove = true;
    bool isHit = false;

    int maxAtk = 0;
    int nowLP;
    int nextLP;
    int fx;
    public Vector3 originPosition;
    Vector3 originScale;

    LayerMask playerLayer;


    // Start is called before the first frame update
    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        originPosition = this.transform.position;
        originScale = this.transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        nowLP = GetComponent<life>().lifePoint;
        playerLayer = LayerMask.GetMask("player");
    }

    // Update is called once per frame
   public void Update()
    {
        nextLP = GetComponent<life>().lifePoint;
       
            Hurt();

        Death();

        Attake1();

        if (originPosition.x + 10 > player.transform.position.x
            && originPosition.x - 10 < player.transform.position.x
            && originPosition.y + 4 > player.transform.position.y
            && originPosition.y - 2f < player.transform.position.y)
        {
            if (canMove == true)
                Move();

        }
        else
        {
            if (canMove == true)
                MoveBack();
        }

    }

    /// <summary>
    /// 移动和跳跃
    /// </summary>
    public  void Move()
    {

        if (player.transform.position.x - this.transform.position.x > 0)
        {
            fx = 1;
            animator.SetBool("IsRun", true);
        }
        else if (player.transform.position.x - this.transform.position.x < 0)
        {
            fx = -1;
            animator.SetBool("IsRun", true);
        }
        else
        {
            fx = 0;
            animator.SetBool("IsRun", false);
        }
        DrectionChange();
        this.transform.Translate(Vector2.right * speed * fx * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 回到原点
    /// </summary>
    public void MoveBack()
    {

        if (this.transform.position.x - originPosition.x > 0)
        {
            fx = -1;
            animator.SetBool("IsRun", true);

        }
        else if (this.transform.position.x - originPosition.x < 0)
        {
            fx = 1;
            animator.SetBool("IsRun", true);

        }
        else
        {
            fx = 0;
            animator.SetBool("IsRun", false);
        }
        DrectionChange();
        this.transform.Translate(Vector2.right * speed * fx * Time.deltaTime, Space.World);
        if (this.transform.position.x < originPosition.x + 0.01 && this.transform.position.x > originPosition.x - 0.01)
        {
            this.transform.position = new Vector3(originPosition.x,this.transform.position.y,originPosition.z);
            if (originPosition.y - this.transform.position.y > 2)
                this.transform.position = originPosition;
            this.transform.localScale = originScale;
        }
    }


    /// <summary>
    /// 改变面朝向
    /// </summary>
    public void DrectionChange()
    {
        this.transform.localScale = new Vector3(fx *
            (this.transform.localScale.x > 0 ? this.transform.localScale.x : -this.transform.localScale.x)
            , this.transform.localScale.y, this.transform.localScale.z);
    }

    /// <summary>
    /// 受伤
    /// </summary>
    public void Hurt()
    {
        
        if (nowLP > nextLP)
        {
            
            if (animator.GetBool("IsHurt") == false)
            {
                animator.SetBool("IsHurt", true);
                canMove = false;
             
                nowLP = nextLP;
                
            }
        }

        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (animator.GetBool("IsHurt") == true)
        //{


        //}
        if (animatorInfo.normalizedTime > 0.9f && animatorInfo.IsTag("hurt"))
        {
           
            animator.SetBool("IsHurt", false);
            canMove = true;
            maxAtk = 0;
            animator.SetBool("IsAttack1", false);
            isAttack1 = false;
            isHit = false;
        }
    }


    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {

        if (nowLP <= 0)
        {
            animator.SetBool("IsAlive", false);
            canMove = false;
        }
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animatorInfo.normalizedTime > 0.9f && animatorInfo.IsTag("Death"))
        {

            this.gameObject.SetActive(false);
            Reset();
        }
    }

    /// <summary>
    /// attack1
    /// </summary>
    public void Attake1()
    {
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
     
        //射线
        RaycastHit2D hitEnemy = Raycast(new Vector2(transform.localScale.x * offeset /** (1 / transform.localScale.y)*/, 0f), new Vector2(transform.localScale.x/ transform.localScale.y, 0f), atk1Range, playerLayer);
        if (hitEnemy && isAttack1 == false && maxAtk == 0 && animator.GetBool("IsHurt") == false)
        {
         
            animator.SetBool("IsAttack1", true);
            canMove = false;
            isAttack1 = true;
        }

        if (animatorInfo.normalizedTime < 0.7f && animatorInfo.normalizedTime > 0.6f && hitEnemy && animatorInfo.IsTag("attack1") && maxAtk == 0)
        {
            isHit = true;
        }

        if (isAttack1 == true && maxAtk == 0 && isHit == true)
        {


            //string jiaoben = enemy.name + "Control";
            player.GetComponent<life>().lifePoint -= atk;
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.transform.localScale.x * 0.25f * 0.4f, 0.5f) * 5, ForceMode2D.Impulse);
            maxAtk++;
        }
        if (player.GetComponent<Animator>().GetBool("IsHurt"))
        {
            player.transform.Translate(Vector2.right * 2 * transform.localScale.x * 0.25f * Time.deltaTime, Space.World);
        }
        

        if (animatorInfo.normalizedTime > 0.99f && isAttack1 == true && animatorInfo.IsTag("attack1"))
        {
            AudioManager.PlayMonsterAtkAudio();
            maxAtk = 0;
            animator.SetBool("IsAttack1", false);
            isAttack1 = false;
            canMove = true;
            isHit = false;

        }
    }

   public  RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Debug.DrawRay(pos + offset, rayDiraction * length, Color.red);
        return hit;
    }

    public void Reset()
    {

        isAttack1 = false;
        canMove = true;
        isHit = false;

         maxAtk = 0;
        nowLP = GetComponent<life>().originLP;
        nextLP = GetComponent<life>().originLP;
        GetComponent<life>().lifePoint = GetComponent<life>().originLP;
        transform.position = originPosition;
        animator.SetBool("IsAlive",true);
    }
}
