using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    AnimatorStateInfo animatorInfo;

    [Header("ÊôÐÔ")]
    //public int life;
    public int Atk = 5;
    public int AddAtk = 0;
    public float speed = 5;
    public float jumpSpeed = 7;

    [Header("×´Ì¬")]
    public bool canJump;
    public bool isAttack = false;

    [Header("¹¥»÷¼ì²â")]
    public float atkRange = 1.0f;
    public float offeset = 0.25f;

    [Header("ÎäÆ÷Óë±³°ü")]
    public GameObject sword;
    public inventory myBeg;

    public GameObject LPUI;
    float sumTime = 0;
    int maxAtk = 0;
    public int nowLP;
    int nextLP;
    bool canMove = true;

    LayerMask enemyLayer ;

    int maxJump = 2;
    int jump = 0;
    private void Awake()
    {
        myBeg.itemList.Clear();
    }



    void Start()
    {
        
        rigidBody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyLayer = LayerMask.GetMask("enemy");
        nowLP = GetComponent<life>().lifePoint;
        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
       
        nextLP = GetComponent<life>().lifePoint;
        Death();
        Hurt();
        if (canMove == true)
        {
            MoveControl();
           
        }
        Attack(/*animatorInfo*/);

    }

    //private void FixedUpdate()
    //{
        
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "ground" && rigidBody.velocity.y == 0 && this.transform.position.y > collision.transform.position.y)
        {
            jump = 0;
            animator.SetBool("IsJump", false);
        }
        AudioManager.PlayFootAudio();
    }

    void Attack(/*AnimatorStateInfo animatorInfo*/)
    {
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetMouseButtonDown(0) && isAttack == false)
        {
            
            animator.SetBool("IsAttack", true);
            isAttack = true;
           
        }
        //ÉäÏß
        RaycastHit2D hitEnemy = Raycast(new Vector2(-transform.localScale.x * offeset, 0f), new Vector2(-transform.localScale.x, 0f), atkRange, enemyLayer);
        if (hitEnemy && isAttack == true && maxAtk == 0 && animatorInfo.normalizedTime < 0.8f && animatorInfo.normalizedTime > 0.7f && animatorInfo.IsTag("Attack"))
        {
            
           GameObject enemy = hitEnemy.transform.gameObject;
            //string jiaoben = enemy.name + "Control";
            enemy.GetComponent<life>().lifePoint -= (Atk + AddAtk);
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2((float)(-transform.localScale.x * 0.25) * 0.4f, 0.5f) * 5, ForceMode2D.Impulse);
            maxAtk++;
        }

        if (animatorInfo.normalizedTime > 0.9f && isAttack == true && animatorInfo.IsTag("Attack"))
        {
            AudioManager.PlayPlayerAtkAudio();
            maxAtk = 0;
            animator.SetBool("IsAttack", false);
            isAttack = false;
        }
        
        
    }


    private void MoveControl()
    {
        float horizontalmove;
        horizontalmove = Input.GetAxis("Horizontal");
        if (isAttack == false || (isAttack == true && animator.GetBool("IsJump")))
        {
            if (horizontalmove != 0)
            {
                if (!animator.GetBool("IsRun") )
                    AudioManager.PlayFootAudio();
                if (jump != 0)
                {
                    AudioManager.StopFootAudio();
                }
                animator.SetBool("IsRun", true);
                if (horizontalmove > 0)
                    this.transform.localScale = new Vector3(-1, 1, 1);
                else if (horizontalmove < 0)
                    this.transform.localScale = new Vector3(1, 1, 1);
                this.transform.Translate(Vector2.right * speed * horizontalmove * Time.deltaTime, Space.World);
            }
            else
            {
                if (animator.GetBool("IsRun"))
                    AudioManager.StopFootAudio();
                animator.SetBool("IsRun", false);
               
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && jump < maxJump && canJump == true)
        {
            if (jump == 1)
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            jump++;
            animator.SetBool("IsJump", true);
           
        }
       
    }


    RaycastHit2D Raycast(Vector2 offset,Vector2 rayDiraction,float length, LayerMask layer)
    {
        Vector2 pos = this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction , length, layer);

        Debug.DrawRay(pos + offset, rayDiraction * length,Color.red);
        return hit;
    }

    void Hurt()
    {

        if (nowLP > nextLP)
        {
            if (animator.GetBool("IsHurt") == false)
            {
                animator.SetBool("IsHurt", true);
                canMove = false;
                //rigidBody.AddForce(new Vector2(-transform.localScale.x * 0.25f * 0.4f, 0.5f) * 5, ForceMode2D.Impulse);
                nowLP = nextLP;

            }
        }

        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (animator.GetBool("IsHurt") == true)
        //{
        //    this.transform.Translate(Vector2.right * 2 * transform.localScale.x * Time.deltaTime, Space.World);

        //}
        if (animatorInfo.normalizedTime > 0.9f && animatorInfo.IsTag("hurt"))
        {
           
            animator.SetBool("IsHurt", false);
            canMove = true;
            maxAtk = 0;
            animator.SetBool("IsAttack", false);
            isAttack = false;
            
        }
    }

    /// <summary>
    /// ËÀÍö
    /// </summary>
    void Death()
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
            GameManager.GameOver(false);

        }
    }
}
