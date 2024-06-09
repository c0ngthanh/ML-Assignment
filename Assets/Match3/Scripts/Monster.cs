using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterType
    {
        Fire = 1,
        Water = 2,
        Grass = 3,
        Electric = 4,
        Light = 5,
        Dark = 6
    }
    [SerializeField] private MonsterSO monsterSO;
    private int baseHP;
    private int baseLevel;
    private MonsterJob job;
    private int baseStar;
    private string ID;
    private MonsterType type;
    private int baseATK;
    private int baseDEF;
    private int baseSpeed;
    private int baseER;
    private int baseEnergy;
    private int baseHR;
    private int baseCritRate;
    private int baseCritDame;
    private int HP;
    private int ATK;
    private int level;
    private int star;
    private int DEF;
    private int energy;
    private int speed;
    private int ER;
    private int HR;
    private int critRate;
    private int critDame;
    private int maxEnergy;
    private int maxHP;
    private float ATK_INCREASE_RATE = 0.1f;
    private float DEF_INCREASE_RATE = 0.1f;
    private float HEAL_INCREASE_RATE = 0.05f;
    private float ENERGY_INCREASE_RATE = 5;
    public Monster(MonsterSO so)
    {
        monsterSO = so;
        baseHP = so.baseHP;
        baseLevel = so.level;
        job = so.job;
        baseStar = so.star;
        ID = so.ID;
        type = so.type;
        baseATK = so.baseATK;
        baseDEF = so.baseDEF;
        baseSpeed = so.baseSpeed;
        baseER = so.baseER;
        baseEnergy = so.baseEnergy;
        baseHR = so.baseHR;
        baseCritRate = so.baseCritRate;
        baseCritDame = so.baseCritDame;
        HP = so.baseHP;
        ATK = so.baseATK;
        level = so.level;
        star = so.star;
        DEF = so.baseDEF;
        energy = 0;
        speed = so.baseSpeed;
        ER = so.baseER;
        HR = so.baseHR;
        critRate = so.baseCritRate;
        critDame = so.baseCritDame;
        maxEnergy = so.baseEnergy;
        maxHP = so.baseHP;
    }
    public void UpdateStats(Dictionary<GemSO, int> gemSOList)
    {
        foreach (KeyValuePair<GemSO, int> item in gemSOList)
        {
            if (item.Value == 0)
            {
                continue;
            }
            switch (item.Key.gemType)
            {
                case GemSO.Type.ATK:
                    ATK += (int)(baseATK * ATK_INCREASE_RATE * item.Value);
                    break;
                case GemSO.Type.DEF:
                    DEF += (int)(baseDEF * DEF_INCREASE_RATE * item.Value);
                    break;
                case GemSO.Type.HEAL:
                    HP += (int)(baseHP * HEAL_INCREASE_RATE * item.Value);
                    if (HP > baseHP)
                    {
                        HP = baseHP;
                    }
                    break;
                case GemSO.Type.ENERGY:
                    energy += (int)(ENERGY_INCREASE_RATE * item.Value * ER / 100);
                    if (energy > baseEnergy)
                    {
                        energy = baseEnergy;
                    }
                    break;
                case GemSO.Type.ELEMENT:
                    BattleManager.Instance.SetElement(true);
                    break;
            }
        }
    }
    public int GetMonsterHP()
    {
        return HP;
    }
    public int GetMonsterDef()
    {
        return DEF;
    }
    public int GetMonsterCritRate()
    {
        return critRate;
    }
    public int GetMonsterCritDame()
    {
        return critDame;
    }
    public int GetMonsterATK()
    {
        return ATK;
    }
    public int GetMonsterEnergy()
    {
        return energy;
    }
    public int GetMonsterMaxEnergy()
    {
        return baseEnergy;
    }
    public int GetMonsterMaxHP()
    {
        return baseHP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    public MonsterType GetMonsterType()
    {
        return type;
    }
    public MonsterSO GetMonsterSO()
    {
        return monsterSO;
    }
    public void SetMonsterSO(MonsterSO value)
    {
        monsterSO = value;
    }

    internal void SetMonsterHP(float value)
    {
        this.HP = (int)value;
    }
}
