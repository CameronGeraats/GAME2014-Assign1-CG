using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/*
 * File: PlayerController.cs
 * Name: Cameron Geraats
 * ID: 100806837
 * Last Modified: 24/10/20
 * Description:
 *      Manages the player movement, input, bullet firing, and restricts player to screen boundary
 * Revisions: No previous revisions
 */
public class PlayerController : MonoBehaviour, IDestructible
{
    public BulletManager bulletManager;

    public Image hpFillBar;

    [Header("Boundary Check")]
    public float boundary;

    [Header("Player Speed")]
    public float speed;
    public float maxSpeed;
    public float lerpValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;
    private RectTransform playerRect;    
            
    public int Health { get; set; }

    // Start is called before the first frame update
    // Sets up the private variables and modifies the rotation if the screen is rotated
    void Start()
    {
        Destroy(GameObject.Find("SoundMenuMusic"));
        Health = 100;
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        playerRect = GetComponentInParent<RectTransform>();
    }

    // Update is called once per frame
    // Runs Movement, Boundary Checking, and Bullet Firing methods per frame
    void Update()
    {
        hpFillBar.fillAmount = Health / 100.0f;
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    // Fires a bullet every 60 frames from the Bullet Pool if available
     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            var bullet = bulletManager.GetBullet(transform.position, gameObject.name);
            bullet.GetComponent<BulletController>().Rotate(0);
        }
    }

    // Handles player input and Moves the player avatar
    private void _Move()
    {
        float direction = 0.0f;
        // Handles portrait mode input
        //if (screenOrient == ScreenOrientation.Portrait)
        {
            // touch input support
            foreach (var touch in Input.touches)
            {
                var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                if (worldTouch.x > transform.position.x)
                {
                    // direction is positive
                    direction = 1.0f;
                }

                if (worldTouch.x < transform.position.x)
                {
                    // direction is negative
                    direction = -1.0f;
                }

                m_touchesEnded = worldTouch;

            }

            // keyboard support
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                // direction is negative
                direction = -1.0f;
            }

            if (m_touchesEnded.x != 0.0f)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, lerpValue), transform.position.y);
            }
            else
            {
                Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * speed, 0.0f);
                m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
                m_rigidBody.velocity *= 0.99f;
            }
        }
    }

    // Checks that the player is between the screen boundaries
    // Portrait orientation checks the X-axis
    // Landscape orientation checks the Y-axis
    private void _CheckBounds()
    {        
        // check right bounds
        if ( playerRect.anchoredPosition.x >= boundary)        //if (transform.position.x >= horizontalBoundary)
        {
            playerRect.anchoredPosition = new Vector2(boundary, playerRect.anchoredPosition.y);            //transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }     
        // check left bounds
        if ( playerRect.anchoredPosition.x <= -boundary)        //if (transform.position.x <= -horizontalBoundary)
        {
            playerRect.anchoredPosition = new Vector2(-boundary, playerRect.anchoredPosition.y);            //transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }
    }
}
