using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * File: EnemyController.cs
 * Name: Cameron Geraats
 * ID: 100806837
 * Last Modified: 24/10/20
 * Description:
 *      Manages the enemy movement, and restricts enemy to screen boundary
 * Revisions: No previous revisions
 */
public class EnemyController : MonoBehaviour, IDestructible
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float direction;

    public BulletManager bulletManager;

    private RectTransform enemyRect;

    public int Health { get; set; }

    // Start is called before the first frame update
    // Sets up the private variables and modifies the rotation if the screen is rotated
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        Health = 20;
        enemyRect = GetComponentInParent<RectTransform>();
    }
    private void _FireBullet()
    {
        // delay bullet firing 
        if (Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position, gameObject.name).GetComponent<BulletController>().Rotate(1);
        }
    }

    // Moves the enemy then checks the boundary
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    // Moves the enemy along one direction, X-axis for portrait OR Y-axis for Landscape
    private void _Move()
    {     
        if(transform.position.y > 700)
            transform.position -= new Vector3(0, 15 * horizontalSpeed * direction * Time.deltaTime, 0.0f);
        transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
    }


    // Checks that the enemy is between the screen boundaries
    // Portrait orientation checks the X-axis
    // Landscape orientation checks the Y-axis
    private void _CheckBounds()
    {   // check right bounds
        if (enemyRect.anchoredPosition.x >= horizontalBoundary)     
        {
            direction = -1.0f;
        }       
        // check left bounds
        if (enemyRect.anchoredPosition.x <= -horizontalBoundary)        
        {
            direction = 1.0f;
        }        
    }
}
