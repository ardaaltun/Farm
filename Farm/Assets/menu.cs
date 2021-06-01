using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class menu : MonoBehaviour
{

    public GameObject PauseMenu;

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
