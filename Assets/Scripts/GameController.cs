using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    [SerializeField] private MenuController menuController;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] spawnPoints;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private TMP_Text highScoreText;
    private int score;
    private int lives = 3;
    public bool isPlaying = false;
    private float spawnRate = 3f;

    [SerializeField] private AudioClip lifeLostSound;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private AudioSource bgm;
    void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void StartGame()
    {
        if (!isPlaying)
        {
            bgm.Play();
            isPlaying = true;
            lives = 3;
            foreach (var enemy in FindObjectsOfType<EnemyController>())
            {
                Destroy(enemy.gameObject);
            }
            StartCoroutine(EnemySpawning());
        }
    }
    public void AddScore(int score)
    {
        print("adding score");
        this.score += score;
        scoreText.text = "Score: " + this.score;
        if(spawnRate >= 1f)
        {
            spawnRate -= 0.05f;
        }

    }
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            bgm.Stop();
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            isPlaying = false;
            StopAllCoroutines();
            endScoreText.text = "Score: " + score;
            if (score > PlayerPrefs.GetInt("highScore", 0))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);
            PlayerPrefs.Save();
            menuController.ShowEndScreen();
        }
        else
        {
            livesText.text = "Lives: " + lives;
            AudioSource.PlayClipAtPoint(lifeLostSound, transform.position);
        }
    }

    public IEnumerator EnemySpawning()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], 
                    spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, 
                    Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(EnemySpawning());
    }
}
