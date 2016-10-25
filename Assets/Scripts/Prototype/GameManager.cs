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
    private MegaManController player;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        player = FindObjectOfType<MegaManController>();

        InitializeHearts();
	}

    // Update is called once per frame
    void Update()
    {
        GameOverVerification();
	}

    private void InitializeHearts()
    {
        currentHeart = player.hp;

        for (int i = hearts.Length - 1; i >= currentHeart; i--)
        {
            hearts[i].SetActive(false);
        }
    }

    public void RemoveHP(int hp)
    {
        for (int i = currentHeart - 1; i >= hp; i--)
        {
            if (i >= 0)
            {
                hearts[i].GetComponent<Image>().sprite = heartOff;
            }
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
