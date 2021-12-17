using System;
using UnityEngine;

namespace ParticleWar
{
    public sealed class GameEventSystem : MonoBehaviour
    {
        public static GameEventSystem current;

        void Awake()
        {
            current = this;
        }

        internal event Action<int> onBallCreate;
        internal void BallCreate(int index)
        {
            if (onBallCreate != null)
            {
                onBallCreate(index);
            }
        }

        internal event Action onGUIUpdate;
        internal void GUIUpdate()
        {
            if (onGUIUpdate != null)
            {
                onGUIUpdate();
            }
        }

        internal event Action<GameData, BallsData> onDataUpdate;
        internal void DataUpdate(GameData data, BallsData ballsData)
        {
            if (onDataUpdate != null)
            {
                onDataUpdate(data, ballsData);
            }
        }

        internal event Action onLoose;
        internal void Loose()
        {
            if (onLoose != null)
            {
                onLoose();
            }
        }
    }
}
