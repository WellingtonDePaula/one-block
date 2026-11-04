using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Blocks {
    public class BlockView : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer _spriteRenderer;

        // Propriedades para acesso controlado externo
        public event Action<bool> OnHoverChanged;
        public event Action Clicked;

        // Campos privados para o estado interno da classe
        private Camera _mainCamera;
        private Color _originalColor;
        private bool _isHovered;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            _originalColor = _spriteRenderer.color;

            _mainCamera = Camera.main;
        }
        private void Update() {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = _mainCamera.ScreenPointToRay(mousePos);
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
            _spriteRenderer.color = Color.yellow;
        }
        public void HoverExit() {
            _spriteRenderer.color = _originalColor;
        }
        #endregion
    }
}