using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToThrowFruits : MonoBehaviour
{
    public GameObject[] threwFruits;
    public GameObject threwBombs;
    public float min = 0.3f;
    public float max = 1.0f;
    public float forceMin = 12;
    public float forceMax = 17;

    public float minDelay = 1.0f; // Retraso inicial en segundos

    public Transform[] throwingAreas;

    public AudioSource soundGame;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartThrowing());
        this.soundGame.Play();
    }

    private IEnumerator StartThrowing()
    {
        yield return new WaitForSeconds(minDelay);

        StartCoroutine(Thrower());
    }

    private IEnumerator Thrower()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));

            Transform t = throwingAreas[UnityEngine.Random.Range(0, throwingAreas.Length)];

            GameObject go = null;

            float p = UnityEngine.Random.Range(0, 100);

            if (p < 10)
            {
                go = threwBombs;
            }
            else
            {
                go = threwFruits[UnityEngine.Random.Range(0, threwFruits.Length)];
            }

            GameObject fruit = Instantiate(go, t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * UnityEngine.Random.Range(forceMin, forceMax), ForceMode2D.Impulse);

            Destroy(fruit, 5);
        }
    }
}
