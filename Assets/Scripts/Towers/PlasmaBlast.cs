using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlasmaBlast : MonoBehaviour
{
    public int damage;
    public float flySpeed; //How fast the blast moves

    public void Init(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            //Deals damage when it hits enemy and then destroys it
            other.GetComponent<Enemy>().LoseHealth(damage);
            Destroy(gameObject); 
        }
        if(other.CompareTag("Destroy"))
        {
            Destroy(gameObject); //Destroys if it hits the zone at the end of the map 
        }
    }

    void Update()
    {
        FlyForward();
    }

    void FlyForward()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime); //Moves the blast to the right
    }
}
