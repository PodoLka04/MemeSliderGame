using UnityEngine;
using DG.Tweening;
using YG;

public class VideoScroller : MonoBehaviour
{
    public float speed = 10f;
    public float returnDuration = 0.5f;
    public Ease easeType = Ease.OutBack;

    private Vector3 originalPosition;
    private bool isDragging;
    private float lastYPosition; // Запоминаем позицию при начале drag
    private Tweener returnTween;

    void Start()
    {
        originalPosition = transform.position;
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
        Data.Instance.cordY = transform.position.y - lastYPosition;

        returnTween = transform.DOMove(originalPosition, returnDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                // Меняем счетчик только после возврата в исходную позицию
                if (Data.Instance.cordY > 1.7f)
                {
                    Data.Instance.VidOrder++; // Вверх -> следующее видео
                    Data.Instance.vidB = true;
                    YandexGame.FullscreenShow();
                }
                if (Data.Instance.cordY < -1.7f && Data.Instance.VidOrder > 1)
                {
                    Data.Instance.VidOrder--; // Вниз -> предыдущее видео
                    Data.Instance.vidB = true;
                    YandexGame.FullscreenShow();
                }
                // Debug.Log("Текущее видео: " + Data.Instance.VidOrder);

                
            });
    }
}
/*using UnityEngine;
using DG.Tweening;

public class moving : MonoBehaviour
{
    public float speed = 10f;
    public float returnDuration = 0.5f; // Длительность возврата
    public Ease easeType = Ease.OutBack; // Тип анимации возврата

    private Vector3 originalPosition; // Исходная позиция
    private bool isDragging = false;
    private Tweener returnTween;

    private void Start()
    {
        // Запоминаем исходную позицию при старте
        originalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Data.Instance.cordY = transform.position.y;
        if (Input.GetMouseButton(0))
        {
            // Если только начали перетаскивание - отменяем текущий твин
            if (!isDragging && returnTween != null)
            {
                returnTween.Kill();
            }

            isDragging = true;

            // Получаем движение мыши по Y
            float mouseY = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;
            Vector3 movement = new Vector3(0, mouseY, 0);

            // Перемещаем объект
            transform.Translate(movement);
        }
        else if (isDragging) // Если кнопку мыши отпустили после перетаскивания
        {
            isDragging = false;

            // Запускаем анимацию возврата
            returnTween = transform.DOMove(originalPosition, returnDuration)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    if (Data.Instance.cordY >= 3)
                    {
                        Data.Instance.VidOrder++; // Увеличиваем счетчик при cordY >= 3
                    }
                    if (Data.Instance.cordY <= -3)
                    {
                        Data.Instance.VidOrder--; // Увеличиваем счетчик при cordY <= 3
                    }
                });
        }
    }
}*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float speed = 10;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse = new Vector3(0, Input.GetAxis("Mouse Y") * speed * Time.deltaTime, 0);
            transform.Translate(mouse * speed);
        }
    }
}*/