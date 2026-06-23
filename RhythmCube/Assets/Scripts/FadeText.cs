using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public Text Text;
    public CanvasGroup CanvasGroup;

    private float _setTime;

    public void SetText(string text)
    {
        Text.text = text;
        _setTime = Time.time;
    }

    void Start()
    {
        _setTime = Time.time;
    }

    void Update()
    {
        float dt = Time.time - _setTime;

        CanvasGroup.alpha = dt < 3 ? 1 : dt > 5 ? 0 : 1 - (dt - 3) / 2;
    }
}
