using UnityEngine;
using DG.Tweening;

public class DownArrowAnimation : MonoBehaviour
{
    public RectTransform arrow; // Перетащите сюда Image стрелки
    public float moveDistance = 30f; // Дистанция движения
    public float duration = 1f; // Длительность одного цикла

    void Start()
    {
        PlayBounceAnimation();
    }

    public void PlayBounceAnimation()
    {
        // Сбрасываем позицию
        arrow.anchoredPosition = Vector2.zero;

        // Создаем последовательность
        Sequence bounceSequence = DOTween.Sequence();

        // 1. Движение вниз
        bounceSequence.Append(arrow.DOAnchorPosY(-moveDistance, duration * 0.5f)
                     .SetEase(Ease.OutSine));

        // 2. Возврат в исходную позицию
        bounceSequence.Append(arrow.DOAnchorPosY(0, duration * 0.5f)
                     .SetEase(Ease.InSine));

        // 3. Зацикливание
        bounceSequence.SetLoops(-1);
    }
}