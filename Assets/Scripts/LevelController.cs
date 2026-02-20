using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelController : Singleton<LevelController>
{
    [SerializeField]
    GameObject _levelButtonPrefab;

    [SerializeField]
    GameObject _content;

    private List<ChangeLevelButton> _AlllvlIndex = new();

    private void Start() { }

    public void CreateButtonLevel(int level)
    {
        var levelbutton = Instantiate(_levelButtonPrefab, _content.transform);
        var lvltext = levelbutton.GetComponentInChildren<Text>();
        lvltext.text = level.ToString();
        var setIndex = levelbutton.GetComponent<ChangeLevelButton>();
        setIndex.indexLvl = level;
        _AlllvlIndex.Add(setIndex);
        if (YG2.saves.levelKillList.Count < level)
        {
            YG2.saves.levelKillList.Add(0);
        }
    }

    private int GetNextLevel(int lastlevel)
    {
        var level = lastlevel + 1;
        return level;
    }

    public void OpenNewLevel()
    {
        CreateButtonLevel(GetNextLevel(BasicManager.Instance.LastLevel));
        //   YG2.SetLeaderboard("Level", YG2.saves.maxLvl);
        YG2.GetLeaderboard("Level");
    }

    public void LoadAllLevel()
    {
        for (int i = 1; i <= YG2.saves.maxLvl; i++)
        {
            CreateButtonLevel(i);
        }

        SetLevel(YG2.saves.currentLvl);
    }

    public void SetLevel(int level)
    {
        _AlllvlIndex[BasicManager.Instance.CurrentLevel - 1].select.gameObject.SetActive(false);
        BasicManager.Instance.StartLevel(level);
        BasicManager.Instance.CurrentLevel = level;
        _AlllvlIndex[BasicManager.Instance.CurrentLevel - 1].select.gameObject.SetActive(true);
    }
}
