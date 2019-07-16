using System;

namespace EventsActionsBestPractices
{
    public static class CurrencyHandler
    {
        public static Action<int> OnKeysChangeEvent;

        public static int GetKeys
        {
            get
            {
                return GameProgressHandler.gameProgressData.gameData.keys;
            }
        }

        public static bool DeductKeys(int amount)
        {
            if(GameProgressHandler.gameProgressData.gameData.keys >= amount)
            {
                GameProgressHandler.gameProgressData.gameData.keys -= amount;
                GameProgressHandler.SaveData();
                OnKeysChangeEvent?.Invoke(GetKeys);
                return true;
            }
            return false;
        }

        public static void CreditKeys(int amount)
        {
            GameProgressHandler.gameProgressData.gameData.keys += amount;
            GameProgressHandler.SaveData();
            OnKeysChangeEvent?.Invoke(GetKeys);
        }

        public static void RegisterOnKeysChangeEvent(Action<int>Callback, bool triggerCallback = false)
        {
            OnKeysChangeEvent += Callback;
            if(triggerCallback)
            {
                Callback(GetKeys);
            }
        }

        public static void UnRegisterOnKeysChangeEvent(Action<int>Callback)
        {
            if(Callback != null)
            {
                OnKeysChangeEvent -= Callback;
            }
        }
    }
}

