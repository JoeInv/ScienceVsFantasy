using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float attackInterval;
    Coroutine attackOrder;
    public int health;
    public int damage;
    public float speed;
    Towers detectedTower;

    void Update()
    {
        if(!detectedTower)
        {
            Move();
        }
    }

    void InflictDamage()
    {
        bool towerDied = detectedTower.LoseHealth(damage);
        detectedTower.GetComponent<SpriteRenderer>().color=Color.red;
        if(towerDied)
        {
            detectedTower = null;
            StopCoroutine(attackOrder);
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            InflictDamage();
            yield return new WaitForSeconds(1f);
            detectedTower.GetComponent<SpriteRenderer>().color=Color.white;
            yield return new WaitForSeconds(attackInterval);
        }
    }

    void Move()
    {
        transform.Translate(-transform.right*speed*Time.deltaTime);
    }

    public void LoseHealth(int val)
    {
        health -= val;
        StartCoroutine(Hurt());
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Hurt()
    {
        GetComponent<SpriteRenderer>().color=Color.red;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color=Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectedTower)
            return;
        
        if(collision.tag == "Tower")
        {
            detectedTower = collision.GetComponent<Towers>();
            attackOrder = StartCoroutine(Attack());
        }
        if(collision.tag == "Die")
        {
            Destroy(gameObject);
            Debug.Log("Enemy Made it to End");
            //Either gonna end after one makes it or do lives
        }
    }

    
}
