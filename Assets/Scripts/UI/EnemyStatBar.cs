using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatBar : MonoBehaviour
{
    [SerializeField] private BaseEnemy me;


    [SerializeField] private Slider barSlider;
    [SerializeField] private Slider easeSlider;
    [SerializeField] private GameObject statBar;
    [SerializeField] private float lerpSpeed = 0.01f;
    [SerializeField] private float maxStat;
    [SerializeField] private float stat;

    // Start is called before the first frame update
    void Start()
    {
        SetUpEnemyHealth();
        barSlider.maxValue = maxStat;
        barSlider.value = stat;
    }

    // Update is called once per frame
    void Update()
    {
        if (stat == maxStat)
        {
            statBar.SetActive(false);
        }
        else
        {
            statBar.SetActive(true);
        }
        stat = me.GetHP();
        if (barSlider.value != stat)
        {
            barSlider.value = stat;
        }

        if (easeSlider.value != stat)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, stat, lerpSpeed);
        }
    }

    private void SetUpEnemyHealth()
    {
        maxStat = me.GetMaxHP();
        stat = me.GetHP();
        stat = maxStat;

    }
}
