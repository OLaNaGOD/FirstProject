using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text _timer;
    private float _time = 30f;

    private void Start()
    {
        _timer = GetComponent<Text>();
    }

    private void Update()
    {
        if (_time <= 0f)
        {
            BasicManager.Instance.RestartLevel();
        }
        _timer.text = _time.ToString("F1");
        _time -= Time.deltaTime;
    }
}
