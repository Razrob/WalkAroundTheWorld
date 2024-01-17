using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private GameObject _off;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            YandexGame.savesData.AudioEnable = !YandexGame.savesData.AudioEnable;
            _off.SetActive(!YandexGame.savesData.AudioEnable);
        });

        YandexGame.DoAfterInit(() =>
        {
            _off.SetActive(!YandexGame.savesData.AudioEnable);
        });
    }
}
