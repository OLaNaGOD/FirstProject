using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        public int currentLvl = 1;
        public int maxLvl = 1;
        public List<int> levelKillList = new List<int>();
        public double currentTree = 0d;
        public int itemsCount = 1;
        public List<int> lvlItem = new List<int>();
        public List<int> lvlAwakening = new List<int>();
        public StateAbilities[] abilities = new StateAbilities[8];
        public float[] abilitiesCooldown = new float[8];
        public bool[] achivments = new bool[16];
        public int ascensionLvl = 1;
        public long serverTime;

        ///////////////////////////////////////////////////////
        ///ACHIVMENT///
        ///////////////////////////////////////////////////////
        public int deathEnemyCount;
        public int bestLvlItem;
        public int bestAwakeningLvl;
        public int deathBossCount;
        public int abilityCount;
    }
}
