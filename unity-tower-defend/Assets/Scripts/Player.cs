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

    bool canFire = true;

    // This method is called by the Slider component when its value changes.
    // The 'angle' parameter automatically receives the new value of the slider.
    private void SetPlayerRotation(float angle)
    {
        // Create a Quaternion representing the desired rotation around the Z-axis
        Quaternion newRotation = Quaternion.Euler(0f, 0f, angle);

        // Apply the rotation to the player's Transform
        transform.rotation = newRotation;
    }

    void rotationSlider_OnValueChanged(float value)
    {
        // Update the player's rotation based on the slider's value
        SetPlayerRotation(value * -1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationSlider.onValueChanged.AddListener(rotationSlider_OnValueChanged);

        fireButton.onClick.AddListener(() => Shoot());
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.white;

        cannonInitialPosition = cannon.transform.localPosition;
    }

    void FixedUpdate()
    {
        // Calculate recoil amount based on time since last fire
        float recoilAmount = (Time.time - lastFireTime) / coolDownTime;
        // Clamp recoilAmount between 0 and 1
        recoilAmount = Mathf.Clamp01(recoilAmount);
        // Apply recoil to cannon's local position
        cannon.transform.localPosition = new Vector3(0f, -0.2f * (1f - recoilAmount), 0f) + cannonInitialPosition;
   

        if (recoilAmount >= 1f)
        {
            canFire = true;
        } else
        {
            canFire = false;
        }

        if (canFire)
        {
             //spriteRenderer.color = new Color(1f, 0.75f, 0f); // Change color to indicate shooting
            
        } else
        {
            //spriteRenderer.color = Color.white;
        }
    }

    // Call this method when you want the tower to shoot
    public void Shoot()
    {
        
        if ( canFire ) {
            lastFireTime = Time.time;

    
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = firePoint.up;

            // 4. Apply a force or set a velocity in that direction
            rb.velocity = direction * bulletSpeed;
        }
    }
}
