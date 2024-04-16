using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speedMin = 0.1f;

    private Rigidbody2D rb;
    private Vector3 lastPositionMouse;
    private Vector3 speedMouse;

    private Collider2D col;
    public AudioSource soundFruit;
    public AudioSource soundBomb;
    public AudioSource soundSword;
    private bool isMoving = false;
    private GameManager gameManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameOverPanel.activeSelf)
        {
            // Verificar si el mouse se ha movido desde el último frame
            if (!isMoving && Input.GetAxis("Mouse X") != 0f && Input.GetAxis("Mouse Y") != 0f)
            {
                // El mouse comenzó a moverse, reproducir el sonido
                soundSword.Play();
                isMoving = true;
            }
            else if (isMoving && Input.GetAxis("Mouse X") == 0f && Input.GetAxis("Mouse Y") == 0f)
            {
                // El mouse dejó de moverse, detener el sonido
                soundSword.Stop();
                isMoving = false;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        col.enabled = MouseMovement();
        
        ConnectSowrdToMouse();
    }

    private void ConnectSowrdToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        rb.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private bool MouseMovement()
    {
        Vector3 positionMouse = transform.position;
        float movement = (lastPositionMouse - positionMouse).magnitude;
        lastPositionMouse = positionMouse;

        if(movement > speedMin)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bombs bombs = collision.GetComponent<Bombs>();
        if(!bombs) 
        {
            this.soundFruit.Play();
        }
        else if(bombs)
        {
            this.soundBomb.Play();
        }
        
    }
}
