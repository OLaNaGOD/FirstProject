using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TreePrefabText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PopUpTextTree());
    }

    private IEnumerator PopUpTextTree()
    {
        var time = 2f;
        var position = new Vector3(Random.Range(-0.3f, 0.3f), 0.2f, 0f);
        while (time >= 0)
        {
            transform.position += position;
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
