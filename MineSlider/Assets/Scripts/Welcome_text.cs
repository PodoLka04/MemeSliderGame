using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Welcome_text : MonoBehaviour
{
    [Header("Animation Settings")]
    public float startScale = 0.1f;
    public float maxScale = 1.5f;
    public float finalScale = 1f;
    public float popDuration = 0.5f;
    public float riseDuration = 0.7f;
    public float riseHeight = 2f;
    public float delayBeforeRise = 0.2f;

    private Vector3 originalPosition;
    private Vector3 originalScale;

    public GameObject text1; // это для того чтобы отключать и включать его
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;

    public GameObject sun;

    public AudioSource Бдыщь;

    public GameObject arrdown;
    public GameObject arrup;

    public GameObject fingdown;
    public GameObject fingup;

    private Vector3 arrdownOriginalPos, arrupOriginalPos; //хз


    void Start()
    {
        // Сохраняем оригинальные параметры
        originalPosition = transform.position;
        originalScale = transform.localScale;

        arrdownOriginalPos = arrdown.transform.position; //хз
        arrupOriginalPos = arrup.transform.position; //хз

        text1.SetActive(false);
        text2.transform.localScale = Vector3.zero;
        text3.transform.localScale = Vector3.zero;
        text4.transform.localScale = Vector3.zero;

        arrdown.transform.localScale = Vector3.zero;
        arrup.transform.localScale = Vector3.zero;

        fingdown.transform.localScale = Vector3.zero;
        fingup.transform.localScale = Vector3.zero;


        sun.transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
    }

    public void PlayAnimation()
    {
        text1.SetActive(true);
        // Сбрасываем состояние
        transform.position = originalPosition;
        transform.localScale = originalScale * startScale;

        arrdown.transform.position = arrdownOriginalPos; //хз
        arrup.transform.position = arrupOriginalPos; //хз

        // Создаем последовательность анимаций
        Sequence sequence = DOTween.Sequence();

        // 1. Первая фаза - "выпрыгивание" из центра
        sequence.Append(transform.DOScale(originalScale * maxScale, popDuration).SetEase(Ease.OutBack));
        Бдыщь.Play();


        // 2. Небольшая пауза
        sequence.AppendInterval(delayBeforeRise);

        // 3. Вторая фаза - подъем вверх с уменьшением
        sequence.Append(transform.DOMoveY(originalPosition.y + riseHeight, riseDuration).SetEase(Ease.OutQuad));
        sequence.Join(transform.DOScale(originalScale * finalScale, riseDuration).SetEase(Ease.InOutSine));

        // 4. Легкое "подпрыгивание" в конце
        sequence.Append(transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 2, 0.5f))
            .AppendCallback(() => Бдыщь.Play()); //тут мудрил пытался попасть звуком вовремя

        sequence.Append(text2.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .AppendInterval(0.5f) // Пауза между анимациями
       .AppendCallback(() => Бдыщь.Play())

       .Append(text3.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .Append(arrdown.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .Append(fingdown.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .AppendInterval(0.2f)
       .AppendCallback(() => Бдыщь.Play())

       .Append(text4.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .Append(arrup.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .Append(fingup.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack))
       .AppendCallback(() =>
        Data.Instance.keyToVid = true);
    }

    // Для тестирования в редакторе
    [ContextMenu("Test Animation")]
    private void TestAnimation()
    {
        PlayAnimation();
    }
}
