using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject coin;
    public int Xstart;
    public int Xend;
    public int Zstart;
    public int Zend;
    public int Ystart;
    public int Yend;
    public int coinCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public bool gameOver;

    private int score;
    private AutoFly_Advanced flyerScript;
    private Coroutine myCoroutine;


    // Use this for initialization
    void Start()
    {
        flyerScript = GameObject.FindWithTag("Player").GetComponent<AutoFly_Advanced>();
        gameOver = false;
        score = 0;
        updateScore();

        // we can repeat with this script
        // InvokeRepeating("SpawnWaves", 1,2);
        
        //start coroutune and save it in variable
        myCoroutine = StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
                // //stop the actual if runing
                //StopCoroutine(myCoroutine);

                // //start new one.... THIS HAS TO BE TESTED...
                // myCoroutine = StartCoroutine(SpawnWaves());
                // flyerScript.speed  = 6;
                // score = 0;
                // updateScore();
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < coinCount; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(Xstart,Xend),
                    Random.Range(Ystart, Yend),
                    Random.Range(Zstart, Zend)
                );

                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(coin, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }



    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        flyerScript.speed += 0.3f;
        updateScore();
    }

    public void GameOver()
    {
        gameOver = true;
        scoreText.text = " GAME OVER !! " + score + "coins";
    }
}
