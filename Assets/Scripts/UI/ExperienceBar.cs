
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{

    [SerializeField] private Slider barSlider;

    [SerializeField] private float lerpSpeed = 0.01f;

    [SerializeField] private float maxEXP;
    [SerializeField] private float currentEXP;

    void Start()
    {
        Setup();
        PlayerManager.Instance.SetEXPBar(this);

    }

    void Update()
    {
        if (barSlider.value != currentEXP)
        {
            barSlider.value = currentEXP;
        }

    }

    private void Setup()
    {
        maxEXP = PlayerManager.Instance.LEVELTHRESHOLD;
        currentEXP = 0;
        barSlider.maxValue = maxEXP;
        barSlider.value = currentEXP;
    }

    public void SetCurrentEXP(int exp)
    {
        currentEXP = exp;
    }
    public void UpdateMaxEXP(int newTheshold)
    {
        barSlider.maxValue = newTheshold;

    }
}
