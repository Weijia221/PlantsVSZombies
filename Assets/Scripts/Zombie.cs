using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ZombieState
{
    Move,
    Eat,
    Die
}
public class Zombie : MonoBehaviour
{
    ZombieState zombieState=ZombieState.Move;
    private Rigidbody2D rgd;
    public float moveSpeed = 3;
    private Animator anim;
    public int atkValue=30;
    public float atkDuration = 2;
    private float atkTimer = 0;
    private Plant currentEatPlant;
    public int HP = 100;
    private int currentHP;
    public GameObject zombieHeadPrefab;
    bool haveHead = true;
    // Start is called before the first frame update
    void Start()
    {
        rgd= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        switch(zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                break;
            default: break;
        }
        
    }

    void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime);
    }
    void EatUpdate()
    {
        atkTimer += Time.deltaTime;
        if(atkTimer > atkDuration && currentEatPlant!=null)
        {
            currentEatPlant.TakeDamage(atkValue);
            atkTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Plant" )
        {
            anim.SetBool("IsAttacking", true);
            TransitionToEat();
            currentEatPlant=collision.GetComponent<Plant>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.tag == "Plant" )
        {
            anim.SetBool("IsAttacking", false);
            zombieState= ZombieState.Move;
            currentEatPlant = null;
        }
    }
    void TransitionToEat()
    {
        zombieState=ZombieState.Eat;
        atkTimer=0;
    }
    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return;
        this.currentHP-=damage;
        if(currentHP<=0)
        {
            currentHP = -1;
            Dead();
        }
        float hpPercent = currentHP * 1f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if(hpPercent<.5f &&haveHead)
        {
            haveHead = false;
            GameObject go = GameObject.Instantiate(zombieHeadPrefab,transform.position, Quaternion.identity);
            Destroy(go,2);
        }
    }
    private void Dead()
    {
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 2);
    }
}
