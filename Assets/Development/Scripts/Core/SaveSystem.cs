using UnityEngine;

namespace UAFaar.Core
{
    public static class SaveSystem
    {
        private const string SaveKey = "UAFaar_#Save";

        public static void Save(SaveData data)
        {
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return new SaveData();

            var json = PlayerPrefs.GetString(SaveKey);

            if (string.IsNullOrEmpty(json))
                return new SaveData();

            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}
