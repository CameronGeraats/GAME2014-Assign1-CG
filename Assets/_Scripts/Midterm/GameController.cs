using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
 * File: GameController.cs
 * Name: Cameron Geraats
 * ID: 100806837
 * Last Modified: 24/10/20
 * Description:
 *      Manages the game orientation and Camera size
 * Revisions: No previous revisions
 */
public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject canvas;

    private int count = 20;
    private List<GameObject> m_enemyPool;

    // Start is called before the first frame update
    // Sets up the private variables and modifies the camera size based on the device
    void Start()
    {
        m_enemyPool = new List<GameObject>();
        //Debug.Log("Start Orient: " + Screen.orientation);
        //screenOrient = Screen.orientation;
        //Debug.Log(Screen.safeArea);

        // Adjusts the camera size to reflect the current device screen size
        //Camera.main.orthographicSize = Screen.height / 2;
    }

    // Update is called once per frame
    // Invokes the Orientation change event if the orientation changes
    void Update()
    {        
        if (Time.frameCount % 600 == 0 && count > 0)
        {
            count--;
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.SetParent(canvas.transform);
            enemy.GetComponent<RectTransform>().localPosition = new Vector3(0,1200,0);
            enemy.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);            
            m_enemyPool.Add(enemy);
        }
        else if (count <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
