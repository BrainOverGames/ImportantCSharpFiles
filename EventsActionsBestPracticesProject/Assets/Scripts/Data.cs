using System;
using UnityEngine;

namespace EventsActionsBestPractices
{
    [Serializable]
    public class GameData
    {
        public int keys;

        public GameData()
        {
            keys = 1;
        }
    }

    [Serializable]
    public class Data
    {
        public GameData gameData;
        public Data()
        {
            gameData = new GameData();
        }
    }
}

