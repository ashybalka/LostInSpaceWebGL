using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class OxygenController : MonoBehaviour
{
    [SerializeField] TMP_Text OxygenText;
    [SerializeField] TMP_Text OxygenDecayText;
    [SerializeField] Image OxygenBarImage;

    private Coroutine oxygenCoroutine;

    private readonly float initialOxygen = 100f;
    private readonly float decayOxygen = 0.25f;

    private float currentOxygen;
    public bool isOxygenLost = false;

    void Awake()
    {
        InitializeoxygenPanel();
    }

    private void InitializeoxygenPanel()
    {
        currentOxygen = initialOxygen;
        oxygenCoroutine ??= StartCoroutine(LostOxygen());
        UpdateOxygenUI();

    }

    private void UpdateOxygenUI()
    {
        OxygenText.text = currentOxygen.ToString("F2");
        OxygenDecayText.text = decayOxygen.ToString("F2");
        OxygenBarImage.fillAmount = currentOxygen / initialOxygen;
    }

    IEnumerator LostOxygen()
    {
        while (true)
        {
            if (isOxygenLost)
            {
                if (currentOxygen > 0)
                {
                    Debug.Log("Lost Oxygen");
                    currentOxygen -= decayOxygen * Time.deltaTime;
                }
                else
                {
                    //GameOver
                    Debug.Log("Oxygen empty");
                    break;
                }
            }

            UpdateOxygenUI();
            yield return null;
        }
    }
}
