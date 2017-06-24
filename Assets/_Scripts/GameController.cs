using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float coinSpawnWait;
    public float coinStartWait;
    public float coinWaveWait;

    public GameObject enemyPlane;
    public int enemyCount;
    public int currentEnemyCount;
    public float enemySpawnWait;
    public float enemyStartWait;
    public float enemyWaveWait;


    public Text scoreText;
    public Text enemyText;
    public bool gameOver;


    private int score;
    private TextMesh highScoreText;
    private AutoFly_Advanced flyerScript;
    private Coroutine coinCourutine;
    private Coroutine enemyCourutine;

    private GameObject menuObject;
    private GameObject highScoresObject;
    public static bool restart = false;


    // Use this for initialization
    void Start()
    {
        flyerScript = GameObject.FindWithTag("Player").GetComponent<AutoFly_Advanced>();
        menuObject = GameObject.FindWithTag("MenuObject");
        highScoresObject = GameObject.FindWithTag("HighScoreBoard");
        highScoreText = GameObject.FindWithTag("HighScoreHolder").GetComponent<TextMesh>();
        //game over will be set to false when calling StartGame method
        gameOver = true;
        score = 0;
        currentEnemyCount = 0;
        highScoresObject.SetActive(false);

        if (restart)
        {
            //just start the game without showing the menu
            this.startGame();
            //hide the Menu
            menuObject.SetActive(false);
        }
        else
        {
            //show the START BUTTON
            menuObject.transform.GetChild(0).gameObject.SetActive(true);
            //hide the RESTART BUTTON
            menuObject.transform.GetChild(1).gameObject.SetActive(false);
            //show the Menu
            menuObject.SetActive(true);
            scoreText.text = "";
            enemyText.text = "";

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }

        if (gameOver)
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
                // //stop the actual if runing
                //StopCoroutine(myCoroutine);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                startGame();
            }
        }
    }

    IEnumerator coinSpawnWaves()
    {
        yield return new WaitForSeconds(coinStartWait);

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

                yield return new WaitForSeconds(coinSpawnWait);
            }

            yield return new WaitForSeconds(coinWaveWait);

            if (gameOver)
            {
                break;
            }
        }



    }

    IEnumerator enemySpawnWaves()
    {
        yield return new WaitForSeconds(enemyStartWait);

        while (true)
        {
            if(currentEnemyCount == 0)
            {         
                for (int i = 0; i < enemyCount; i++)
                {

                    if (gameOver)
                    {
                        break;
                    }

                    Vector3 spawnPosition;
                    currentEnemyCount++;
                    updateEnemyCount();

                    //alternate between spawning form top/down and from left/right
                    if (i % 2 == 0)
                    {
                        int xPos;
                        //will the plane sapwn left or right

                        if (Random.value < 0.5f)
                            xPos = Xstart-20;
                        else
                            xPos = Xend+20;

                         spawnPosition = new Vector3(
                            xPos,
                            Random.Range(Ystart + 10, Yend + 10),
                            Random.Range(Zstart, Zend)
                        );
                    }
                    else
                    {
                        int zPos;
                        //will the plane sapwn top or bottom

                        if (Random.value < 0.5f)
                            zPos = Zstart-20;
                        else
                            zPos = Zend+20;

                         spawnPosition = new Vector3(
                            Random.Range(Xstart, Xend),
                            Random.Range(Ystart + 10, Yend + 10),
                            zPos
                        );
                    }


                    Quaternion spawnRotation = Quaternion.identity;

                    Instantiate(enemyPlane, spawnPosition, spawnRotation);

                    yield return new WaitForSeconds(enemySpawnWait);
                }
            }

            yield return new WaitForSeconds(enemyWaveWait);

            if (gameOver)
            {
                break;
            }
        }



    }

    public void startGame()
    {
        updateScore();
        updateEnemyCount();
        gameOver = false;
        menuObject.SetActive(false);
        highScoresObject.SetActive(false);

        flyerScript.startFlying();
        // we can repeat with this script
        // InvokeRepeating("SpawnWaves", 1,2);

        //start coroutune and save it in variable
        coinCourutine = StartCoroutine(coinSpawnWaves());
        enemyCourutine = StartCoroutine(enemySpawnWaves());
    }
    public void restartGame()
    {
        restart = true;
        menuObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOver = true;

        //hide the START BUTTON
        menuObject.transform.GetChild(0).gameObject.SetActive(false);
        //show the RESTART BUTTON
        menuObject.transform.GetChild(1).gameObject.SetActive(true);
        //show the Menu
        menuObject.SetActive(true);

        scoreText.text = "Final Score: " + score+ " " ;
        enemyText.text = "";

        SaveScore();

    }

    public void ExitGame()
    {
       restart = false;

        // DELETE THIS WHEN BUILDING FOR ANDROID
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif


        Application.Quit();
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score;
        
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

    public void updateEnemyCount()
    {
        enemyText.text = "Enemies: " + currentEnemyCount;
    }

    public void ShowHighScores()
    {
        highScoresObject.SetActive(true);

        List<string> highScores = GetScores();

        highScoreText.text = "Highscores:\n";

        foreach (var str in highScores.Take(5))
        {
            highScoreText.text += str.PadLeft(3, ' ') + " points\n";
        }
      
    }

    public void SaveScore()
    {
        //get the curent highscores and add the new one
        List<string> highScores = GetScores();
        highScores.Add(score.ToString());

        //sort them and puth them back
        var sortedScores = highScores.OrderByDescending(x => int.Parse(x)).ToList();
        string stringScores = string.Join(",", sortedScores.ToArray());

        PlayerPrefs.SetString("highScores", stringScores);

    }
    public List<string> GetScores()
    {
        //get the currentHigh scores as string and parse them to list
        var allScores = PlayerPrefs.GetString("highScores");
        List<string> highScores = new List<string>();

        if (!string.IsNullOrEmpty(allScores))
        {
            highScores = allScores.Split(',').ToList();
        }
        return highScores;
    }

}
