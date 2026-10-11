using UnityEngine;

namespace OneBlock.Core.Input {
    public class InputManager : MonoBehaviour {
        // Campos estáticos e constantes
        public static InputManager Instance;

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo
        public PlayerControls InputActions => inputActions;

        // Campos privados para o estado interno da classe
        private PlayerControls inputActions;

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
            inputActions = new PlayerControls();
        }
        private void OnDisable() {
            if (inputActions != null) {
                inputActions.Disable();
            }
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void SwitchToGameplay() {
            inputActions.Disable();
            inputActions.Gameplay.Enable();
        }
        #endregion
    }
}