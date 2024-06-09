using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleResult : MonoBehaviour
{
    // public static BattleResult Instance;
    private int winRound=0;
    private int loseRound=0;
    // private void Awake(){
    // }
    private void Awake(){
        winRound=0;
        loseRound=0;
        BattleManager.Instance.OnGameOver += ShowBattleResutl;
    }
    private void Start(){
    }
    private void ShowBattleResutl(object sender, EventArgs e)
    {
        if(BattleManager.Instance.isWin){
            winRound +=1;
        }else{
            loseRound +=1;
        }
        Debug.Log("Win round: " + winRound + " lose round: " + loseRound);
    }
}
