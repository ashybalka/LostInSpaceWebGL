using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [Header("Статы")]
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

    [Header("Активности")]
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
        AddActivity("Сoncentrate", "Focus", 120f, 0f, 0, 1, true,
            "Постепенно начинаете осознавать себя, чувствуете слабость, " +
            "но инстинкты уже требуют действовать. Вы живы, но что-то пошло не так.");
        //50
        AddActivity("Recover", "Focus", 60f, 0f, 0, 1, true,
            "Индикатор кислорода мигает тревожным красным светом. " +
            "Системы сообщают, что уровень кислорода в помещении критически низок — у вас всего несколько минут, " +
            "прежде чем его станет совсем мало. Глубоко вдохнув, чувствуете холодный, разреженный воздух, " +
            "который уходит из вашей капсулы с каждым ударом сердца.");
        //150
        AddActivity("InvestigateCapsule", "Observation", 150f, 0f, 0, 1, true,
            "Вы приглядываетесь внимательнее и замечаете, что кнопка отрытия капсулы практически разбита вдребезги: " +
            "острые края пластика неровно обломаны, а из-под них торчат разорванные провода. " +
            "Нажать на нее в таком состоянии бессмысленно – система, вероятно, не среагирует. " +
            "Придется искать обходные пути или как-то починить механизм, чтобы выбраться наружу.");
        //60
        AddActivity("BrokeCapsuleDoor", "Strength", 120f, 0f, 0, 1, true, 
            "С каждой секундой двери немного поддаются, скрипят, как будто протестуют, " +
            "а вам приходится буквально выламывать путь наружу. Наконец, с громким хрустом и скрежетом, " +
            "створка открывается достаточно, чтобы выбраться. Вы отпускаете дверь, и она с шумом ударяется о стену.");
        //45
        AddActivity("RecoverCapsuleButton", "Intelligence", 90f, 0f, 0, 1, true, 
            "Оглядевшись в крохотном пространстве криокапсулы, вы замечаете, что возле кнопки выхода, виден оголенный провод, " +
            "и вы понимаете, что именно он — причина неисправности." +
            "Осторожно наклонившись к панели, вы прикладываете провод к нужному контакту. " +
            "Несколько минут манипуляций, и вы решаете проверить: нажимаете на кнопку ещё раз. " +
            "На этот раз вы чувствуете, как механизм начинает оживать, и крышка капсулы издает тихий механический звук, " +
            "сообщая, что система снова работает.");
        //15
        AddActivity("RepeatInvestigateCapsule", "Observation", 20, 0f, 0, 5, true, 
            "Вы осматриваетесь внутри капсулы, и взгляд падает на маленький отсек, " +
            "вмонтированный в стену рядом с вашим плечом. На нем почти стертая надпись: «Аварийная маска»." +
            "Вы открываете крышку отсека и обнаруживаете кислородную маску. Она покрыта тонким слоем пыли, но, кажется, " +
            "ещё пригодна к использованию."); 

        AddActivity("O2Mask", "Intelligence", 60f, 0f, 0, 1, true, 
            "Вы цепляете маску к шлангу на внутренней панели и вдыхаете." +
            "Это даёт ощущение облегчения — хотя бы на какое-то время дыхание будет стабильным.");

        AddActivity("TakeO2Mask", "Strength", 30f, 0f, 0, 1, true,
            "Понимая, что каждый вдох приближает тебя к истощению запасов воздуха, " +
            "ты аккуратно снимаешь кислородную маску с креплений внутри капсулы. " +
            "Маска выглядит прочной, но есть следы старения — небольшие потертости " +
            "на ремнях и чуть пожелтевший пластик.");

        AddActivity("ExitFromCapsule", "Agility", 120f, 0f, 0, 1000000000, true, 
            "Выбравшись из капсулы, ты ощущаешь легкий холод и слышишь, " +
            "как металлический пол под ногами отзывается глухими звуками. " +
            "Оглянувшись, понимаешь, что находишься в своей каюте. " +
            "Тусклый свет аварийных ламп пробивается через клубы дыма, заполнившего часть помещения, " +
            "а через небольшое окно в стене видно лишь бескрайний черный космос, усыпанный звездами.");

        //Ch1 Cabin
        AddActivity("InvestigateCabin", "Observation", 120, 0f, 0, 1, true,
            "Каюта выглядит так, словно ее потрясло мощное столкновение. Некоторые шкафы сорваны с креплений, " +
            "а личные вещи разбросаны по полу, среди них можно различить инструменты, " +
            "остатки оборудования и кое-где разлетевшиеся документы.");

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
