using System.Collections;
using UnityEngine;

public class PupapAchivment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        var time = 3f;
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
