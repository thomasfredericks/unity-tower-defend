using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public Vector2 velocity;

    bool alive = true;
    float deathTime = 0f;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            spriteRenderer.color = new Color(
                1f,
                0.75f,
                0f,
                Mathf.Lerp(1f, 0f, (Time.time - deathTime) / 1.0f)
            );
            return;
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the bullet once it leaves the player's view
        Destroy(gameObject);
        // Optional: Add a Debug.Log here if you want to confirm it's working
        // Debug.Log("Bullet left the screen and was destroyed.");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
         if (!alive) return;
        alive = false;
        deathTime = Time.time;
        Destroy(gameObject, 1.0f);
    }

    // You might also want to destroy the bullet if it hits something
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            return;
        }
        // Destroy the bullet upon impact
        Destroy(gameObject);
    }
}
