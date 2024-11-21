using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FinalInstincts : MonoBehaviour
{
    [SerializeField] GameObject[] InstinctsSkills;
    [SerializeField] Stats stats;

    public List<InitialInstincts> initialInstincts = new();

    [Serializable]
    public class InitialInstincts
    {
        public string statName;
        public float levelInstinct;
        public float currentInstinctXP;

        public InitialInstincts(string statName, float levelInstinct, float currentInstinctXP)
        {
            this.statName = statName;
            this.levelInstinct = levelInstinct;
            this.currentInstinctXP = currentInstinctXP;
        }
    }

    private void AddInstinct(string statName, float levelInstinct, float currentInstinctXP)
    {
        if (initialInstincts.FirstOrDefault(n => n.statName == statName) == null)
        {
            initialInstincts.Add(new InitialInstincts(statName, levelInstinct, currentInstinctXP));
        }
    }

    public void InitializeInstincts()
    {
        foreach (var item in stats.stats)
        {
            AddInstinct(item.statName, item.levelInstinct, item.currentInstinctXP);
        }
    }

    public void UpdateInstinct()
    {
        foreach (var item in initialInstincts)
        {
            item.levelInstinct = stats.stats.FirstOrDefault(stats => stats.statName == item.statName).levelInstinct;
            item.currentInstinctXP = stats.stats.FirstOrDefault(stats => stats.statName == item.statName).currentInstinctXP;
        }
    }

    public void GameOverTextInstinct()
    {
        foreach (var item in InstinctsSkills)
        {
            item.GetComponentsInChildren<TMP_Text>().FirstOrDefault(n => n.name == "Modifier1Text").text =
                $"x{initialInstincts.FirstOrDefault(stats => stats.statName == item.name).levelInstinct:F2}";

            item.GetComponentsInChildren<TMP_Text>().FirstOrDefault(n => n.name == "Modifier2Text").text =
                $"x{stats.stats.FirstOrDefault(stats => stats.statName == item.name).levelInstinct:F2}";
        }
    }
}
