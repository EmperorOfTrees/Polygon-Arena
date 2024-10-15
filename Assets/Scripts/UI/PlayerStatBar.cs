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
        barSlider.value = stat;
    }

    // Update is called once per frame
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
}
