using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] hearts;
    public Sprite heartOn;
    public Sprite heartOff;

    public GameObject gameOverCanvas;

    private int currentHeart;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;

        // if(heartBoost) hearts.Length == 5; else hearts.Length == 3;

        currentHeart = hearts.Length;
	}
	
	// Update is called once per frame
	void Update () {
        GameOverVerification();
	}

    public void RemoveHP(int hp)
    {
        for (int i = currentHeart - 1; i >= hp; i--)
        {
            hearts[i].GetComponent<Image>().sprite = heartOff;
        }

        currentHeart = hp;
    }

    private void GameOverVerification()
    {
        if (hearts[0].GetComponent<Image>().sprite.name.Equals(heartOff.name))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

    public void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
