using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToThrowFruits : MonoBehaviour
{
    public GameObject threwFruits;
    public float min = 0.3f;
    public float max = 1.0f;
    public float forceMin = 12;
    public float forceMax = 17;

    public Transform[] throwingAreas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Thrower());
    }

    private IEnumerator Thrower()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));

            Transform t = throwingAreas[UnityEngine.Random.Range(0, throwingAreas.Length)];

            GameObject fruit = Instantiate(threwFruits, t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * UnityEngine.Random.Range(forceMin, forceMax), ForceMode2D.Impulse);

            Destroy(fruit, 5);
        }
    }
}
