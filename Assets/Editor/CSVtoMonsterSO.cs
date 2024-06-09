using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;

public class CSVtoMonsterSO
{
    private static int numberOfColumns = 15;
    private static string tempName = "";
    private static string path = "Assets/Resources/ScriptableObject/Monster";
    [MenuItem("Utilities/Generate Monster SO")]
    public static void GenerateMonsterSO()
    {
        string[] allLines = CSVRead.ReadCSV("monster");
        // File.Open(Application.dataPath + monsterCSVPath,)
        // string[] allLines = File.ReadAllLines(Application.dataPath + monsterCSVPath);
        int tableSize = allLines.Length / numberOfColumns - 1;
        for (int i = 0; i < tableSize; i++)
        {
            tempName = allLines[numberOfColumns * (i + 1)];
            MonsterSO monsterSO = ScriptableObject.CreateInstance<MonsterSO>();
            if (File.Exists($"{path}/{tempName}.asset"))
            {
                monsterSO = Resources.Load<MonsterSO>($"ScriptableObject/Monster/{tempName}");
                Debug.Log("Create " + monsterSO);
            }
            monsterSO.monsterName = allLines[numberOfColumns * (i + 1)];
            monsterSO.ID = allLines[numberOfColumns * (i + 1) + 1];
            monsterSO.level = int.Parse(allLines[numberOfColumns * (i + 1) + 2]);
            monsterSO.type = (Monster.MonsterType)int.Parse(allLines[numberOfColumns * (i + 1) + 3]);
            monsterSO.job = (MonsterJob) int.Parse(allLines[numberOfColumns * (i + 1) + 4]);
            monsterSO.star = int.Parse(allLines[numberOfColumns * (i + 1) + 5]);
            monsterSO.baseHP = int.Parse(allLines[numberOfColumns * (i + 1) + 6]);
            // switch (int.Parse(allLines[numberOfColumns * (i + 1) + 3]))
            // {
            //     case 1:
            //         monsterSO.type = Monster.MonsterType.Fire;
            //         break;
            //     case 2:
            //         monsterSO.type = Monster.MonsterType.Water;
            //         break;
            //     case 3:
            //         monsterSO.type = Monster.MonsterType.Grass;
            //         break;
            //     case 4:
            //         monsterSO.type = Monster.MonsterType.Electric;
            //         break;
            //     case 5:
            //         monsterSO.type = Monster.MonsterType.Dark;
            //         break;
            //     case 6:
            //         monsterSO.type = Monster.MonsterType.Light;
            //         break;
            // }
            monsterSO.baseATK = int.Parse(allLines[numberOfColumns * (i + 1) + 7]);
            monsterSO.baseDEF = int.Parse(allLines[numberOfColumns * (i + 1) + 8]);
            monsterSO.baseSpeed = int.Parse(allLines[numberOfColumns * (i + 1) + 9]);
            monsterSO.baseER = int.Parse(allLines[numberOfColumns * (i + 1) + 10]);
            monsterSO.baseEnergy = int.Parse(allLines[numberOfColumns * (i + 1) + 11]);
            monsterSO.baseHR = int.Parse(allLines[numberOfColumns * (i + 1) + 12]);
            monsterSO.baseCritRate = int.Parse(allLines[numberOfColumns * (i + 1) + 13]);
            monsterSO.baseCritDame = int.Parse(allLines[numberOfColumns * (i + 1) + 14]);
            if (!File.Exists($"{path}/{tempName}.asset"))
            {
                AssetDatabase.CreateAsset(monsterSO, $"{path}/{monsterSO.monsterName}.asset");
            }

            AssetDatabase.Refresh();
        }
        AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();
    }
}
