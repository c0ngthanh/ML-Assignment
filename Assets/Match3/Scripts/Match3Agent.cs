using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Extensions.Match3;

public class Match3Agent : Agent {

    private Match3MLAgentsBoard match3Board;

    private void Awake() {
        match3Board = GetComponent<Match3MLAgentsBoard>();
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        foreach (Move move in match3Board.ValidMoves()) {
            discreteActions[0] = move.MoveIndex;
            break;
        }
    }

}
