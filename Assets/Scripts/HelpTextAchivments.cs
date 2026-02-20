using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class HelpTextAchivments : BaseHelpText
{
    private int _index;
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            Active = YG2.saves.achivments[_index];
        }
    }

    [SerializeField]
    GameObject _achivmentCloseIcon;

    bool _active;

    [SerializeField]
    AchivmentsObject _achivmentsList;

    private void Start()
    {
        _obj = AchivmentController.Instance.obj;
        _text = _obj.GetComponent<Text>();
    }

    public override void SetText()
    {
        if (YG2.lang == "en")
        {
            _text.text = _achivmentsList.achivmentsList[Index].achivmentName_en;
        }
        else
            _text.text = _achivmentsList.achivmentsList[Index].achivmentName_ru;

        Debug.Log(Index);
    }

    public bool Active
    {
        get { return _active; }
        set
        {
            _active = value;
            CheckRecieved();
            YG2.saves.achivments[_index] = _active;
        }
    }

    void CheckRecieved()
    {
        if (Active)
        {
            _achivmentCloseIcon.gameObject.SetActive(false);
        }
    }
}
