using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [Header("�����")]
    public List<Stat> stats = new ();

    [Serializable]
    public class Stat
    {
        public string statName;

        public float level = 1;
        public float currentXP = 0f;
        public float level1XP = 100f;

        public float levelInstinct = 1;
        public float currentInstinctXP = 0f;
        public float level1InstinctXP = 500f;

        public TMP_Text statLevelText;
        public Image statLevelImage;

        public TMP_Text statInstinctLevelText;
        public Image statInstinctLevelImage;

        public void UpdateModifier()
        {
            statLevelText.text = $"x{level:F2}";
            statInstinctLevelText.text = $"x{levelInstinct:F2}";
        }

        public float ImageFillAmount()
        {
            float requiredXP = ((float)Math.Pow(1.25d, level - 1)) * level1XP;
            return currentXP / requiredXP;
        }

        public float ImageInstinctFillAmount()
        {
            float requiredInstinctXP = ((float)Math.Pow(1.25d, levelInstinct - 1)) * level1InstinctXP;
            return currentInstinctXP / requiredInstinctXP;
        }
    }

    public void ClearStats()
    {
        foreach (var stat in stats)
        {
            stat.level = 1f;
            stat.currentXP = 0f;
        }
    }

    [Header("����������")]
    public List<ActivityBase> activities;

    [Serializable]
    public class ActivityBase
    {
        public string name;
        public string type;
        public float initialTime;
        public float runningTime;
        public int completed;
        public int completedMax;
        public bool storing;
        public string story;

        public ActivityBase(string name, string type, float initialTime, float runningTime, int completed, int completedMax, bool storing, string story)
        {
            this.name = name;
            this.type = type;
            this.initialTime = initialTime;
            this.runningTime = runningTime;
            this.completed = completed;
            this.completedMax = completedMax;
            this.storing = storing;
            this.story = story;
        }
    }

    public void ActivitiesDesriptionAdd()
    {
        activities = new();
        //Ch0 Capsule
        //100
        AddActivity("�oncentrate", "Focus", 120f, 0f, 0, 1, true,
            "���������� ��������� ���������� ����, ���������� ��������, " +
            "�� ��������� ��� ������� �����������. �� ����, �� ���-�� ����� �� ���.");
        //50
        AddActivity("Recover", "Focus", 60f, 0f, 0, 1, true,
            "��������� ��������� ������ ��������� ������� ������. " +
            "������� ��������, ��� ������� ��������� � ��������� ���������� ����� � � ��� ����� ��������� �����, " +
            "������ ��� ��� ������ ������ ����. ������� �������, ���������� ��������, ����������� ������, " +
            "������� ������ �� ����� ������� � ������ ������ ������.");
        //150
        AddActivity("InvestigateCapsule", "Observation", 150f, 0f, 0, 1, true,
            "�� ��������������� ������������ � ���������, ��� ������ ������� ������� ����������� ������� ���������: " +
            "������ ���� �������� ������� ��������, � ��-��� ��� ������ ����������� �������. " +
            "������ �� ��� � ����� ��������� ������������ � �������, ��������, �� ����������. " +
            "�������� ������ �������� ���� ��� ���-�� �������� ��������, ����� ��������� ������.");
        //60
        AddActivity("BrokeCapsuleDoor", "Strength", 120f, 0f, 0, 1, true, 
            "� ������ �������� ����� ������� ���������, �������, ��� ����� ����������, " +
            "� ��� ���������� ��������� ���������� ���� ������. �������, � ������� ������� � ���������, " +
            "������� ����������� ����������, ����� ���������. �� ���������� �����, � ��� � ����� ��������� � �����.");
        //45
        AddActivity("RecoverCapsuleButton", "Intelligence", 90f, 0f, 0, 1, true, 
            "����������� � ��������� ������������ �����������, �� ���������, ��� ����� ������ ������, ����� ��������� ������, " +
            "� �� ���������, ��� ������ �� � ������� �������������." +
            "��������� ������������ � ������, �� ������������� ������ � ������� ��������. " +
            "��������� ����� �����������, � �� ������� ���������: ��������� �� ������ ��� ���. " +
            "�� ���� ��� �� ����������, ��� �������� �������� �������, � ������ ������� ������ ����� ������������ ����, " +
            "�������, ��� ������� ����� ��������.");
        //15
        AddActivity("RepeatInvestigateCapsule", "Observation", 20, 0f, 0, 5, true, 
            "�� �������������� ������ �������, � ������ ������ �� ��������� �����, " +
            "�������������� � ����� ����� � ����� ������. �� ��� ����� ������� �������: ���������� �����." +
            "�� ���������� ������ ������ � ������������� ����������� �����. ��� ������� ������ ����� ����, ��, �������, " +
            "��� �������� � �������������."); 

        AddActivity("O2Mask", "Intelligence", 60f, 0f, 0, 1, true, 
            "�� �������� ����� � ������ �� ���������� ������ � ��������." +
            "��� ��� �������� ���������� � ���� �� �� �����-�� ����� ������� ����� ����������.");

        AddActivity("TakeO2Mask", "Strength", 30f, 0f, 0, 1, true,
            "�������, ��� ������ ���� ���������� ���� � ��������� ������� �������, " +
            "�� ��������� �������� ����������� ����� � ��������� ������ �������. " +
            "����� �������� �������, �� ���� ����� �������� � ��������� ���������� " +
            "�� ������ � ���� ����������� �������.");

        AddActivity("ExitFromCapsule", "Agility", 120f, 0f, 0, 1000000000, true, 
            "���������� �� �������, �� �������� ������ ����� � �������, " +
            "��� ������������� ��� ��� ������ ���������� ������� �������. " +
            "�����������, ���������, ��� ���������� � ����� �����. " +
            "������� ���� ��������� ���� ����������� ����� ����� ����, ������������ ����� ���������, " +
            "� ����� ��������� ���� � ����� ����� ���� ���������� ������ ������, ��������� ��������.");

        //Ch1 Cabin
        AddActivity("InvestigateCabin", "Observation", 120, 0f, 0, 1, true,
            "����� �������� ���, ������ �� �������� ������ ������������. ��������� ����� ������� � ���������, " +
            "� ������ ���� ���������� �� ����, ����� ��� ����� ��������� �����������, " +
            "������� ������������ � ���-��� ������������� ���������.");

        AddActivity("RepeatInvestigateCabin", "Observation", 30, 0f, 0, 15, true,
            "");

        AddActivity("O2Ballon", "Intelligence", 100, 0f, 0, 15, true,
    "");
        AddActivity("TakeO2Ballon", "Strength", 30, 0f, 0, 15, true,
    "");
        AddActivity("DigUpCloset", "Strength", 200, 0f, 0, 15, true,
    "");
        AddActivity("BrokeDoor", "Strength", 60, 0f, 0, 15, true,
    "");
        AddActivity("CrackDoor", "Intelligence", 80, 0f, 0, 15, true,
    "");
        AddActivity("TakeTape", "Intelligence", 100, 0f, 0, 15, true,
    "");
        AddActivity("ScrapMetal", "Intelligence", 100, 0f, 0, 15, true,
    "");
        AddActivity("DigUpSpacesuit", "Observation", 300, 0f, 0, 15, true,
    "");
        AddActivity("TakeSpacesuit", "Strength", 30, 0f, 0, 15, true,
    "");
        AddActivity("ConcentrateCabin", "Focus", 200, 0f, 0, 15, true,
    "");
        AddActivity("ExitToHall", "Focus", 100, 0f, 0, 15, true,
    "");
        AddActivity("BackToCapsule", "Focus", 100, 0f, 0, 15, true,
    "");




    }



    private void Update()
    {
        foreach (var stat in stats)
        {
            stat.statLevelImage.fillAmount = stat.ImageFillAmount();
            stat.statInstinctLevelImage.fillAmount = stat.ImageInstinctFillAmount();
        }
    }

    private void AddActivity(string name, string type, float initialTime, float runningTime,
        int completed, int completedMax, bool storing, string story)
    {
        activities.Add(new ActivityBase(name, type, initialTime, runningTime, completed, completedMax, storing, story));
    }

    public void UpdateAllModifiers()
    {
        foreach (var stat in stats)
        {
            stat.UpdateModifier();
        }
    }
}
