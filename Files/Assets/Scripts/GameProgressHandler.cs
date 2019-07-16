using System;
using UnityEngine;

namespace EventsActionsBestPractices
{
    public class GameProgressHandler : MonoBehaviour
    {
        const string gameProgressString = "gameprogress";

        public static Data gameProgressData;

        private void Awake()
        {
            //PlayerPrefs.DeleteAll();
            ReadLocalData();
        }

        void ReadLocalData()
        {
            if (!PlayerPrefs.HasKey(gameProgressString))
            {
                SaveDefaultProgress();
            }
            else
            {
                LoadData();
            }
        }

        private void SaveDefaultProgress()
        {
            gameProgressData = new Data();
            SaveData();
        }

        public static void LoadData()
        {
            Debug.LogError(gameProgressData);
            //data decryption can be added 
            gameProgressData = JsonUtility.FromJson<Data>(PlayerPrefs.GetString(gameProgressString));
            //PlayerPrefs.GetString(gameProgressString);
        }

        public static void SaveData()
        {
            Debug.LogError(JsonUtility.ToJson(gameProgressData));
            //data encryption can be added
            PlayerPrefs.SetString(gameProgressString, JsonUtility.ToJson(gameProgressData));
            //PlayerPrefs.SetString(gameProgressString, "GameProgress");
        }
    }
}

