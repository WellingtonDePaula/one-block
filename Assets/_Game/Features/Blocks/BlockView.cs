using OneBlock.Core.Input;
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
                if (hit.collider.gameObject.Equals(this.gameObject) != _isHovered) {
                    _isHovered = true;
                    OnHoverChanged?.Invoke(true);
                }
            } else if(_isHovered != false) {
                _isHovered = false;
                OnHoverChanged?.Invoke(false);
            }
        }
        private void OnEnable() {
            InputManager.Instance.InputActions.Gameplay.Interact.performed += HandleOnGameplayInteractPerformed;

            OnHoverChanged += HandleOnHoverChanged;
        }
        private void OnDisable() {
            InputManager.Instance.InputActions.Gameplay.Interact.performed -= HandleOnGameplayInteractPerformed;

            OnHoverChanged -= HandleOnHoverChanged;
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        private void HandleOnHoverChanged(bool hover) {
            if(hover) {
                _spriteRenderer.color = Color.yellow;
            } else {
                _spriteRenderer.color = _originalColor;
            }
        }
        private void HandleOnGameplayInteractPerformed(InputAction.CallbackContext context) {
            if(!_isHovered) { return; }

            Clicked?.Invoke();
        }
        #endregion
    }
}