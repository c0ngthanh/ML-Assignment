using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Extensions.Match3;

public class Match3Agent_Done : Agent {

    private Match3MLAgentsBoard_Done match3Board;

    private void Awake() {
        match3Board = GetComponent<Match3MLAgentsBoard_Done>();
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        Debug.Log("######");
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        foreach (Move move in match3Board.ValidMoves()) {
            //Debug.Log(move.Column + ", " + move.Row + ": " + move.Direction);
            discreteActions[0] = move.MoveIndex;
            break;
        }
    }

}
