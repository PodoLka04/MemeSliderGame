using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;
using YG;

public class video : MonoBehaviour
{
    // 1. Ссылки на компоненты (перетягиваем в инспекторе)
    public VideoPlayer vid; // Сам видеоплеер
    public GameObject vidGO; // Объект, который показывает видео (например, экран)
    //bool vidB = false; // Флажок "надо включить видео"
    public string nameVid; // Имя файла без .mp4 (например "реклама1")


    public void Start()
    {
        vidGO.SetActive(false);
    }

    // 2. Метод, который вызываем при нажатии кнопки "Смотреть"
    public void StartVid1()
    {
        Data.Instance.vidB = true; // Поднимаем флажок
    }

    // 3. FixedUpdate - работает каждый кадр (но для видео лучше Update)
    void Update()
    {

        // Если флажок поднят (кто-то нажал кнопку)
        if (Data.Instance.vidB && Data.Instance.VidOrder < 24) //чтобы видео не перскочило
        {
            // Включаем экран
            vidGO.SetActive(true);

            // Включаем видеоплеер
            vid.enabled = true;



            // Говорим откуда брать видео (StreamingAssets/реклама1.mp4)
            vid.url = System.IO.Path.Combine(Application.streamingAssetsPath, nameVid + Data.Instance.VidOrder + ".mp4");

            // Дважды запускаем (лишнее, хватит одного Play)
            //if (Data.Instance.GoVideo)
            //{
            vid.Play();
            //    Data.Instance.GoVideo = false;
            //}
            // Опускаем флажок
            Data.Instance.vidB = false;
        }



        // Если видео закончилось
        if (!vid.isPlaying)
        {
            // Выключаем всё
            vid.enabled = false;
            vidGO.SetActive(false);
        }

    }

    void OnEnable()
    {
        YandexGame.OpenFullAdEvent += OnAdOpened;
        YandexGame.CloseFullAdEvent += OnAdClosed;
    }

    void OnDisable()
    {
        YandexGame.OpenFullAdEvent -= OnAdOpened;
        YandexGame.CloseFullAdEvent -= OnAdClosed;
    }

    void OnAdClosed()
    {
        // Перезагружаем текущее видео или обновляем ленту  
        Data.Instance.vidB = true;
    }
    void OnAdOpened()
    {
        // Перезагружаем текущее видео или обновляем ленту  
        Data.Instance.vidB = false;
    }




}
