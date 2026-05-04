using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Phone_activity : MonoBehaviour
{
    public GameObject phone;
    public GameObject dark_bg_pan;
    public GameObject warning_pan;

    [SerializeField] private Image darkOverlay;   // Затемнение (Чёрный UI Image)
    [SerializeField] private GameObject loadingScreen; // Экран загрузки

    public RectTransform loading_circle;

    private void Start()
    {
        dark_bg_pan.SetActive(false);
        warning_pan.SetActive(false);
        loadingScreen.SetActive(false);



        // Появление с "пружинным" эффектом (OutBack)
        phone.transform.localScale = Vector3.zero;
        phone.transform.DOScale(1, 1.5f).SetEase(Ease.OutBack);
    }

    public void App_button()
    {
        dark_bg_pan.SetActive(true);
        warning_pan.SetActive(true);
    }

    public void Yes_button()
    {
        dark_bg_pan.SetActive(false);
        warning_pan.SetActive(false);
    }

    public void EnterApp()
    {

        // 2. Затемняем экран (0 → 0.8 прозрачности)
        darkOverlay.DOFade(1f, 1f).From(0);

        // 3. Увеличиваем телефон (эффект "погружения")
        phone.transform.DOScale(3f, 1.5f)  // Увеличиваем в 3 раза
            .SetEase(Ease.InQuad) // Ускорение к концу (как "падение")
            .OnComplete(() =>
            {
                // 4. Показываем экран загрузки
                loadingScreen.SetActive(true);
                loadingScreen.transform.localScale = Vector3.zero;
                loadingScreen.transform.DOScale(1, 0.5f)
                    .SetEase(Ease.OutBack);
                loading_circle.DORotate(new Vector3(0, 0, -360), 1.3f, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
            });
    }




    IEnumerator LoadSceneAfterDelay()
    {
        // Ждём указанное количество секунд
        yield return new WaitForSeconds(5f);

        // Загружаем новую сцену
        SceneManager.LoadScene("InAppScene");
    }

    public void Load_to_NewScene()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }
}


