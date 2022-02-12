using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public GameObject strikeSound;

    Animator anim;
    BoxCollider hitbox;
    bool strikeTime = false;

    void Start() 
    {
        anim = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider>();
    }

    public void Strike() 
    {
        if (!FindObjectOfType<Health>().isDead) 
        {
            anim.Play("BatAttack");
        }
    }

    public void CheckColliding() 
    {
        strikeTime = true;
    }

    public void StopColliding() 
    {
        strikeTime = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (strikeTime && other.tag == "Monster") 
        {
            Instantiate(strikeSound, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<MonsterHealth>().TakeDamage(damage);
            strikeTime = false;
        }
    }
}
