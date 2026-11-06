using OneBlock.Core.Input;
using UnityEngine;

namespace OneBlock.Core.Game {
    public class GameManager : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            InputManager.Instance.SwitchToGameplay();
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
