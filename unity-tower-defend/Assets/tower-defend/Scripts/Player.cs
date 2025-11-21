using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Assign your bullet prefab in the Inspector
    public GameObject bulletPrefab;

    // Point where the bullet will spawn
    public Transform firePoint;

    public GameObject cannon;
    Vector3 cannonInitialPosition;

    // Speed at which the bullet will travel
    public float bulletSpeed = 10f;

    public float coolDownTime = 1f;
    private float lastFireTime = 0f;

    public Slider rotationSlider;

    public Button fireButton;

    SpriteRenderer spriteRenderer;

    public GameObject readyToFireIndicator;
    SpriteRenderer readyToFireIndicatorSpriteRenderer;

    bool canFire = true;

    public bool getCanFire()
    {
        return canFire;
    }

    public void SetPlayerRotation(float angle)
    {
        // Create a Quaternion representing the desired rotation around the Z-axis
        Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);
        // Apply the rotation to the player's Transform
        transform.rotation = newRotation;
    }

    void rotationSlider_OnValueChanged(float value)
    {
        SetPlayerRotation(value * -1f);
    }

    void Start()
    {
        rotationSlider.onValueChanged.AddListener(rotationSlider_OnValueChanged);

        fireButton.onClick.AddListener(() => Shoot());

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.white;

        cannonInitialPosition = cannon.transform.localPosition;

        readyToFireIndicatorSpriteRenderer = readyToFireIndicator.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // |-- Recoil animation
        float recoilAmount = (Time.time - lastFireTime) / coolDownTime;
        recoilAmount = Mathf.Clamp01(recoilAmount);
        cannon.transform.localPosition =
            new Vector3(0f, -0.3f * (1f - recoilAmount), 0f) + cannonInitialPosition;
        // |--

        // |--  Update canFire based on cooldown
        if (Time.time - lastFireTime >= coolDownTime)
        {
            canFire = true;
            readyToFireIndicatorSpriteRenderer.color = Color.green;
        }
        else
        {
            canFire = false;
            readyToFireIndicatorSpriteRenderer.color = Color.black;
        }
        // |--
    }

    public void Shoot()
    {
        if (canFire)
        {
            lastFireTime = Time.time;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = firePoint.up;

            rb.velocity = direction * bulletSpeed;
        }
    }
}
