
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public SavableStatsData SavableStatsData;
        public SavableSkinData SavableSkinData;
        public SavableMapPropertiesData SavableMapPropertiesData;
        public SavableCustomMapPropertiesData SavableCustomMapPropertiesData;
        public SavableCompletedAchievements SavableCompletedAchievements;

        public bool AudioEnable = true;

        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
        }
    }
}
