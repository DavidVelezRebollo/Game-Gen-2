using ANT.Interfaces.Ant;
using ANT.Input;

using UnityEngine;
using System.Collections.Generic;

namespace ANT.Components.Ants
{
    public class AntsManager : MonoBehaviour {
        [SerializeField] private List<AntComponent> InitialAnts;

        private List<IAnt> _currentAnts = new List<IAnt>();
        private InputManager _input;

        private void Start() {
            _input = InputManager.Instance;

            InitialAnts.ForEach(ant => {
                _currentAnts.Add(ant.GetAnt());

                if (_currentAnts.Count > 1) {
                    ant.Attach();
                    ant.SetAttachedAnt(InitialAnts[_currentAnts.Count - 2]);
                }
            });
        }

        private void Update() {
            if (!_input.AntSelectFlag()) return;
        }
    }
}
