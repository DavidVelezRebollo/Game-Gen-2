using UnityEngine;

namespace ANT.Components.Ants {
    public class AntTower : AntBuilding {
        protected override void Build() {
            Vector3 firstAntPosition = new Vector3(AntsManager.GetAnt(0).GetAntCurrentPosition().x,
                AntsManager.GetAnt(0).GetAntCurrentPosition().y);
            float offset = 1.05f;

            for (int i = 1; i < AntsManager.CurrentAntsCount(); i++) {
                AntComponent ant = AntsManager.GetAnt(i);
                ant.BuildTower(firstAntPosition, offset);

                offset += 1.05f;
            }
        }
    }
}
