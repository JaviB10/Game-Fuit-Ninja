using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    public GameObject prefabCutBomb;

    public void CreateCutFruit()
    {
        GameObject inst = (GameObject)Instantiate(prefabCutBomb, transform.position, transform.rotation);

        Rigidbody[] rbsCuts = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbsCuts)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

        FindObjectOfType<GameManager>().DecreaseScore();

        Destroy(inst.gameObject, 5);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sword e = collision.GetComponent<Sword>();

        if (!e)
        {
            return;
        }

        CreateCutFruit();

        FindObjectOfType<GameManager>().ToTouchBomb();
    }
}
