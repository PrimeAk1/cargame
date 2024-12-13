using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;             
    public float distanceToTravel = 12f; 
    private float distanceTraveled = 0f; 

    private Vector3 direction = Vector3.forward;  
    private Vector3 lastPosition;                 
    public Animator anim;
    private GameManager gameManager;

    void Start()
    {
        lastPosition = transform.position;  
        anim.SetBool("isForward", true);
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);

        
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);

        
        lastPosition = transform.position;

        
        if (distanceTraveled >= distanceToTravel)
        {
            direction = -direction;       
            distanceTraveled = 0f;        

            bool isMovingForward = anim.GetBool("isForward");
            anim.SetBool("isForward", !isMovingForward);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            gameManager.GameOver();
        }
    }
}
