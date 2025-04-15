using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlasmaBlast : MonoBehaviour
{
    public int damage;
    public float flySpeed;

    public void Init(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        Debug.Log(other.tag);
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy shot");
            other.GetComponent<Enemy>().LoseHealth(damage);
            Destroy(gameObject);
        }
        if(other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        FlyForward();
    }

    void FlyForward()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime);
    }
}
