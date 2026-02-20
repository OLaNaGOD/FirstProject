using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AchivmentController : Singleton<AchivmentController>
{
    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    GameObject _transform;

    [SerializeField]
    GameObject _pupapPrefab;

    [SerializeField]
    GameObject _pupapTransform;

    public GameObject obj;

    public AchivmentsObject objSa;

    private List<HelpTextAchivments> _list = new();

    public event Action OnAchivmentCheker;

    private void Start() { }

    public void Init()
    {
        for (int i = 0; i < objSa.achivmentsList.Length; i++)
        {
            var r = Instantiate(_prefab, _transform.transform);
            var c = r.GetComponentInChildren<HelpTextAchivments>();
            c.Index = i;
            _list.Add(c);
            //  Debug.Log(c.Index);
        }
        OnAchivmentCheker += AchivmentCheker;
    }

    public void InvokeAchivmentMessage(int id)
    {
        Instantiate(_pupapPrefab, _pupapTransform.transform);
    }

    public void InvokeAction()
    {
        OnAchivmentCheker?.Invoke();
    }

    public void AchivmentCheker()
    {
        Achivment1();
        Achivment2();
        Achivment3();
        Achivment4();
        Achivment5();
        Achivment6();
        Achivment7();
        Achivment8();
        Achivment9();
        Achivment10();
        Achivment11();
        Achivment12();
        Achivment13();
        Achivment14();
        Achivment15();
        Achivment16();
    }

    private void Achivment1()
    {
        if (_list[0].Active != true && YG2.saves.deathEnemyCount > 0)
        {
            InvokeAchivmentMessage(0);
            _list[0].Active = true;
        }
    }

    private void Achivment2()
    {
        if (_list[1].Active != true && YG2.saves.bestLvlItem >= 1)
        {
            InvokeAchivmentMessage(0);
            _list[1].Active = true;
        }
    }

    private void Achivment3()
    {
        if (_list[2].Active != true && YG2.saves.bestAwakeningLvl >= 1)
        {
            InvokeAchivmentMessage(0);
            _list[2].Active = true;
        }
    }

    private void Achivment4()
    {
        if (_list[3].Active != true && YG2.saves.deathBossCount >= 1)
        {
            InvokeAchivmentMessage(0);
            _list[3].Active = true;
        }
    }

    private void Achivment5()
    {
        if (_list[4].Active != true && YG2.saves.currentTree >= 100000)
        {
            InvokeAchivmentMessage(0);
            _list[4].Active = true;
        }
    }

    private void Achivment6()
    {
        if (_list[5].Active != true && YG2.saves.bestLvlItem >= 100)
        {
            InvokeAchivmentMessage(0);
            _list[5].Active = true;
        }
    }

    private void Achivment7()
    {
        if (_list[6].Active != true && YG2.saves.deathEnemyCount >= 1000)
        {
            InvokeAchivmentMessage(0);
            _list[6].Active = true;
        }
    }

    private void Achivment8()
    {
        if (_list[7].Active != true && YG2.saves.deathBossCount >= 100)
        {
            InvokeAchivmentMessage(0);
            _list[7].Active = true;
        }
        Debug.Log(YG2.saves.deathBossCount);
    }

    private void Achivment9()
    {
        if (_list[8].Active != true && YG2.saves.abilityCount == 8)
        {
            InvokeAchivmentMessage(0);
            _list[8].Active = true;
        }
    }

    private void Achivment10()
    {
        if (_list[9].Active != true && YG2.saves.maxLvl >= 50)
        {
            InvokeAchivmentMessage(0);
            _list[9].Active = true;
        }
    }

    private void Achivment11()
    {
        if (_list[10].Active != true && YG2.saves.maxLvl >= 100)
        {
            InvokeAchivmentMessage(0);
            _list[10].Active = true;
        }
    }

    private void Achivment12()
    {
        if (_list[11].Active != true && YG2.saves.ascensionLvl > 1)
        {
            InvokeAchivmentMessage(0);
            _list[11].Active = true;
        }
    }

    private void Achivment13()
    {
        if (_list[12].Active != true && YG2.saves.itemsCount == 30)
        {
            InvokeAchivmentMessage(0);
            _list[12].Active = true;
        }
    }

    private void Achivment14()
    {
        if (_list[13].Active != true && YG2.saves.bestAwakeningLvl >= 30)
        {
            InvokeAchivmentMessage(0);
            _list[13].Active = true;
        }
    }

    private void Achivment15()
    {
        if (_list[14].Active != true && YG2.saves.maxLvl >= 200)
        {
            InvokeAchivmentMessage(0);
            _list[14].Active = true;
        }
    }

    private void Achivment16()
    {
        if (_list[15].Active != true && YG2.saves.maxLvl >= 200 && YG2.saves.itemsCount == 1)
        {
            InvokeAchivmentMessage(0);
            _list[15].Active = true;
        }
    }
}
