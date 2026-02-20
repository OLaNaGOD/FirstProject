using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveController : Singleton<SaveController>
{
    private float _saveTimer;
    private const float SaveDelay = 2f;

    public void SaveData()
    {
        YG2.saves.itemsCount = ShopController.Instance._items.Count;
        YG2.SaveProgress();
    }

    private void Update()
    {
        if (_saveTimer < SaveDelay)
        {
            _saveTimer += Time.deltaTime;
        }
        if (_saveTimer >= SaveDelay)
        {
            SaveData();
            YG2.saves.serverTime = YG2.ServerTime();
            _saveTimer = 0f;
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log("Ñבנמס סויגא");
        //    YG2.SetDefaultSaves();
        //    YG2.SaveProgress();

        //    SceneManager.LoadScene(0);
        //}

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    BasicManager.Instance.TreeCount += 9999999999999999999999999999d;
        //}

        if (Input.GetKeyDown(KeyCode.D))
        {
            YG2.SetDefaultSaves();
            Debug.Log("Ñויג");
            SaveData();
        }
    }

    private void Start()
    {
        YG2.SetDefaultSaves();
    }

    public void LoadData()
    {
        BasicManager.Instance.TreeCount = YG2.saves.currentTree;
        LevelController.Instance.LoadAllLevel();
        ShopController.Instance.LoadTools();
    }

    public void LoadDefaultData()
    {
        StopAllCoroutines();
        YG2.saves.currentLvl = 1;
        YG2.saves.maxLvl = 1;
        YG2.saves.levelKillList.Clear();
        YG2.saves.currentTree = 0d;
        YG2.saves.itemsCount = 1;
        YG2.saves.lvlItem.Clear();
        YG2.saves.lvlAwakening.Clear();
        Array.Fill(YG2.saves.abilities, StateAbilities.Closed);
        Array.Fill(YG2.saves.abilities, StateAbilities.Closed);

        Array.Clear(YG2.saves.abilitiesCooldown, 0, YG2.saves.abilitiesCooldown.Length);
        YG2.saves.deathEnemyCount = 0;
        YG2.saves.bestLvlItem = 0;
        YG2.saves.bestAwakeningLvl = 0;
        YG2.saves.deathBossCount = 0;
        YG2.saves.abilityCount = 0;
        YG2.saves.serverTime = 0;
        YG2.SaveProgress();
        SceneManager.LoadScene(0);
    }
}
