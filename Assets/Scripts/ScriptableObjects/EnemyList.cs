using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Scriptable Objects/EnemyList")]
public class EnemyList : ScriptableObject
{
    [SerializeField]
    public EnemyArray[] _lvlArray;
}

[Serializable]
public sealed class Enemy
{
    public string NameRu;
    public string NameEn;
    public Sprite Icon;
}

[Serializable]
public sealed class EnemyArray
{
    public Enemy[] _enemyArray;
}
