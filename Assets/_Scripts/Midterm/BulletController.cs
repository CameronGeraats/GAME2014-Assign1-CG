using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * File: BulletController.cs
 * Name: Cameron Geraats
 * ID: 100806837
 * Last Modified: 24/10/20
 * Description:
 *      Manages the bullet movement, restricts bullets to the screen boundary, and returns them when out of bounds
 * Revisions: No previous revisions
 */
public class BulletController : MonoBehaviour, IApplyDamage
{
    public float verticalSpeed;
    public float verticalBoundary;
    public BulletManager bulletManager;
    public int damage;
    public string CreatorObject;

    private ScreenOrientation screenOrient;
    private RectTransform bulletTransf;

    // Start is called before the first frame update
    // Sets up the private variables and modifies the rotation if the screen is rotated
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        bulletTransf = GetComponentInParent<RectTransform>();      
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    // Moves the bullet up or to the right for portrait and landscape mode respectively
    private void _Move()
    {
        if(CreatorObject == "Player")
            transform.position += new Vector3(0.0f, verticalSpeed, 0) * Time.deltaTime;          
        else
            transform.position -= new Vector3(0.0f, verticalSpeed, 0) * Time.deltaTime;          
    }

    public void Rotate(int type = 0)
    {
        bulletTransf = GetComponentInParent<RectTransform>();       
        switch (type)
        {
            case 0:
                bulletTransf.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                bulletTransf.eulerAngles = new Vector3(0, 0, 180);
                break;
        }
    }

    // Checks that the bullet is within the screen boundaries
    // Portrait orientation checks the Y-axis
    // Landscape orientation checks the X-axis
    private void _CheckBounds()
    {
        if (bulletTransf.anchoredPosition.y > verticalBoundary || bulletTransf.anchoredPosition.y < -verticalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    // Handles the collision with other collidable objects
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name != CreatorObject)
        {
            if (other.gameObject.GetComponent<IDestructible>() != null)
            {
                if (other.gameObject.GetComponent<EnemyController>() != null)
                {
                    other.gameObject.GetComponent<EnemyController>().Health -= ApplyDamage();
                    if (other.gameObject.GetComponent<EnemyController>().Health <= 0)
                    {
                        other.gameObject.SetActive(false);
                    }
                    SoundManager.PlaySound("hit_1");
                }
                else if(other.gameObject.GetComponent<PlayerController>() != null)
                {
                    other.gameObject.GetComponent<PlayerController>().Health -= ApplyDamage();
                    if (other.gameObject.GetComponent<PlayerController>().Health <= 0)
                    {
                        other.gameObject.SetActive(false);                        
                        SceneManager.LoadScene("GameOver");
                    }
                    SoundManager.PlaySound("hit_1");
                }
               
                bulletManager.ReturnBullet(gameObject);
            }
        }
    }

    public int ApplyDamage()
    {
        return damage;
    }
}
