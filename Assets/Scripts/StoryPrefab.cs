using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryPrefab : MonoBehaviour
{
    public Image storyImage;
    public TMP_Text storyName;
    public TMP_Text gameTime;
    public TMP_Text gameTimeDifference;
    public TMP_Text storyText;
    private string sign;

    public void InitializeStory(string name, float time, float timeDifference, string activityText)
    {
        storyName.text = name;
        gameTime.text = FillTimeString(time);

        gameTimeDifference.color = ColorSet(timeDifference);
        sign = DifferenceTimeSign(timeDifference);
        gameTimeDifference.text = sign + FillTimeString(timeDifference);

        storyText.text = activityText;
    }

    private static Color ColorSet(float time)
    {
        return time switch
        {
            0 => (Color.black),
            > 0 => Color.red,
            < 0 => Color.green,
            _ => Color.black,
        };
    }
    private static string DifferenceTimeSign(float time)
    {
        return time switch
        {
            0 => "",
            > 0 => "+",
            < 0 => "-",
            _ => "",
        };
    }

    private string FillTimeString(float time)
    { 
        return TimeSpan.FromSeconds(time).ToString("mm':'ss");
    }
}


