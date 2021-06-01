using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uicodes : MonoBehaviour
{
    public GameObject healthbar;
    public float health, maxHealth = 100;
    public Text count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.transform.localScale = new Vector3(health / maxHealth, 1f, 1f);
        //print(health);
        if(health < 0)
        {
            SceneManager.LoadScene(2);
        }

        count.text = GameObject.Find("Terrain").GetComponent<spawner>().killed.ToString();
    }
}
