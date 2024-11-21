using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] Stats stats;

    [Header("Oxygen")]
    [SerializeField] TMP_Text OxygenText;
    [SerializeField] TMP_Text OxygenDecayText;
    [SerializeField] Image OxygenBarImage;

    private readonly float initialOxygen = 100f;
    private float currentOxygen;
    private readonly float decayOxygen = 0.25f;

    [Header("Time")]
    [SerializeField] TMP_Text TimeText;

    public float gameTime = 0f;

    private Coroutine oxygenCoroutine;
    private Coroutine timeCoroutine;

    [Header("GameOver")]
    [SerializeField] FinalInstincts finalInstincts;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] ActivitiesVision activitiesVision;
    [SerializeField] Queue queue;


    public GameObject Tip;

    public enum Level
    {
        Capsule,
        Cabin,
        Hall
    }
    [Header("Level")]
    public Level currentLevel;

    void Start()
    {
        InitiAlizeNewGame();
    }

    private void InitiAlizeNewGame()
    {
        InitializeoxygenPanel();
        finalInstincts.InitializeInstincts();
        currentLevel = Level.Capsule;

        stats.ActivitiesDesriptionAdd();
        stats.ClearStats();
        stats.UpdateAllModifiers();

        activitiesVision.CleanProgressBar();
        activitiesVision.CheckActivityVision();

        queue.UpdateExecutable();

        GetComponent<StoryManager>().ClearStories();
    }

    private void InitializeoxygenPanel()
    {
        currentOxygen = initialOxygen;

        //OxygenText.text = currentOxygen.ToString();
        //OxygenDecayText.text = decayOxygen.ToString();
        //OxygenBarImage.fillAmount = 1f;

        gameTime = 0f;
        TimeText.text = TimeSpan.FromSeconds(gameTime).ToString("mm':'ss");
    }

    public void GameRunning()
    {
        //oxygenCoroutine ??= StartCoroutine(LostOxygen());
        timeCoroutine ??= StartCoroutine(AddTime());
    }

    public void GamePause()
    {
        if (oxygenCoroutine != null)
        {
            StopCoroutine(oxygenCoroutine);
            oxygenCoroutine = null;
        }

        if (timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
            timeCoroutine = null;
        }
    }

    IEnumerator LostOxygen()
    {
        while (currentOxygen > 0)
        {
            currentOxygen -= decayOxygen * Time.deltaTime;

            OxygenText.text = currentOxygen.ToString("F2");
            OxygenBarImage.fillAmount = currentOxygen / initialOxygen;
            yield return null;
        }
    }

    IEnumerator AddTime()
    {
        while (currentOxygen > 0)
        {
            gameTime += Time.deltaTime;

            TimeText.text = TimeSpan.FromSeconds(gameTime).ToString("mm':'ss");
            yield return null;
        }
        GameOver();
    }


    public void AddO2(float amount)
    {
        currentOxygen += amount;
        if (currentOxygen > initialOxygen)
        {
            currentOxygen = initialOxygen;
        }

        OxygenText.text = currentOxygen.ToString();
        OxygenBarImage.fillAmount = currentOxygen / initialOxygen;
    }

    public void AddO2()
    {
        currentOxygen = initialOxygen;
        OxygenText.text = currentOxygen.ToString();
        OxygenBarImage.fillAmount = currentOxygen / initialOxygen;
    }

    public void GameOver()
    {
        StopRunningActivities();
        GamePause();
        GameOverPanel.SetActive(true);
        finalInstincts.GameOverTextInstinct();  
        finalInstincts.UpdateInstinct();
    }

    public void NewGame()
    {
        InitiAlizeNewGame();

        GameOverPanel.SetActive(false);
    }

    public void StopRunningActivities()
    {
        foreach (var activity in activitiesVision.Activities)
        {
            var activityComponent = activity.GetComponent<Activity>();
            if (activityComponent.isRunning)
            {
                activityComponent.PauseStartActivity();
            }
        }
    }

    public void ChangePlace(Level level)
    {
        currentLevel = level;
        Debug.Log("Current Level: " + currentLevel);
    }
}
