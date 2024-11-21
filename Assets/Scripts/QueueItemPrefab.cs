using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueItemPrefab : MonoBehaviour
{
    public Image Q_Image;
    public Image Progress;
    [SerializeField] TMP_Text Q_Name;
    [SerializeField] TMP_Text Q_Count;

    public int count = 1;
    public int id;

    private Queue queueParent;
    public bool isExecutable = true;



    public void Initialize(Sprite image, string name, int queueItemId, Queue queue)
    {
        Q_Image.sprite = image;
        Q_Name.text = name;
        Q_Count.text = count.ToString();
        id = queueItemId;
        queueParent = queue;
    }

    public void AddItem()
    {
        count++;
        Q_Count.text = count.ToString();
    }
    public void RemoveItem()
    {
        queueParent.DeleteListItem(id);
        Destroy(gameObject);
    }
}
