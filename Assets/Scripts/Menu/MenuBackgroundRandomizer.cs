using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackgroundRandomizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetSpriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    private void Start()
    {
        _targetSpriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

}
