using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform _notification;
    [SerializeField] private TextMeshProUGUI _notificationText;

    private Queue<(string, float)> _notifications = new Queue<(string, float)>();
    private bool _notificationIsShown;
    
    private IEnumerator DisplayNotification((string, float) _notificationInfo)
    {
        _notificationIsShown = true;
        _notificationText.text = _notificationInfo.Item1;

        Vector2 _position = _notification.anchoredPosition;
        float _offcet = Mathf.Abs(_notification.anchoredPosition.y) * 2;

        for(int i = 0; i < 20; i++)
        {
            _position.y -= _offcet / 20f;
            _notification.anchoredPosition = _position;
            yield return new WaitForSeconds(0.015f);
        }

        yield return new WaitForSeconds(_notificationInfo.Item2);

        for (int i = 0; i < 20; i++)
        {
            _position.y += _offcet / 20f;
            _notification.anchoredPosition = _position;
            yield return new WaitForSeconds(0.015f);
        }

        _notificationIsShown = false;
        CheckExpectedNotifications();
    }

    private void CheckExpectedNotifications()
    {
        if (!_notificationIsShown && _notifications.Count > 0) StartCoroutine(DisplayNotification(_notifications.Dequeue()));
    }


    public void DisplayNotification(string _text, float _displayTime)
    {
        _notifications.Enqueue((_text, _displayTime));
        CheckExpectedNotifications();
    }
}
