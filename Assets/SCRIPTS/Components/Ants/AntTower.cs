using ANT.Components.Audio;

using UnityEngine;

namespace ANT.Components.Ants {
    public class AntTower : AntBuilding {
        [SerializeField] private GameObject Leaf;

        protected override void Build() {
            Vector3 firstAntPosition = new Vector3(AntsManager.GetAnt(0).GetAntCurrentPosition().x,
                AntsManager.GetAnt(0).GetAntCurrentPosition().y);
            float offset = AntsManager.GetAnt(0).GetTowerOffset();

            for (int i = 1; i < AntsManager.CurrentAntsCount(); i++) {
                AntComponent ant = AntsManager.GetAnt(i);
                ant.BuildTower(firstAntPosition, offset);

                offset += ant.GetTowerOffset();
            }

            Vector3 lastAntPosition = new Vector3(AntsManager.GetAnt(AntsManager.CurrentAntsCount() - 1).GetAntCurrentPosition().x,
                AntsManager.GetAnt(AntsManager.CurrentAntsCount() - 1).GetAntCurrentPosition().y);

            Leaf.transform.position = lastAntPosition + new Vector3(0, 1f);
            SoundManager.Instance.Play("AntTower");
            AntsManager.ProtectAnts();
            Interactable = false;
            Hide();
        }
    }
}
