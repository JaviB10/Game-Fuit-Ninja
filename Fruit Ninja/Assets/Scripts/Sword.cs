using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ConnectSowrdToMouse();
    }

    private void ConnectSowrdToMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        rb.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
