using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/*
 * Updates scoreText
 */
public class ScoreKeeper : MonoBehaviour
{
    // Public Variables
    public static int score = 0;
    [SerializeField] GameObject scoreParticle;
    [SerializeField] float scoreComplete = 2000;
    [SerializeField] string nextLevel = "";

    //Private Variables
    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        ResetScore();
        Score(0);
    }
    //Calls EndGame every frame 
    void Update()
    {
        EndGame();
    }

    public static void ResetScore()
    {
        score = 0;
    }
    //Update score in game
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
    //End game when score is greater than the required score for completion of level
    public void EndGame()
    {
        if (score >= scoreComplete)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}