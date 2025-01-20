using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    private PlayerStats playerStats;

    [SerializeField] private Slider barSlider;
    [SerializeField] private Slider easeSlider;
    [SerializeField] private float lerpSpeed = 0.01f;
    [SerializeField] private float maxStat;
    [SerializeField] private float stat;
    [SerializeField] private string resourceName;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        SetUpStat(resourceName);
        barSlider.maxValue = maxStat;
        easeSlider.maxValue = maxStat;
        barSlider.value = stat;
        easeSlider.value = stat;
        PlayerManager.Instance.FindStatBar(this);
    }

    void Update()
    {
        stat = playerStats.GetCurrentStat(resourceName);
        if (barSlider.value != stat)
        {
            barSlider.value = stat;
        }

        if (easeSlider.value != stat)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, stat, lerpSpeed);
        }
    }

    private void SetUpStat(string resource)
    {
        maxStat = playerStats.GetMaxStat(resource);
        stat = maxStat;
    }

    public void StatUpdate()
    {
        maxStat = playerStats.GetMaxStat(resourceName);
        barSlider.maxValue = maxStat;
        easeSlider.maxValue = maxStat;
    }
    public string GetResourceName()
    {
        return resourceName;
    }
}
