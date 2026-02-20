using UnityEngine;
using UnityEngine.UI;
using YG;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField]
    Button _button;
    public int indexLvl;
    public Image select;

    void Start()
    {
        _button.onClick.AddListener(SetLevel);
        // Debug.Log(YG2.saves.maxLvl);
        // Debug.Log(BasicManager.Instance.LastLevel);
    }

    public void SetLevel()
    {
        LevelController.Instance.SetLevel(indexLvl);
        BasicManager.Instance.SetDeathText();
    }
}
