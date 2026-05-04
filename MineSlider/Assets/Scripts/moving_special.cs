using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YG;

public class moving_special : MonoBehaviour
{
    public float speed = 10f;
    public float returnDuration = 0.5f;
    public Ease easeType = Ease.OutBack;

    private Vector3 originalPosition;
    private bool isDragging;
    private float lastYPosition; // Запоминаем позицию при начале drag
    private Tweener returnTween;



    public GameObject panel1;
    public GameObject panel2;


    public Welcome_text ekzemplar; // Перетащите объект в инспекторе - экземпляр  


    void Start()
    {
        originalPosition = transform.position;

        YandexGame.FullscreenShow();

        panel2.SetActive(false);
        panel1.transform.localScale = Vector3.zero;
        panel1.transform.DOScale(0.96f, 2f).SetEase(Ease.OutBack)
            .OnComplete(()=> ekzemplar.PlayAnimation()); //начало анимации  стекстом, где уже все последовательно (!!!welcome_text!!!)
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0))
        {
            Drag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    void StartDrag()
    {
        isDragging = true;
        lastYPosition = transform.position.y;
        if (returnTween != null) returnTween.Kill();
    }

    void Drag()
    {
        float deltaY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
        transform.Translate(0, deltaY, 0);
    }

    void EndDrag()
    {
        isDragging = false;

        // Определяем направление движения
        Data.Instance.cordY2 = transform.position.y - lastYPosition;

        returnTween = transform.DOMove(originalPosition, returnDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // Меняем счетчик только после возврата в исходную позицию
                if (Data.Instance.cordY2 > 1.7f && Data.Instance.keyToVid)
                {
                    Data.Instance.vidB = true; // Поднимаем флажок
                    panel1.SetActive(false);
                }
            });
    }
}

