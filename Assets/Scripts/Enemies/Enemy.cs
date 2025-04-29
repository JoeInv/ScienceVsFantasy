using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float attackInterval; //Each enemy attacks at a different rate
    Coroutine attackOrder;
    public int health; 
    public int damage;
    public float speed;
    Towers detectedTower; //The Tower the enemy is targeting 
    Animator anim;
    public bool gameOver = false;
    public static event System.Action OnGameOver; //Event for when enemies make it to the end and player loses

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //If its not attacking a tower it'll keep moving forward
        if(!detectedTower)
        {
            Move();
        }
    }

    void InflictDamage()
    {
        if (detectedTower == null)
        return;
        bool towerDied = detectedTower.LoseHealth(damage);
        detectedTower.GetComponent<SpriteRenderer>().color=Color.red; //Flashes the tower red when hit so player can tell
        if(towerDied)
        {
            detectedTower = null;
            if (attackOrder != null)
                StopCoroutine(attackOrder);
            anim.enabled = true; //Reenables the walking animation. Have to do it this way because I made the sprites in Aesprite and for some reason it locks the controllers
            //If I were to make changes to the controller and save it would just reset it and I didn't have time to keep messing with it so this is the next best thing I found
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            if (detectedTower == null)
            yield break;
            
            Vector3 ogPos = transform.position;
            Vector3 towerPos = detectedTower.transform.position;

            StartCoroutine(JumpAtTower(ogPos, towerPos)); //Didn't have time to make animations for the sprites other than the slime and I couldn't get it working unforuntely so I had to make do with this
            InflictDamage();
            yield return new WaitForSeconds(1f);
            if (detectedTower != null)
            detectedTower.GetComponent<SpriteRenderer>().color=Color.white; //Resets sprite back to normal
            yield return new WaitForSeconds(attackInterval);
        }
    }

    IEnumerator JumpAtTower(Vector3 ogPos, Vector3 towerPos)
{
    //Has the enemy jump at the enemy and then return back to where it was since I wasn't able to get attack animations working in time
    Vector3 dir = (towerPos - ogPos).normalized;
    Vector3 attackPos = ogPos + dir * 0.2f;
    float t = 0f;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(ogPos, attackPos, t);
            t += Time.deltaTime * 10f;
            yield return null;
        }
    t = 0f;
        while (t < 1f)
        {
            transform.position = Vector3.Lerp(attackPos, ogPos, t);
            t += Time.deltaTime * 10f;
            yield return null;
        }
    }

    void Move()
    {
        transform.Translate(-transform.right*speed*Time.deltaTime);
    }

    public void LoseHealth(int val)
    {
        health -= val;
        StartCoroutine(Hurt()); //Flashes enemy red
        if (health <= 0)
        {
            EnemySpawner.instance.EnemyKilled(); //When enemy has 0 health it dies and adds to the EnemyKilled in GameManager
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
            if (attackOrder == null)
            anim.enabled = false; //Stop the walking animation when it is at the Tower
            attackOrder = StartCoroutine(Attack());
        }
        else if(collision.tag == "Die")
        {
            Destroy(gameObject); //Kills out of bounds enemies if it were to happen
        }
        else if(collision.tag == "Lose")
        {
            Destroy(gameObject); //Triggers the game over event when an enemy makes it to the end
            gameOver = true;

            if (OnGameOver != null)
                OnGameOver.Invoke(); 
        }
    }

    
}
