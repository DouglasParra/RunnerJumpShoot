using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Metros: " + Mathf.Round(scoreCount);
        highScoreText.text = "Distância Máxima: " + Mathf.Round(highScoreCount);

	}

    public void AddScore(int pointsToAdd){
        scoreCount += pointsToAdd;
    }
}
