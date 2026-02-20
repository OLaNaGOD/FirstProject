using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public abstract class BaseAbilitie : MonoBehaviour
{
    [SerializeField]
    protected double _price;

    [SerializeField]
    protected int _id;

    [SerializeField]
    protected float _time;

    [SerializeField]
    protected float _coolDown;

    [SerializeField]
    protected Sprite _closedSprite;

    [SerializeField]
    protected Sprite _openSprite;
    protected Image _image;
    protected Text _timer;
    protected Button _buttonAbilitie;
    protected StateAbilities _state;

    private void Start()
    {
        _state = YG2.saves.abilities[_id];
        _image = GetComponent<Image>();
        _timer = GetComponentInChildren<Text>();
        _timer.gameObject.SetActive(false);
        _buttonAbilitie = GetComponent<Button>();
        _buttonAbilitie.onClick.AddListener(UseAbilities);
        CheckAbility();
    }

    private void CheckAbility()
    {
        switch (_state)
        {
            case StateAbilities.Closed:
                _image.sprite = _closedSprite;
                break;
            case StateAbilities.Awalibale:
                _image.sprite = _openSprite;
                break;
            case StateAbilities.CoolDown:
                _image.sprite = _openSprite;
                StartCoroutine(CoolDown());
                break;
        }
    }

    public abstract IEnumerator AbilitieStart();

    protected IEnumerator CoolDown()
    {
        _timer.gameObject.SetActive(true);

        _buttonAbilitie.interactable = false;
        if (YG2.saves.abilitiesCooldown[_id] <= 0f)
        {
            YG2.saves.abilitiesCooldown[_id] = _coolDown;
        }

        while (YG2.saves.abilitiesCooldown[_id] > 0)
        {
            int minutes = Mathf.FloorToInt(YG2.saves.abilitiesCooldown[_id] / 60f);
            int seconds = Mathf.FloorToInt(YG2.saves.abilitiesCooldown[_id] % 60f);
            _timer.text = $"{minutes:00}:{seconds:00}";
            YG2.saves.abilitiesCooldown[_id] -= Time.deltaTime;

            yield return null;
        }
        _state = StateAbilities.Awalibale;
        YG2.saves.abilities[_id] = _state;
        CheckAbility();
        _timer.gameObject.SetActive(false);

        _buttonAbilitie.interactable = true;
    }

    private void BuyAbilities()
    {
        if (BasicManager.Instance.TreeCount >= _price)
        {
            BasicManager.Instance.TreeCount -= _price;
            _state = StateAbilities.Awalibale;
            YG2.saves.abilities[_id] = _state;

            CheckAbility();
            YG2.saves.abilityCount++;
            AchivmentController.Instance.InvokeAction();
        }
    }

    private void UseAbilities()
    {
        switch (_state)
        {
            case StateAbilities.Closed:
                BuyAbilities();
                break;
            case StateAbilities.CoolDown:
                break;
            case StateAbilities.Awalibale:
                StartCoroutine(AbilitieStart());
                break;
        }
    }
}
