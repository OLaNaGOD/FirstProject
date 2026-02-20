using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TimeADS : MonoBehaviour
{
    [SerializeField]
    Text text;

    private void Start()
    {
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        var t = new WaitForSeconds(1f);

        text.text = "2";
        yield return t;

        text.text = "1";
        yield return t;

        YG2.InterstitialAdvShow();
        Destroy(gameObject);
    }
}
