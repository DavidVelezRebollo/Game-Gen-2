using ANT.Input;
using ANT.Interfaces.Ant;
using ANT.Components.Core;

using UnityEngine;
using System.Collections.Generic;

namespace ANT.Components.Ants
{
    public class AntsManager : MonoBehaviour {
        [SerializeField] private List<AntComponent> InitialAnts;

        private readonly List<AntComponent> _currentAnts = new();
        private InputManager _input;
        private GameManager _gameManager;

        private int _auxIndex;

        private bool _onSelectMode;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            _gameManager = GameManager.Instance;

            InitialAnts.ForEach(ant => {
                _currentAnts.Add(ant);

                if (_currentAnts.Count > 1) {
                    ant.SetAttachedAnt(InitialAnts[_currentAnts.Count - 2]);
                    ant.SetAttachedSpeed(_currentAnts[0].GetSpeed());
                }
            });
        }

        private void Update() {
            if (_onSelectMode) {
                SelectAnt();
                return;
            }
            
            _onSelectMode = _input.AntSelectFlag;

            if (!_onSelectMode) return;
            
            _gameManager.SetState(GameStates.Paused);
            _currentAnts[0].Highlight(Color.white);
            _auxIndex = 0;
        }

        #endregion

        #region Auxiliar Methods

        private void SelectAnt() {
            if (_input.RightFlag) {
                _currentAnts[_auxIndex].Dehighlight();
                _auxIndex = _auxIndex == 0 ? _currentAnts.Count - 1 : (_auxIndex -  1) % _currentAnts.Count;
                _currentAnts[_auxIndex].Highlight(Color.white);
            }
                
            if (_input.LeftFlag) {
                _currentAnts[_auxIndex].Dehighlight();
                _auxIndex = _auxIndex == _currentAnts.Count - 1 ? 0 : (_auxIndex +  1) % _currentAnts.Count;
                _currentAnts[_auxIndex].Highlight(Color.white);
            }

            if (_input.AntSelectFlag) {
                // Ant position change in the list
                (_currentAnts[_auxIndex].transform.position, _currentAnts[0].transform.position)
                    = (_currentAnts[0].transform.position, _currentAnts[_auxIndex].transform.position);
                (_currentAnts[_auxIndex], _currentAnts[0]) = (_currentAnts[0], _currentAnts[_auxIndex]);

                // Attachs the ants
                _currentAnts[0].Dehighlight();
                _currentAnts[0].SetAttachedAnt(null);

                for (int i = 1; i < _currentAnts.Count; i++) {
                    _currentAnts[i].SetAttachedAnt(_currentAnts[i - 1]);
                    _currentAnts[i].SetAttachedSpeed(_currentAnts[0].GetSpeed());
                }

                // Resume the game
                _gameManager.SetState(GameStates.Playing);
                _onSelectMode = false;
            }
        }

        #endregion
    }
}
