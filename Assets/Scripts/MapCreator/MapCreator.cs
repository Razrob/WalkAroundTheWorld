using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private NotificationDisplay _notificationDisplay;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _defaulTile;
    [SerializeField] private Sprite _defaultMapSprite;

    [SerializeField] private Slider _verticalScaleSlider;
    [SerializeField] private Slider _octaveSlider;
    [SerializeField] private Slider _frequencySlider;
    [SerializeField] private Slider _waterLevelSlider;
    [SerializeField] private Toggle _waterLevelToggle;

    [SerializeField] private GameObject _tileChoiceWindow;

    [SerializeField] private TextMeshProUGUI _layersCountText;

    [SerializeField] private int _maxLayersCount;
    [SerializeField] private RectTransform _mapLayersParent;
    [SerializeField] private MapLayerUI _mapLayerPrefab;

    private List<MapLayerUI> _mapLayers = new List<MapLayerUI>();

    private HeightsBlocks _heightsBlocks;
    private NoiseProperties _noiseProperties;
    private float _verticalScale = 10;
    private float _waterLevel = 0;

    private int _activeLayerIndex;

    private int _editableMapIndex = -1;

    private void OnEnable()
    {
        ClearAllProperties();
    }

    private void Start()
    {
        _verticalScaleSlider.onValueChanged.AddListener((_value) => _verticalScale = _value);
        _octaveSlider.onValueChanged.AddListener((_value) => _noiseProperties.OctaveNumber = (int)_value);
        _frequencySlider.onValueChanged.AddListener((_value) => _noiseProperties.Frequency = _value);
        _waterLevelSlider.onValueChanged.AddListener((_value) => _waterLevel = _value);
    }

    private void UpdateLayerDisplay(MapLayerUI _mapLayer)
    {

        _mapLayer.LayerName.text = $"Слой {_mapLayers.Count}";
        _mapLayer.LayerTile.sprite = null;

        int _layerIndex = _mapLayers.Count - 1;
        _mapLayer.LayerHeight.onValueChanged.AddListener((_value) => ChangeLayerHeight(_layerIndex, _value));
        _mapLayer.LayerSelectButton.onClick.AddListener(() => SelectLayerTile(_layerIndex));

        _mapLayer.LayerHeight.value = _heightsBlocks.Blocks[_mapLayers.Count - 1].Height;
        _mapLayer.LayerTile.sprite = _heightsBlocks.Blocks[_mapLayers.Count - 1].Block.GetComponent<SpriteRenderer>().sprite;
    }

    private void AddLayer()
    {
        if (_heightsBlocks.Blocks != null) Array.Resize(ref _heightsBlocks.Blocks, _heightsBlocks.Blocks.Length + 1);
        else _heightsBlocks.Blocks = new HeightsBlocks.HeightBlock[1];
        _heightsBlocks.Blocks[_mapLayers.Count].Block = _defaulTile;

        MapLayerUI _mapLayer = Instantiate(_mapLayerPrefab, _mapLayersParent);
        _mapLayers.Add(_mapLayer);

        UpdateLayerDisplay(_mapLayer);

        _layersCountText.text = _mapLayers.Count.ToString();
    }

    private void RemoveLayer()
    {
        Array.Resize(ref _heightsBlocks.Blocks, _heightsBlocks.Blocks.Length - 1);
        Destroy(_mapLayers[_mapLayers.Count - 1].gameObject);
        _mapLayers.RemoveAt(_mapLayers.Count - 1);
    }

    private bool CheckPropertiesCorrectness()
    {
        if (_heightsBlocks.Blocks == null || _heightsBlocks.Blocks.Length < 1) _notificationDisplay.DisplayNotification("Для сохранения вы должны добавить слои", 3);
        if (string.IsNullOrEmpty(_inputField.text)) _notificationDisplay.DisplayNotification("Для сохранения вы должны ввести название карты", 3);

        return _heightsBlocks.Blocks != null && _heightsBlocks.Blocks.Length > 0 && !string.IsNullOrEmpty(_inputField.text);
    }

    private void RefreshLayersDisplay()
    {
        foreach (MapLayerUI _mapLayer in _mapLayers) Destroy(_mapLayer.gameObject);
        _mapLayers.Clear();

        for(int i = 0; i < _heightsBlocks.Blocks.Length; i++)
        {
            MapLayerUI _mapLayer = Instantiate(_mapLayerPrefab, _mapLayersParent);
            _mapLayers.Add(_mapLayer);

            UpdateLayerDisplay(_mapLayer);
            _layersCountText.text = _mapLayers.Count.ToString();
        }
    }

    private void RefreshSlidersValues()
    {
        _verticalScaleSlider.value = _verticalScale;
        _octaveSlider.value = _noiseProperties.OctaveNumber;
        _frequencySlider.value = _noiseProperties.Frequency;
        _waterLevelSlider.value = _waterLevel;
    }
    
    public void SetEditableMapProperties(int _mapIndex)
    {
        _editableMapIndex = _mapIndex;

        CustomMapProperties _customMap = AvailabilityMapProperties.GetCustomMapProperties(_mapIndex);

        _heightsBlocks = _customMap._heightsBlocks;
        _noiseProperties = _customMap._noiseProperties;
        _verticalScale = _customMap._verticalScale;
        _waterLevel = _customMap._waterLevel;

        _inputField.text = _customMap._mapName;

        RefreshLayersDisplay();
        RefreshSlidersValues();
        if (_waterLevel == _heightsBlocks.Blocks[0].Height) _waterLevelToggle.isOn = true;
        else _waterLevelToggle.isOn = false;
    }

    public void ClearAllProperties()
    {
        _heightsBlocks = new HeightsBlocks();
        _noiseProperties = new NoiseProperties { Amplitude = 1, AmplitudeMultiplier = 0.5f, ClampingType = ClampingType.Interpolation, FrequencyMultiplier = 2, Frequency = 5, OctaveNumber = 3 };

        _editableMapIndex = -1;
        _verticalScale = 10;
        _waterLevel = 0;

        RefreshSlidersValues();
        _inputField.text = "";

        foreach (MapLayerUI _mapLayer in _mapLayers) Destroy(_mapLayer.gameObject);
        _mapLayers.Clear();

        _layersCountText.text = _mapLayers.Count.ToString();
    }

    public void ChangeLayerCount(int _countChange)
    {
        if (_mapLayers.Count + _countChange < 0 || _mapLayers.Count + _countChange > _maxLayersCount) return;
        if (_countChange == 1) AddLayer();
        else if (_countChange == -1) RemoveLayer();
    }


    public void SelectLayerTile(int _layerIndex)
    {
        _activeLayerIndex = _layerIndex;
        _tileChoiceWindow.SetActive(true);
    }

    public void ChangeLayerHeight(int _layerIndex, float _value)
    {
        _heightsBlocks.Blocks[_layerIndex].Height = _value;
    }

    public void ChangeLayerTile(GameObject _tile, Sprite _sprite)
    {
        _heightsBlocks.Blocks[_activeLayerIndex].Block = _tile;
        _mapLayers[_activeLayerIndex].LayerTile.sprite = _sprite;
    }

    public void SaveCurrentMapProperties()
    {
        if (!CheckPropertiesCorrectness()) return;

        CustomMapProperties _customMap;

        if (_editableMapIndex > -1) _customMap = AvailabilityMapProperties.GetCustomMapProperties(_editableMapIndex);
        else _customMap = ScriptableObject.CreateInstance<CustomMapProperties>();

        _customMap.SetHeightsBlocks = _heightsBlocks;
        _customMap.SetNoiseProperties = _noiseProperties;
        _customMap._verticalScale = _verticalScale;

        if (_waterLevelToggle.isOn) _waterLevel = _heightsBlocks.Blocks[0].Height;
        _customMap._waterLevel = _waterLevel;

        _customMap.SetName = _inputField.text;

        if (_editableMapIndex < 0)
        {
            _customMap.SetSprite = _defaultMapSprite;

            AvailabilityMapProperties.AddCustomMapProperties(_customMap);
            _editableMapIndex = AvailabilityMapProperties.CustomMapCount - 1;
        }

        _notificationDisplay.DisplayNotification("Карта сохранена", 1.5f);
    }
}
