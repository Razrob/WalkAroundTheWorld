using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameSaver 
{

    private static string _statsDataPath = $"{Application.persistentDataPath}/StatsData.dat";
    private static string _skinsDataPath = $"{Application.persistentDataPath}/SkinsData.dat";
    private static string _mapPropertiesDataPath = $"{Application.persistentDataPath}/MapPropertiesData.dat";
    private static string _customMapPropertiesDataPath = $"{Application.persistentDataPath}/CustomMapPropertiesData.dat";
    private static string _achievementsDataPath = $"{Application.persistentDataPath}/AchievementsData.dat";

    private static string _skinsResourcesPath = "Skins/";
    private static string _mapPropertiesResourcesPath = "MapProperties/";

    private static string _customMapPropertiesTileResourcesPath = "Tiles/Prefabs/";
    private static string _customMapPropertiesSpriteResourcesPath = "Sprites/";

    private static string _achievementsResourcesPath = "Achievements/";


    private static BinaryFormatter _binaryFormatter = new BinaryFormatter();

    private static void LoadStats()
    {
        if (!File.Exists(_statsDataPath)) return;

        using (FileStream _fileStream = File.Open(_statsDataPath, FileMode.Open, FileAccess.Read))
        {
            SavableStatsData _statsData = (SavableStatsData)_binaryFormatter.Deserialize(_fileStream);
            CoinWallet.Balance = _statsData.Balance;
            PlayerStats.Steps = _statsData.Steps;

        }
    }

    private static void LoadSkins()
    {
        if (!File.Exists(_skinsDataPath)) return;

        using (FileStream _fileStream = File.Open(_skinsDataPath, FileMode.Open))
        {
            SavableSkinData _skinData = (SavableSkinData)_binaryFormatter.Deserialize(_fileStream);

            List<Skin> _skins = new List<Skin>();
            for (int i = 0; i < _skinData.SkinFileNames.Count; i++) _skins.Add(Resources.Load<Skin>($"{_skinsResourcesPath}{_skinData.SkinFileNames[i]}"));

            AvailabilitySkins.Skins = _skins;
            AvailabilitySkins.SelectedSkin = Resources.Load<Skin>($"{_skinsResourcesPath}{_skinData.SelectedSkinName}");
        }
    }

    private static void LoadMaps()
    {
        if (!File.Exists(_mapPropertiesDataPath)) return;

        using (FileStream _fileStream = File.Open(_mapPropertiesDataPath, FileMode.Open))
        {
            SavableMapPropertiesData _mapPropertiesData = (SavableMapPropertiesData)_binaryFormatter.Deserialize(_fileStream);

            List<MapProperties> _maps = new List<MapProperties>();
            for (int i = 0; i < _mapPropertiesData.MapPropertiesFileNames.Count; i++) _maps.Add(Resources.Load<MapProperties>($"{_mapPropertiesResourcesPath}{_mapPropertiesData.MapPropertiesFileNames[i]}"));
            AvailabilityMapProperties.MapProperties = _maps;

            if (_mapPropertiesData.SelectedMapPropertiesName != null)
            {
                MapProperties _mapProperties = Resources.Load<MapProperties>($"{_mapPropertiesResourcesPath}{_mapPropertiesData.SelectedMapPropertiesName}");
                if (_mapProperties != null) AvailabilityMapProperties.SelectedMap = _mapProperties;
            }
        }
    }

    private static void LoadCustomMaps()
    {

        if (!File.Exists(_customMapPropertiesDataPath)) return;

        using (FileStream _fileStream = File.Open(_customMapPropertiesDataPath, FileMode.Open))
        {
            SavableCustomMapPropertiesData _customMapPropertiesData = (SavableCustomMapPropertiesData)_binaryFormatter.Deserialize(_fileStream);

            List<CustomMapProperties> _customMaps = new List<CustomMapProperties>();

            for(int i = 0; i < _customMapPropertiesData.CustomMapProperetiesData.Length; i++)
            {
                CustomMapProperties _customMap = ScriptableObject.CreateInstance<CustomMapProperties>();
                _customMap.CustomNoiseProperties = _customMapPropertiesData.CustomMapProperetiesData[i].NoiseProperties;

                HeightsBlocks _heightsBlocks = new HeightsBlocks();
                _heightsBlocks.Blocks = new HeightsBlocks.HeightBlock[_customMapPropertiesData.CustomMapProperetiesData[i].HeightsBlocksData.Blocks.Length];
                for (int x = 0; x < _heightsBlocks.Blocks.Length; x++)
                {
                    _heightsBlocks.Blocks[x].Block = Resources.Load<GameObject>($"{_customMapPropertiesTileResourcesPath}{_customMapPropertiesData.CustomMapProperetiesData[i].HeightsBlocksData.Blocks[x].Item1}");
                    _heightsBlocks.Blocks[x].Height = _customMapPropertiesData.CustomMapProperetiesData[i].HeightsBlocksData.Blocks[x].Item2;
                }
                _customMap.CustomHeightsBlocks = _heightsBlocks;

                _customMap.CustomVerticalScale = _customMapPropertiesData.CustomMapProperetiesData[i].VerticalScale;
                _customMap.CustomWaterLevel = _customMapPropertiesData.CustomMapProperetiesData[i].WaterLevel;

                _customMap._mapName = _customMapPropertiesData.CustomMapProperetiesData[i].MapName;
                _customMap.CustomSprite = Resources.Load<Sprite>($"{_customMapPropertiesSpriteResourcesPath}{_customMapPropertiesData.CustomMapProperetiesData[i].SpriteName}");

                _customMaps.Add(_customMap);

            }

            if(_customMapPropertiesData.SelectedMapName != null)
            {
                foreach (CustomMapProperties _customMap in _customMaps)
                {
                    if (_customMap.ItemName == _customMapPropertiesData.SelectedMapName)
                    {
                        AvailabilityMapProperties.SelectedMap = _customMap;
                        break;
                    }
                }
            }

            AvailabilityMapProperties.CustomMapProperties = _customMaps;

        }
    }

    private static void LoadCompletedAchievements()
    {
        if (!File.Exists(_achievementsDataPath)) return;

        using (FileStream _fileStream = File.Open(_achievementsDataPath, FileMode.Open))
        {
            Achievement[] _achievements = Resources.FindObjectsOfTypeAll<Achievement>();
            SavableCompletedAchievements _savableCompletedAchievements = (SavableCompletedAchievements)_binaryFormatter.Deserialize(_fileStream);

            for(int i = 0; i < _achievements.Length; i++)
            {
                for (int x = 0; x < _savableCompletedAchievements.SavableAchievements.Length; x++)
                {
                    if (_savableCompletedAchievements.SavableAchievements[x].AchievementName == _achievements[i].name) _achievements[i].RewardIsReceived = _savableCompletedAchievements.SavableAchievements[i].IsComplete;
                }
            }

        }
    }






    private static void SaveStats()
    {
        using (FileStream _fileStream = File.Create(_statsDataPath))
        {
            SavableStatsData _statsData = new SavableStatsData
            {
                Balance = CoinWallet.Balance,
                Steps = PlayerStats.Steps
            };
            _binaryFormatter.Serialize(_fileStream, _statsData);
        }
    }

    private static void SaveSkins()
    {
        using (FileStream _fileStream = File.Create(_skinsDataPath))
        {
            SavableSkinData _skinData = new SavableSkinData();

            foreach (Skin _skin in AvailabilitySkins.Skins) _skinData.SkinFileNames.Add(_skin.name);
            _skinData.SelectedSkinName = AvailabilitySkins.SelectedSkin.name;
            _binaryFormatter.Serialize(_fileStream, _skinData);
        }
    }

    private static void SaveMaps()
    {
        using (FileStream _fileStream = File.Create(_mapPropertiesDataPath))
        {
            SavableMapPropertiesData _mapPropertiesData = new SavableMapPropertiesData();

            foreach (MapProperties _map in AvailabilityMapProperties.MapProperties) _mapPropertiesData.MapPropertiesFileNames.Add(_map.name);
            if(!AvailabilityMapProperties.SelectedMapIsCustom()) _mapPropertiesData.SelectedMapPropertiesName = AvailabilityMapProperties.SelectedMap.name;
            else _mapPropertiesData.SelectedMapPropertiesName = null;

            _binaryFormatter.Serialize(_fileStream, _mapPropertiesData);
        }

    }

    private static void SaveCustomMaps()
    {
        using (FileStream _fileStream = File.Create(_customMapPropertiesDataPath))
        {
            SavableCustomMapPropertiesData _customMapPropertiesData = new SavableCustomMapPropertiesData();
            _customMapPropertiesData.CustomMapProperetiesData = new SavableCustomMapPropertiesData.MapData[AvailabilityMapProperties.CustomMapCount];

            for(int i = 0; i < AvailabilityMapProperties.CustomMapCount; i++)
            {
                CustomMapProperties _customMap = AvailabilityMapProperties.GetCustomMapProperties(i);

                _customMapPropertiesData.CustomMapProperetiesData[i] = new SavableCustomMapPropertiesData.MapData();
                _customMapPropertiesData.CustomMapProperetiesData[i].NoiseProperties = _customMap.NoiseProperties;

                _customMapPropertiesData.CustomMapProperetiesData[i].HeightsBlocksData.Blocks = new (string, float)[_customMap.HeightsBlocks.Blocks.Length];
                for(int x = 0; x < _customMap.HeightsBlocks.Blocks.Length; x++)
                    _customMapPropertiesData.CustomMapProperetiesData[i].HeightsBlocksData.Blocks[x] = (_customMap.HeightsBlocks.Blocks[x].Block.name, _customMap.HeightsBlocks.Blocks[x].Height);

                _customMapPropertiesData.CustomMapProperetiesData[i].VerticalScale = _customMap.VerticalScale;
                _customMapPropertiesData.CustomMapProperetiesData[i].WaterLevel = _customMap.WaterLevel;

                _customMapPropertiesData.CustomMapProperetiesData[i].SpriteName = _customMap.ItemImage.name;
                _customMapPropertiesData.CustomMapProperetiesData[i].MapName = _customMap.ItemName;

                if (AvailabilityMapProperties.SelectedMapIsCustom()) _customMapPropertiesData.SelectedMapName = AvailabilityMapProperties.SelectedMap.ItemName;
                else _customMapPropertiesData.SelectedMapName = null;
            }

            _binaryFormatter.Serialize(_fileStream, _customMapPropertiesData);
        }
    }

    private static void SaveCompletedAchievements()
    {

        using (FileStream _fileStream = File.Create(_achievementsDataPath))
        {
            Achievement[] _achievements = Resources.FindObjectsOfTypeAll<Achievement>();

            SavableCompletedAchievements _savableCompletedAchievements = new SavableCompletedAchievements();
            _savableCompletedAchievements.SavableAchievements = new SavableCompletedAchievements.SavableAchievement[_achievements.Length];

            for(int i = 0; i < _achievements.Length; i++)
            {
                _savableCompletedAchievements.SavableAchievements[i] = new SavableCompletedAchievements.SavableAchievement();

                _savableCompletedAchievements.SavableAchievements[i].AchievementName = _achievements[i].name;
                _savableCompletedAchievements.SavableAchievements[i].IsComplete = _achievements[i].RewardIsReceived;
            }
            _binaryFormatter.Serialize(_fileStream, _savableCompletedAchievements);
        }
    }



    public static void Init()
    {
        CoinWallet.OnBalanceChanged += (_value) => SaveStats();
        PlayerStats.OnStepsChanged += (_value) => SaveStats();

        AvailabilitySkins.OnSkinsChanged += SaveSkins;
        AvailabilityMapProperties.OnMapPropertiesChanged += SaveMaps;
        AvailabilityMapProperties.OnCustomMapPropertiesChanged += SaveCustomMaps;
        AchievementsDisplay.OnRewardTaken += SaveCompletedAchievements;
    }

    public static void LoadGameSaves()
    {
        LoadStats();
        LoadSkins();
        LoadMaps();
        LoadCustomMaps();
        LoadCompletedAchievements();
    }




}
