using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AscensionScript : MonoBehaviour
{
    [SerializeField]
    Button _yes;

    [SerializeField]
    Button _no;

    [SerializeField]
    Text _count;

    const double VALUE = 10000000d;
    public int AscensionLvl
    {
        get { return YG2.saves.ascensionLvl; }
        private set { YG2.saves.ascensionLvl = value; }
    }

    private void Start()
    {
        _yes.onClick.AddListener(Yes);
        _no.onClick.AddListener(No);
        _count.text = ValueConverterStatic.DoubleToString(Price());
    }

    private void Update()
    {
        if (Price() <= BasicManager.Instance.TreeCount)
        {
            _yes.interactable = true;
        }
        else
        {
            _yes.interactable = false;
        }
    }

    private void Yes()
    {
        AscensionLvl++;
        BasicManager.Instance.TreeCount -= Price();
        AchivmentController.Instance.InvokeAction();
        SaveController.Instance.LoadDefaultData();
    }

    private void No()
    {
        this.gameObject.SetActive(false);
    }

    private double Price()
    {
        double price;
        price = VALUE * Math.Pow(10, AscensionLvl);
        return price;
    }
}
