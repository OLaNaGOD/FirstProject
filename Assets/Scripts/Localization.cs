using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField]
    string _enText;
    Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Start()
    {
        if (YG2.lang == "en")
        {
            _text.text = _enText;
        }
    }
}
