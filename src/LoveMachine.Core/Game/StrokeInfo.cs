using UnityEngine;

namespace LoveMachine.Core.Game
{
    public struct StrokeInfo
    {
        public float Amplitude { get; set; }
        public float DurationSecs { get; set; }
        public float Completion { get; set; }
        public Vector3 Pos { get; set; }
        public Vector3 Rot { get; set; }
    }
}