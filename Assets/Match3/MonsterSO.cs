using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MonsterSO")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;
    public string ID;
    public int level;
    public Monster.MonsterType type;
    public MonsterJob job;
    public int star;
    public int baseHP;
    public int baseATK;
    public int baseDEF;
    public int baseSpeed;
    public int baseER;
    public int baseEnergy;
    public int baseHR;
    public int baseCritRate;
    public int baseCritDame;
}
public enum MonsterJob
    {
        DPS = 1,
        Tanker = 2 ,
        Supporter = 3,
    }
