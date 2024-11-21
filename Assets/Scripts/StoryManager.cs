using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] Stats stats;
    [SerializeField] GameObject StoryPrefab;
    [SerializeField] GameObject ParentObject;

    [SerializeField] ScrollRect scrollRect;

    public List<ActivityTime> activitiesTime = new();
    public float currentActivityTime = 0f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void ClearStories()
    {
        foreach (var story in ParentObject.GetComponentsInChildren<StoryPrefab>())
        {
            Destroy(story.gameObject);
        }
    }

    public void CreateStory(string name, string story)
    {
        AddActivity(name, gameManager.gameTime);
        var storyItem = Instantiate(StoryPrefab);
        storyItem.transform.SetParent(ParentObject.transform, false);
        storyItem.GetComponent<StoryPrefab>().InitializeStory(name, gameManager.gameTime, currentActivityTime, story);

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;  
    }

    public void AddActivity(string name, float time)
    {
        var existingActivity = activitiesTime.FirstOrDefault(n => n.Name == name);
        if (existingActivity == null)
        {
            activitiesTime.Add(new ActivityTime(name, time));
            currentActivityTime = 0f;
        }
        else
        {
            currentActivityTime = time - existingActivity.Time;
            existingActivity.Time = time;
        }
    }

    [Serializable]
    public class ActivityTime
    {
        public string Name;
        public float Time;

        public ActivityTime(string name, float time)
        {
            Name = name;
            Time = time;
        }
    }
}
