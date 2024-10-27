using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        uiBar.fillAmount = GetPercentage(); // fillAmount 값에 새로운 값이 들어감 curValue로 인해
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue );
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
