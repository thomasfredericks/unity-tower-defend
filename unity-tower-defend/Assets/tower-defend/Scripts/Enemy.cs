using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float torqueKp = 1f; // proportional gain
    public float torqueKd = 0.1f; // derivative gain
    public float maxTorque = 40f; // clamp torque

    [Header("Movement Settings")]
    public float moveSpeed = 2f;

    private Rigidbody2D rb;

    bool alive = true;
    float deathTime = 0f;

    SpriteRenderer spriteRenderer;

    //public float maxAngularVelocity;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!alive)
                return;
            alive = false;
            deathTime = Time.time;
            Destroy(gameObject, 0.5f);
        }
    }

    void FixedUpdate()
    {
        if (!alive)
            return;

        // --- ROTATION ---
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Angle difference (-180 to 180)
        float angleDiff = Mathf.DeltaAngle(rb.rotation, targetAngle);

        // PD torque: proportional + derivative
        float torque = angleDiff * torqueKp - rb.angularVelocity * torqueKd;

        // Clamp torque so it never spins uncontrollably
        torque = Mathf.Clamp(torque, -maxTorque, maxTorque);

        rb.AddTorque(torque);

        // --- FORWARD MOVEMENT ---
        rb.AddForce(transform.right * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            spriteRenderer.color = new Color(
                1f,
                0f,
                0f,
                Mathf.Lerp(1f, 0f, (Time.time - deathTime) / 0.5f)
            );
            return;
        }
    }
}
