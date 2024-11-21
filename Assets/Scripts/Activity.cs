using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static Stats;
using System;

public class Activity : MonoBehaviour
{
    public bool isRunning = false;

    public string activityName;
    [SerializeField] Image activityProgressBar;
    
    [SerializeField] Image PlayImage;
    [SerializeField] Image PauseImage;

    [SerializeField] GameManager gameManager;
    [SerializeField] Stats activityBase;
    [SerializeField] StoryManager storyManager;
    [SerializeField] ActivitiesVision activitiesVision;
    [SerializeField] Queue queue;

    [SerializeField] OxygenController oxygen;

    [SerializeField] GameObject QueueList;

    public float activityProgressBarFillAmount = 0f;

    private bool showing;


    public void CleanActivityProgress()
    {
        activityProgressBarFillAmount = 0f;
        activityProgressBar.fillAmount = activityProgressBarFillAmount;
        UpdateButtonImage(false);
    }

    public void PauseStartActivity()
    {
        StopRunningActivities();
        isRunning = !isRunning;
        UpdateButtonImage(isRunning);

        var thisActivity = activityBase.activities.FirstOrDefault(a => a.name == activityName);
        var thisStat = activityBase.stats.FirstOrDefault(a => a.statName == thisActivity.type);


        if (isRunning)
        {
            StartCoroutine(RunActivity(thisActivity, thisStat));
            oxygen.isOxygenLost = true;
            //gameManager.GameRunning();
        }
        else
        {
            oxygen.isOxygenLost = false;
            gameManager.GamePause();
        }
    }

    private void UpdateButtonImage(bool isActive)
    {
        PlayImage.gameObject.SetActive(!isActive);
        PauseImage.gameObject.SetActive(isActive);
    }

    IEnumerator RunActivity(ActivityBase thisActivity, Stat thisStat)
    {
        while (isRunning && thisActivity.runningTime < thisActivity.initialTime)
        {
            thisActivity.runningTime += Time.deltaTime * thisStat.level * thisStat.levelInstinct;
            activityProgressBarFillAmount = thisActivity.runningTime / thisActivity.initialTime;
            activityProgressBar.fillAmount = activityProgressBarFillAmount;

            if (QueueList.GetComponentsInChildren<QueueItemPrefab>().FirstOrDefault(n => n.name == thisActivity.name && n.isExecutable))
            {
                QueueList.GetComponentsInChildren<QueueItemPrefab>().FirstOrDefault(n => n.name == thisActivity.name).Progress.fillAmount = activityProgressBarFillAmount;
            }

            thisStat.currentXP += 10 * Time.deltaTime * thisStat.levelInstinct * thisStat.level;
            thisStat.currentInstinctXP += 10 * Time.deltaTime * thisStat.levelInstinct * thisStat.level; ;

            LevelUp(thisStat);
            activityBase.UpdateAllModifiers();

            yield return null;
        }
        if (thisActivity.runningTime >= thisActivity.initialTime)
        {
            isRunning = false;
            thisActivity.completed++;
            gameManager.GamePause();
            if (thisActivity.completed == thisActivity.completedMax)
            {
                ActivityArgs(thisActivity);
                if (thisActivity.storing)
                {
                    storyManager.CreateStory(thisActivity.name, thisActivity.story);
                }
                QueueInExecutable(thisActivity);
                gameObject.SetActive(false);

            }
            else
            {
                thisActivity.runningTime = 0f;
                activityProgressBar.fillAmount = 0;
                ActivityArgs(thisActivity);
                UpdateButtonImage(isRunning);
            }
            activitiesVision.CheckActivityVision();
        }
    }

    private void LevelUp(Stat thisStat)
    {
        float levelXP = ((float)Math.Pow(1.25d, thisStat.level - 1)) * thisStat.level1XP;
        float levelInstinctXP = ((float)Math.Pow(1.25d, thisStat.levelInstinct - 1)) * thisStat.level1InstinctXP;
        if (thisStat.currentXP >= levelXP)
        {
            Debug.Log("LevelUp");
            thisStat.level *= 1.05f; ;
            thisStat.currentXP -= levelXP;
        }
        if (thisStat.currentInstinctXP >= levelInstinctXP)
        {
            thisStat.levelInstinct *= 1.01f;
            thisStat.currentInstinctXP -= levelInstinctXP;
        }
    }

    public void StopRunningActivities()
    {
        foreach (var activity in activitiesVision.Activities)
        {
            var activityComponent = activity.GetComponent<Activity>();
            if (activityComponent.isRunning && activity.name != activityName)
            {
                activityComponent.PauseStartActivity();
            }
        }
    }


    public void ActivityArgs(ActivityBase thisActivity)
    {
        switch (thisActivity.name)
        {
            case "O2Mask":
                gameManager.AddO2();
                break;
            case "ExitFromCapsule":
                gameManager.ChangePlace(GameManager.Level.Cabin);
                break;
        }
    }


    public void AddToQueue()
    {
        var thisActivity = activityBase.activities.FirstOrDefault(a => a.name == activityName);
        var thisStat = activityBase.stats.FirstOrDefault(a => a.statName == thisActivity.type);
        queue.AddToQueue(thisStat, thisActivity);
    }

    public void QueueInExecutable(ActivityBase thisActivity)
    {
        if (QueueList.GetComponentsInChildren<QueueItemPrefab>().FirstOrDefault(n => n.name == thisActivity.name && n.isExecutable) != null)
        {
            QueueList.GetComponentsInChildren<QueueItemPrefab>().FirstOrDefault(n => n.name == thisActivity.name && n.isExecutable).isExecutable = false;
        }
    }

    public void ShowTips()
    {
        showing = true;
        //StartCoroutine(ShowTipsRoutine(showing));
    }

    public void HideTips()
    {
        showing = false;
        //gameManager.Tip.SetActive(false);
    }


    IEnumerator ShowTipsRoutine(bool showing)
    {
        while (showing)
        {
            gameManager.Tip.SetActive(true);
            Debug.Log(Input.mousePosition);
            gameManager.Tip.transform.localPosition = new Vector2(Input.mousePosition.x - 1, Input.mousePosition.y - 1);
            gameManager.Tip.GetComponentInChildren<TMP_Text>().text = activityProgressBarFillAmount * 100 + "%";
            yield return null;
        }
        gameManager.Tip.SetActive(false);
    }
}
