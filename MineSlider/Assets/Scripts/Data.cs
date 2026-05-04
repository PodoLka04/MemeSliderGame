using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Instance { get; private set; }

    [SerializeField] public float VidOrder = 1;
    [SerializeField] public float cordY = 0;
    [SerializeField] public bool vidB = false;
    [SerializeField] public float cordY2 = 0;
    [SerializeField] public bool keyToVid = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликаты
        }
    }

}
