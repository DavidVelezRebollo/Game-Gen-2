using ANT.Components.HUD;

using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(BoxCollider2D), typeof(TowerBridgesUI))]
    public class AntBridge : AntBuilding {
        [SerializeField] private GameObject BrigeSpot;
        [SerializeField] private float Offset;

        protected override void Build() {
            Vector3 bridgeSpotPosition = BrigeSpot.transform.position;
            Vector3 bridgeSpotScale = BrigeSpot.transform.localScale;
            Vector3 firstAntPosition = new Vector3(bridgeSpotPosition.x - Offset, bridgeSpotPosition.y);
            float offset = 0f;

            for(int i = 1; i < AntsManager.CurrentAntsCount(); i++) {
                AntComponent ant = AntsManager.GetAnt(i);
                ant.BuildBridge(firstAntPosition, offset);
                AntsManager.RemoveAnt(ant);

                offset += 1.15f;
            }
        }

        private void OnDrawGizmosSelected() {
            if (!BrigeSpot) {
                Debug.LogWarning("Bridge Spot Missing");
                return;
            }

            Gizmos.DrawWireCube(BrigeSpot.transform.position, BrigeSpot.transform.localScale);
        }
    }
}
