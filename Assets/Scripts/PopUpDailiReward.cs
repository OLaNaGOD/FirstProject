using UnityEngine;
using UnityEngine.UI;

public class PopUpDailiReward : MonoBehaviour
{
    [SerializeField]
    Text _text;

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        _text.text = ValueConverterStatic.DoubleToString(IdleScript.Instance.TreeBonus);
    }
}
