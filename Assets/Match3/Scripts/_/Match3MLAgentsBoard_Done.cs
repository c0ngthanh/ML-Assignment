using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Extensions.Match3;

public class Match3MLAgentsBoard_Done : AbstractBoard {

    [SerializeField] private Match3 match3;
    [SerializeField] private Match3Visual match3Visual;
    private LevelSO levelSO;
    private Agent agent;

    private void Awake() {
        levelSO = match3.GetLevelSO();
        Columns = levelSO.width;
        Rows = levelSO.height;
        NumCellTypes = levelSO.gemList.Count;
        NumSpecialTypes = levelSO.goalType == LevelSO.GoalType.Glass ? 1 : 0;

        agent = GetComponent<Agent>();
    }

    private void Start() {
        match3.OnGemGridPositionDestroyed += Match3_OnGemGridPositionDestroyed;
        match3.OnGlassDestroyed += Match3_OnGlassDestroyed;
        match3Visual.OnStateWaitingForUser += Match3Visual_OnStateWaitingForUser;
        match3.OnOutOfMoves += Match3_OnOutOfMoves;
        match3.OnWin += Match3_OnWin;
        match3.OnMoveUsed += Match3_OnMoveUsed;
    }

    private void Match3_OnMoveUsed(object sender, System.EventArgs e) {
        if (levelSO.goalType == LevelSO.GoalType.Glass) {
            agent.AddReward(-.5f);
        }
    }

    private void Match3_OnWin(object sender, System.EventArgs e) {
        //Debug.Log(agent.GetCumulativeReward());
        agent.EndEpisode();
        //SceneManager.LoadScene(0);
    }

    private void Match3_OnOutOfMoves(object sender, System.EventArgs e) {
        agent.EndEpisode();
        //SceneManager.LoadScene(0);
    }

    private void Match3Visual_OnStateWaitingForUser(object sender, System.EventArgs e) {
        agent.RequestDecision();
    }

    private void Match3_OnGlassDestroyed(object sender, System.EventArgs e) {
        if (levelSO.goalType == LevelSO.GoalType.Glass) {
            agent.AddReward(2f);
        }
    }

    private void Match3_OnGemGridPositionDestroyed(object sender, System.EventArgs e) {
        if (levelSO.goalType == LevelSO.GoalType.Score) {
            agent.AddReward(1f);
        }
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
            match3Visual.SwapGridPositions(startX, startY, endX, endY);
            return true;
        } else {
            return false;
        }
    }

}
