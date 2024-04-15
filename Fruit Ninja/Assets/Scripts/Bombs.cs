using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sword e = collision.GetComponent<Sword>();

        if (!e)
        {
            return;
        }

        FindObjectOfType<GameManager>().ToTouchBomb();
    }
}
