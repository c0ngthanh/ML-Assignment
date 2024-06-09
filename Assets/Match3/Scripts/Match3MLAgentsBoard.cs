using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Extensions.Match3;
using System;

public class Match3MLAgentsBoard : AbstractBoard {

    [SerializeField] private Match3 match3;
    [SerializeField] private Match3Visual match3Visual;

    private LevelSO levelSO;
    private Agent agent;

    private void Awake() {
        levelSO = match3.GetLevelSO();

        Columns = levelSO.width;
        Rows = levelSO.height;
        NumCellTypes = levelSO.gemList.Count;
        NumSpecialTypes = levelSO.goalType == LevelSO.GoalType.Score ? 0 : 1;

        agent = GetComponent<Agent>();

        match3Visual.OnStateWaitingForUser += Match3Visual_OnStateWaitingForUser;
        BattleManager.Instance.OnGameOver += BattleManager_OnGameOver;
        BattleManager.Instance.OnAttack += BattleManager_OnAttack;
    }

    private void BattleManager_OnAttack(object sender, EventArgs e)
    {
        agent.AddReward(BattleManager.Instance.GetMonsterHPDif());
    }

    private void BattleManager_OnGameOver(object sender, EventArgs e)
    {
        if(BattleManager.Instance.isWin){
            agent.AddReward(10);
        }else{
            agent.AddReward(-10);
        }
        agent.EndEpisode();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // private void Match3_OnWin(object sender, System.EventArgs e) {
    //     agent.EndEpisode();
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    // }

    // private void Match3_OnOutOfMoves(object sender, System.EventArgs e) {
    //     agent.EndEpisode();
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    // }

    // private void Match3_OnMoveUsed(object sender, System.EventArgs e) {
    //     if (levelSO.goalType == LevelSO.GoalType.Glass) {
    //         agent.AddReward(-.5f);
    //     }
    // }

    // private void Match3_OnGlassDestroyed(object sender, System.EventArgs e) {
    //     if (levelSO.goalType == LevelSO.GoalType.Glass) {
    //         agent.AddReward(1f);
    //     }
    // }

    // private void Match3_OnGemGridPositionDestroyed(object sender, System.EventArgs e) {
    //     if (levelSO.goalType == LevelSO.GoalType.Score) {
    //         agent.AddReward(1f);
    //     }
    // }

    private void Match3Visual_OnStateWaitingForUser(object sender, System.EventArgs e) {
        agent.RequestDecision();
    }

    public override int GetCellType(int row, int col) {
        GemSO gemSO = match3.GetGemSO(col, row);
        return levelSO.gemList.IndexOf(gemSO);
    }

    public override int GetSpecialType(int row, int col) {
        return match3.HasGlass(col, row) ? 1 : 0;
    }

    public override bool IsMoveValid(Move m) {
        int startX = m.Column;
        int startY = m.Row;
        var moveEnd = m.OtherCell();
        int endX = moveEnd.Column;
        int endY = moveEnd.Row;
        return match3.CanSwapGridPositions(startX, startY, endX, endY);
    }

    public override bool MakeMove(Move m) {
        int startX = m.Column;
        int startY = m.Row;
        var moveEnd = m.OtherCell();
        int endX = moveEnd.Column;
        int endY = moveEnd.Row;
        if (match3.CanSwapGridPositions(startX, startY, endX, endY)) {
            // Can do this move
            match3Visual.SwapGridPositions(startX, startY, endX, endY);
            return true;
        } else {
            // Cannot do this move
            return false;
        }
    }

}
