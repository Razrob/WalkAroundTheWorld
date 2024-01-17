using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Start()
    {
        YandexGame.DoAfterInit(() =>
        {
            SceneManager.LoadScene(_sceneName);
        });
    }
}
