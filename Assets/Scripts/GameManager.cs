using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy1, enemy2, enemy3, enemy4, enemy5, menuButton, restartButton, nextLevelButton, pause;
    [SerializeField] private TMPro.TextMeshProUGUI timeText, gameOverText;
    public Camera mainCamera, leftCamera, rightCamera;
    [SerializeField] private AudioClip crashSound;
    private AudioSource audioSource;
    public Animator anim;
    private bool isSpawning = true;
    private bool isPaused = false;
    private bool isGameOver = false;
    public int timeLeft = 12;
    private Vector3 startingPos1, startingPos2, startingPos3, startingPos4, startingPos5;
    private DetectCollision detectCollision;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        detectCollision = FindObjectOfType<DetectCollision>();

        startingPos1 = enemy1.transform.position;
        startingPos2 = enemy2.transform.position;
        startingPos3 = enemy3.transform.position;
        startingPos4 = enemy4.transform.position;
        startingPos5 = enemy5.transform.position; 

        StartCoroutine(Countdown());
        StartCoroutine(SpawnEnemies());

        anim.SetBool("isDrive", false);
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !isGameOver)
            { 
                Resume(); 
            }

            else if(!isPaused && !isGameOver)
            { 
                Pause(); 
            }
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isDrive", true);
        }
        else
        {
            anim.SetBool("isDrive", false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            mainCamera.enabled = false;
            leftCamera.enabled = true;
            rightCamera.enabled = false;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            mainCamera.enabled = false;
            leftCamera.enabled = false;
            rightCamera.enabled = true;
        }
        else 
        {
            mainCamera.enabled = true;
            leftCamera.enabled = false;
            rightCamera.enabled = false;
        } 
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            timeText.SetText("Time Left: " + timeLeft);
        }
        isSpawning = false;
        GameOver();
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(5);
            Instantiate(enemy1, startingPos1, enemy1.transform.rotation);
            Instantiate(enemy2, startingPos2, enemy2.transform.rotation);
            Instantiate(enemy3, startingPos3, enemy3.transform.rotation);
            Instantiate(enemy4, startingPos4, enemy4.transform.rotation);
            Instantiate(enemy5, startingPos5, enemy5.transform.rotation);
        }
    }

    public void GameOver()
    {
        if(detectCollision.win) 
        {
            gameOverText.SetText("You Won!");
            menuButton.SetActive(true);
            restartButton.SetActive(true);
            nextLevelButton.SetActive(true);
        }

        if(!detectCollision.win)
        {
            gameOverText.SetText("Game Over!");
            menuButton.SetActive(true);
            restartButton.SetActive(true);
            audioSource.PlayOneShot(crashSound, 0.8f);
        }
        isGameOver = true;
        Time.timeScale = 0;
    }

    public void Pause()
    {
        pause.SetActive(true);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pause.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }
}
