using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AchivmentsObject", menuName = "Scriptable Objects/AchivmentsObject")]
public class AchivmentsObject : ScriptableObject
{
    public AchivmentName[] achivmentsList;
}

[Serializable]
public class AchivmentName
{
    public string achivmentName_ru;
    public string achivmentName_en;
}
