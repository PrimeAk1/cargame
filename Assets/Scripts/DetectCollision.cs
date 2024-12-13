using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameManager gameManager;
    public bool win;
    [SerializeField] private AudioClip pickUpSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall")) 
        {
            win = false;
            gameManager.GameOver();
        }

        if(other.gameObject.CompareTag("Win")) 
        {
            win = true;
            gameManager.GameOver();
        }

        if(other.gameObject.CompareTag("Time")) 
        {
            gameManager.timeLeft += 3;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickUpSound, 0.8f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")) 
        {
            win = false;
            gameManager.GameOver();
        }
    }
}
