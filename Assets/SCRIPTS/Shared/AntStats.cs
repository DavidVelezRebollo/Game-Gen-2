using UnityEngine;

namespace ANT.Shared {
    [CreateAssetMenu(fileName = "Ant", menuName = "Game/Ant")]
    public class AntStats : ScriptableObject {
        public AntTypes Type;
        public float HealthPoints = 100f;
        public float Speed = 1f;
    }
}
