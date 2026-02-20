using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ADSController : Singleton<ADSController>
{
    [SerializeField]
    Button _buttonADS1;

    [SerializeField]
    Button _buttonADS2;

    [SerializeField]
    GameObject _timerAdsPrefab;

    [SerializeField]
    GameObject _canvasTransform;
    public bool freeModf = false;

    private void Start()
    {
        StartCoroutine(ShowADS());
        _buttonADS1.onClick.AddListener(ShowADSReward1);
        _buttonADS2.onClick.AddListener(ShowADSReward2);
    }

    public IEnumerator ShowADS()
    {
        while (true)
        {
            var time = 180f;
            while (time > 0)
            {
                //ebug.Log(time);
                time -= Time.deltaTime;
                yield return null;
            }

            Instantiate(_timerAdsPrefab);
        }
    }

    public void ShowADSReward1()
    {
        YG2.RewardedAdvShow("1");
        for (int i = 0; i < YG2.saves.abilitiesCooldown.Length; i++)
        {
            YG2.saves.abilitiesCooldown[i] = 0f;
        }
    }

    public void ShowADSReward2()
    {
        YG2.RewardedAdvShow("2");
        freeModf = true;
    }
}
