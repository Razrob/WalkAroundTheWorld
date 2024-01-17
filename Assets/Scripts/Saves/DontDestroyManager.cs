using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

[DefaultExecutionOrder(-1000000)]
public class DontDestroyManager : MonoBehaviour
{
    [SerializeField] private GameObject _loading;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        YandexGame.DoAfterInit(() =>
        {
            _loading.SetActive(false);
        });
    }
}
