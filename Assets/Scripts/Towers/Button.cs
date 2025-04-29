using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Sprite deactivated;
    public Sprite activated;
    public GameObject laserPrefab;
    public float duration;
    private SpriteRenderer spriteRenderer;
    private bool isDeactivated = false;
    private GameObject laser;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = activated;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (isDeactivated)
        return;
        //When an enemy touches the button it activates the laser and deactivates the button
        if (collision.CompareTag("Enemy"))
        {
            SoundManager.instance.ButtonActivated();
            spriteRenderer.sprite = deactivated;
            isDeactivated = true;
            StartCoroutine(ActivateLaser());
        }
    }

    IEnumerator ActivateLaser()
    {
        //Runs the laser and then destroys it
        Vector3 laserSpawn = transform.position + new Vector3(4f, 0f, 0f);
        laser = Instantiate(laserPrefab, laserSpawn, Quaternion.identity);
        SoundManager.instance.CrazyLaser();
        yield return new WaitForSeconds(duration);
        Destroy(laser);
    }
}
