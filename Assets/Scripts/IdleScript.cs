using UnityEngine;
using UnityEngine.UI;
using YG;

public class IdleScript : Singleton<IdleScript>
{
    private long _currentTime;

    [SerializeField]
    GameObject _popUpBonusPrefab;

    [SerializeField]
    GameObject _canvas;

    private double _treeBonus;
    public double TreeBonus
    {
        get { return _treeBonus; }
    }

    public void Init()
    {
        _currentTime = YG2.ServerTime();
        if (YG2.saves.serverTime != 0 && YG2.saves.itemsCount > 1)
        {
            var value = (_currentTime - YG2.saves.serverTime) / 30000;
            Debug.Log(value);
            _treeBonus = value * BasicManager.Instance.AddTree(BasicManager.Instance.CurrentLevel);
            Instantiate(_popUpBonusPrefab, _canvas.transform);
            BasicManager.Instance.TreeCount += _treeBonus;
        }
    }
}
