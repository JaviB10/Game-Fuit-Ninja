using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speedMin = 0.1f;

    private Rigidbody2D rb;
    private Vector3 lastPositionMouse;
    private Vector3 speedMouse;

    private Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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
}
