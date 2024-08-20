using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOnContact : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Won Game");
            winMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
