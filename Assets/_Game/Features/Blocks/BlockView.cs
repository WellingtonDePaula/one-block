using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Blocks {
    public class BlockView : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer spriteRenderer;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private Camera mainCamera;
        private Color _originalColor;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            _originalColor = spriteRenderer.color;

            mainCamera = Camera.main;
        }
        private void Update() {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            // Bateu em algo
            if (hit.collider != null) {
                if (hit.collider.gameObject.Equals(this.gameObject)) {
                    Debug.Log("Bateu Aqui");
                }
            }
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void HoverEnter() {
            spriteRenderer.color = Color.yellow;
        }
        public void HoverExit() {
            spriteRenderer.color = _originalColor;
        }
        #endregion
    }
}