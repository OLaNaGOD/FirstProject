using System;
using System.Collections;
using UnityEngine;

public interface IAbilites
{
    public void UseAbilities();
    public bool BuyAbilities();
    public IEnumerator CoolDown();
    public IEnumerator AbilitieStart(Action ability);
}

public enum StateAbilities
{
    Closed = 0,
    Awalibale = 1,
    CoolDown = 2,
}
