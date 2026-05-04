using DG.Tweening;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public float colorChangeSpeed = 1f;

    void Start()
    {
        text.color = Color.red;
        Sequence rainbow = DOTween.Sequence()
            .Append(text.DOColor(Color.yellow, colorChangeSpeed))
            .Append(text.DOColor(Color.green, colorChangeSpeed))
            .Append(text.DOColor(Color.cyan, colorChangeSpeed))
            .Append(text.DOColor(Color.blue, colorChangeSpeed))
            .Append(text.DOColor(Color.magenta, colorChangeSpeed))
            .Append(text.DOColor(Color.red, colorChangeSpeed))
            .SetLoops(-1);
    }
}