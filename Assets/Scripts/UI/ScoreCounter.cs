using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI scoreUI;
    // Start is called before the first frame update
    private void Awake()
    {
        scoreUI = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = "score: " + GameManager.Instance.score;
    }
}
