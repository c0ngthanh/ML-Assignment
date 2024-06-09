using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public Image image1;
    public Image image2;
    public MonsterSO monsterSO1;
    public MonsterSO monsterSO2;
    public Match3 match3;
    private bool isMonster1Attack = true;

    public EventHandler OnGameOver;
    public EventHandler OnAttack;
    private Monster monster1;
    private Monster monster2;
    private bool isElement = false;
    public bool isWin;
    public int winRound;
    public int loseRound;

    private void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        winRound =0;
        loseRound =0;
        float scale;
        monster1 = new Monster(monsterSO1); 
        monster2 = new Monster(monsterSO2); 

        image1.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO1.monsterName);
        image1.SetNativeSize();
        scale = image1.GetComponent<RectTransform>().rect.width/image1.GetComponent<RectTransform>().rect.height;
        image1.GetComponent<RectTransform>().sizeDelta = new Vector2(250*scale,250);

        image2.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO2.monsterName);
        image2.SetNativeSize();
        scale = image2.GetComponent<RectTransform>().rect.width/image2.GetComponent<RectTransform>().rect.height;
        image2.GetComponent<RectTransform>().sizeDelta = new Vector2(250*scale,250);
    }
    public void Battle(){
        if(isMonster1Attack){
            Attack(monster1, monster2);
            isMonster1Attack = false;
        }else{
            Attack(monster2, monster1);
            isMonster1Attack = true;
        }
    }
    public void InvokeGameOver(){
        OnGameOver?.Invoke(this,EventArgs.Empty);
    }

    private void Attack(Monster monsterAtk, Monster monsterAtked)
    {
        monsterAtk.UpdateStats(match3.totalGemClear);
        BattleDamage(monsterAtk,monsterAtked);
        isElement = false;
        // Debug.Log(monsterAtk.GetMonsterSO() + " attack " + monsterAtked.GetMonsterSO() + " ATK: " + monsterAtk.GetMonsterATK() + " HP: " +monsterAtked.GetMonsterHP());
        foreach (KeyValuePair<GemSO,int> item in match3.totalGemClear)
        {
            if(item.Key.gemType == GemSO.Type.ATK){
                // Debug.Log("ATK Gem: " + item.Value);
            }   
        }
        match3.ClearDictionary();
        OnAttack.Invoke(this,EventArgs.Empty);
    }
    public void BattleDamage(Monster AttackMonster, Monster AttackedMonster)
    {
        float effectiveBonus = GameConfig.baseCounterBonus;
        if (isElement)
        {
            effectiveBonus = GetEffectiveBonus(AttackMonster.GetMonsterType(), AttackedMonster.GetMonsterType());
        }
        int monsterDEF = AttackedMonster.GetMonsterDef();
        float attackDame = AttackMonster.GetMonsterATK() * effectiveBonus * (1 - (monsterDEF / (monsterDEF + 100f)));
        if (AttackMonster.GetMonsterCritRate() > UnityEngine.Random.Range(0, 100))
        {
            attackDame = attackDame * AttackedMonster.GetMonsterCritDame() / 100;
        }
        AttackedMonster.SetMonsterHP(AttackedMonster.GetMonsterHP() - attackDame);
        // if(AttackedMonster.GetMonsterHP() <= 0){
        //     AttackedMonster.GetComponent<CharacterBattle>().isDead = true;
        // }
    }
    private float GetEffectiveBonus(Monster.MonsterType AttackMonsterType, Monster.MonsterType AttackedMonsterType)
    {
        switch (AttackMonsterType)
        {
            case Monster.MonsterType.Fire:
                if (AttackedMonsterType == Monster.MonsterType.Grass)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Water)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Water:
                if (AttackedMonsterType == Monster.MonsterType.Fire)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Grass || AttackedMonsterType == Monster.MonsterType.Electric)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Grass:
                if (AttackedMonsterType == Monster.MonsterType.Water || AttackedMonsterType == Monster.MonsterType.Electric)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Fire)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Electric:
                if (AttackedMonsterType == Monster.MonsterType.Water)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Grass)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Dark:
                if (AttackedMonsterType == Monster.MonsterType.Light)
                {
                    return GameConfig.effectiveBonus;
                }
                break;
            case Monster.MonsterType.Light:
                if (AttackedMonsterType == Monster.MonsterType.Dark)
                {
                    return GameConfig.effectiveBonus;
                }
                break;
        }
        return GameConfig.baseCounterBonus;
    }

    public void SetElement(bool element)
    {
        this.isElement = element;
    }
    public bool TryGetGameOver(){
        if(monster1.GetMonsterHP() <=0){
            loseRound +=1;
            Debug.Log("Lose");
            // Debug.Log("Win round: " + winRound + " lose round: " + loseRound);
            isWin = false;
            return true;
        }else if(monster2.GetMonsterHP()<=0){
            winRound +=1;
            Debug.Log("Win");
            // Debug.Log("Win round: " + winRound + " lose round: " + loseRound);
            isWin = true;
            return true;
        }
        return false;
    }
    public float GetMonsterHPDif(){
        return monster1.GetMonsterHP()/monster1.GetMaxHP() - monster2.GetMonsterHP()/monster2.GetMaxHP();
    }
}
