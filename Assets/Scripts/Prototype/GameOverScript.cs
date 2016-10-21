using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.GameOver();
        }
    }
}
