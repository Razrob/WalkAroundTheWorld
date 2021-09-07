using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileChoiceWindow : MonoBehaviour
{
    [SerializeField] private MapCreator _mapCreator;

    [SerializeField] private RectTransform _tileButtonsParent;
    [SerializeField] private TileButtonUI _tileButtonPrefab;

    [SerializeField] private GameObject[] _tiles;

    private void Awake()
    {
        for(int i = 0; i < _tiles.Length; i++)
        {
            TileButtonUI _tileButtonUI = Instantiate(_tileButtonPrefab, _tileButtonsParent);
            Sprite _sprite = _tiles[i].GetComponent<SpriteRenderer>().sprite;
            _tileButtonUI.TileImage.sprite = _sprite;
            int _index = i;
            _tileButtonUI.TileButton.onClick.AddListener(() => 
            {
                _mapCreator.ChangeLayerTile(_tiles[_index], _sprite);
                gameObject.SetActive(false);
            });
        }
        gameObject.SetActive(false);
    }

}
