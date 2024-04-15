using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject prefabCutFruit;

    public void CreateCutFruit()
    {
        GameObject inst = (GameObject)Instantiate(prefabCutFruit, transform.position, transform.rotation);

        Rigidbody[] rbsCuts = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody r in rbsCuts)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

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
        
    }
}
