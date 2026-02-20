using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BasicManager : Singleton<BasicManager>
{
    [SerializeField]
    Button _clickButton;

    [SerializeField]
    Button _ascensionButton;

    [SerializeField]
    Text _ascensionButtonText;

    [SerializeField]
    GameObject _ascensionWindow;

    [SerializeField]
    Text _treeCounter;

    [SerializeField]
    Text _hpCounter;

    [SerializeField]
    Text _dpsText;

    [SerializeField]
    Text _clickdmgText;

    [SerializeField]
    Text _deathCounter;

    [SerializeField]
    Text _nameEnemy;

    [SerializeField]
    Image _hpBar;

    [SerializeField]
    GameObject _prefabPopUpText;

    [SerializeField]
    GameObject _transformPopUpText;

    [SerializeField]
    Image _blood;
    double _treeCount = 0;
    public double treeModif = 1;
    public double dpsModif = 1;

    [SerializeField]
    double _enemyHp;
    private bool isBoss;
    double _enemyMaxHp;
    double _clickDamage = 1d;
    public double critChance = 0.05d;
    public double critMultiplier = 2.0d;
    public double clickDmgModf = 1d;

    public double ClickDamage
    {
        get { return _clickDamage * clickDmgModf; }
        set
        {
            _clickDamage = value;
            _clickdmgText.text = ValueConverterStatic.DoubleToString(_clickDamage);
            ;
        }
    }

    double _autoDamage;
    public double AutoDamage
    {
        get { return _autoDamage; }
        set
        {
            _autoDamage = value;
            _dpsText.text = ValueConverterStatic.DoubleToString(_autoDamage);
        }
    }

    [SerializeField]
    EnemyList _enemyList;

    private GameObject _enemyObject;
    public GameObject EnemyObject
    {
        set
        {
            if (_enemyObject != null)
            {
                Destroy(_enemyObject);
            }
            ;
            _enemyObject = value;
        }
        get { return _enemyObject; }
    }
    private int _currentLevel = 1;

    public int LastLevel
    {
        set
        {
            YG2.saves.maxLvl = value;
            AchivmentController.Instance.InvokeAction();
        }
        get { return YG2.saves.maxLvl; }
    }
    public int CurrentLevel
    {
        set
        {
            _currentLevel = value;
            YG2.saves.currentLvl = _currentLevel;
            SetDeathText();
        }
        get { return _currentLevel; }
    }

    public float treeModfBonus = 0f;

    public int DefeatEnemyForLevel
    {
        set
        {
            YG2.saves.levelKillList[CurrentLevel] = value;
            SetDeathText();
        }
        get { return YG2.saves.levelKillList[CurrentLevel]; }
    }
    public double EnemyHp
    {
        set
        {
            _enemyHp = value;
            SetHpText();
            DeathEnemy();
        }
        get { return _enemyHp; }
    }
    public double TreeCount
    {
        set
        {
            _treeCount = value;
            _treeCounter.text = ValueConverterStatic.DoubleToString(_treeCount);
            YG2.saves.currentTree = _treeCount;
            AchivmentController.Instance.InvokeAction();
        }
        get { return _treeCount; }
    }

    private void SetHpText()
    {
        if (EnemyHp > 0)
        {
            _hpCounter.text = ValueConverterStatic.DoubleToString(_enemyHp);
        }
        else
        {
            _hpCounter.text = "0";
        }
    }

    public void SetDeathText()
    {
        if (IsBoss(CurrentLevel))
        {
            _deathCounter.text = DefeatEnemyForLevel.ToString() + "/1";
        }
        else
        {
            _deathCounter.text = DefeatEnemyForLevel.ToString() + "/10";
        }
    }

    private void Start()
    {
        _clickButton.onClick.AddListener(HitEnemy);
        _ascensionButton.onClick.AddListener(Ascension);
        //   LateInit();

        SaveController.Instance.LoadData();
        AchivmentController.Instance.Init();
        _ascensionButtonText.text =
            "X" + (YG2.saves.ascensionLvl * Math.Pow(2, YG2.saves.ascensionLvl - 1)).ToString();
        IdleScript.Instance.Init();
        Debug.Log(DefeatEnemyForLevel);
    }

    private void Update()
    {
        var ratio = _enemyHp / _enemyMaxHp;
        ratio = Mathf.Clamp01((float)ratio);
        var target = (float)ratio;
        _hpBar.fillAmount = Mathf.MoveTowards(_hpBar.fillAmount, target, Time.deltaTime * 2);

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Time.timeScale++;
        //    Debug.Log("TimeScale= " + Time.timeScale);
        //}

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Time.timeScale = 1f;
        //    Debug.Log("TimeScale= " + Time.timeScale);
        //}
    }

    public bool IsBoss(int level)
    {
        bool isBoss = false;
        if (level > 0 && level % 5 == 0)
        {
            isBoss = true;
        }

        return isBoss;
    }

    public void StartLevel(int level)
    {
        isBoss = IsBoss(level);
        _enemyMaxHp = (10 * (level - 1 + Mathf.Pow(1.55f, level - 1)) * (isBoss ? 10 : 1));
        EnemyHp = _enemyMaxHp;
        EnemyObject = EnemyController.Instance.SpawnEnemy(level, isBoss);
        _nameEnemy.text = EnemyController.Instance.GetName();
        AnimController.Instance.AnimObj = EnemyObject;
    }

    private void DeathEnemy()
    {
        if (EnemyHp <= 0)
        {
            TreeCount += AddTree(CurrentLevel);

            var r = Instantiate(_prefabPopUpText, _transformPopUpText.transform);
            var rb = r.GetComponent<Text>();
            rb.text = ValueConverterStatic.DoubleToString(AddTree(CurrentLevel));
            r.transform.position += new Vector3(
                UnityEngine.Random.Range(-100f, 100f),
                UnityEngine.Random.Range(-100f, 100f)
            );
            StartCoroutine(DeathCorutine());

            DefeatEnemyForLevel++;
            if (
                isBoss
                && YG2.saves.levelKillList[CurrentLevel - 1] >= 1
                && CurrentLevel == LastLevel
            )
            {
                LevelController.Instance.OpenNewLevel();
                LastLevel++;
            }
            else if (
                !isBoss
                && YG2.saves.levelKillList[CurrentLevel - 1] >= 10
                && CurrentLevel == LastLevel
            )
            {
                LevelController.Instance.OpenNewLevel();
                LastLevel++;
            }
            if (isBoss)
                YG2.saves.deathBossCount++;
            YG2.saves.deathEnemyCount++;
            AchivmentController.Instance.InvokeAction();
            AudioService.Instance.PlaySfxDeath();
        }
    }

    public void HitEnemy()
    {
        if (BasicManager.Instance.EnemyObject)
        {
            AnimController.Instance.SetTrigger();

            EnemyHp -= ClickDamage * CritModf();
            ClickTreeBonus();
        }
    }

    private void ClickTreeBonus()
    {
        TreeCount += AddTreeBonus(CurrentLevel) * treeModfBonus;
    }

    private double CritModf()
    {
        return UnityEngine.Random.value < critChance ? critMultiplier : 1.0;
    }

    private IEnumerator DeathCorutine()
    {
        WaitForSeconds _1sec = new WaitForSeconds(0.5f);

        Destroy(EnemyObject);
        _blood.gameObject.SetActive(true);
        yield return _1sec;
        _blood.gameObject.SetActive(false);
        RestartLevel();
        _hpBar.fillAmount = 1f;
    }

    public void RestartLevel()
    {
        StartLevel(CurrentLevel);
    }

    public double AddTree(int level)
    {
        return (Math.Ceiling(Math.Pow(1.56f, level - 1) * (IsBoss(level) ? 10 : 1)))
            * treeModif
            * (YG2.saves.ascensionLvl * Math.Pow(2, YG2.saves.ascensionLvl - 1));
    }

    private double AddTreeBonus(int level)
    {
        return (Math.Ceiling(Math.Pow(1.56d, level - 1) * (IsBoss(level) ? 10 : 1))) * treeModif;
    }

    private void Ascension()
    {
        _ascensionWindow.SetActive(true);
    }
}
