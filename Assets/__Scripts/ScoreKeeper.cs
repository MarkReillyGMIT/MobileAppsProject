using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    // Public Variables
    #region
    public static int score = 0;
    public GameObject scoreParticle;
    #endregion

    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        ResetScore();
        Score(0);
    }

    // Update is called once per frame
    void Update()
    {
        EndGame();
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public void Score(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score.ToString();

        if (scoreParticle)
        {
            //Instantiate(scoreParticle, transform.position, Quaternion.identity);

            GameObject particleScore = Instantiate(scoreParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
            ParticleSystem ps = particleScore.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = Color.yellow;
            Destroy(particleScore, 2f);
        }
    }

    public void EndGame()
    {
        if (score >= 2000)
        {
            SceneManager.LoadScene("Level_One_Complete");
        }
    }
}