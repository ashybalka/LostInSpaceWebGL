using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Queue : MonoBehaviour
{
    public List<Sprite> statsImages;
    [SerializeField] GameObject queuePrefab;
    [SerializeField] GameObject queueParent;
    [SerializeField] GameObject activityParent;

    [SerializeField] GameObject PlayImage;
    [SerializeField] GameObject PauseImage;

    public List<string> queueList;

    public bool isRunning = false;
    private Coroutine queueCoroutine;

    void Start()
    {
        
    }


    public void AddToQueue(Stats.Stat thisStat, Stats.ActivityBase thisActivity)
    {
        var queueItem = Instantiate(queuePrefab);
        queueItem.transform.SetParent(queueParent.transform, false);
        queueItem.name = thisActivity.name;

        Sprite spriteImage = statsImages.FirstOrDefault(x => x.name == thisStat.statName);
        
        int queueItemId = queueList.Count;
        //queueList.Add(activityName);

        queueItem.GetComponent<QueueItemPrefab>().Initialize(spriteImage, thisActivity.name, queueItemId, this);
    }

    public void DeleteListItem(int id)
    {
        queueList.RemoveAt(id);
    }

    public void QueueRunPause()
    {
        isRunning = !isRunning;
        UpdateButtonImage(isRunning);
        if (isRunning)
        {
            queueCoroutine ??= StartCoroutine(QueueRoutine());
        }
        else
        {
            if (queueCoroutine != null)
            {
                StopCoroutine(queueCoroutine);
                queueCoroutine = null;
            }
        }

    }

    private void UpdateButtonImage(bool isActive)
    {
        PlayImage.SetActive(!isActive);
        PauseImage.SetActive(isActive);
    }

    IEnumerator QueueRoutine()
    {
        while (isRunning)
        {
            var executableCount = queueParent.GetComponentsInChildren<QueueItemPrefab>().Where(n => n.isExecutable);
            Debug.Log(executableCount.Count());
            if (executableCount.Count() != 0)
            {
                var Q_Activity = activityParent.GetComponentsInChildren<Activity>().FirstOrDefault(n => n.name == executableCount.FirstOrDefault().name);
                if (Q_Activity == null)
                {
                    executableCount.FirstOrDefault().isExecutable = false;
                }
                if (Q_Activity != null && !Q_Activity.isRunning)
                {
                    Q_Activity.PauseStartActivity();
                }
            }
            else
            {
                break;
            }
            yield return null;
        }
        QueueRunPause();
    }

    public void UpdateExecutable()
    {
        foreach (var child in queueParent.GetComponentsInChildren<QueueItemPrefab>())
        {
            child.isExecutable = true;
            child.Progress.fillAmount = 0;
        }
    }

    public bool IsActivityAllowed(Activity activity)
    {
       return activity.gameObject.activeInHierarchy; 
    }
}
