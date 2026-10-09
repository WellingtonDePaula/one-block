using SoWell.Utils.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features {
    [RequireComponent(typeof(Collider2D))]
    public class BlockController : MonoBehaviour {
        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color hoverColor = new Color(0.85f, 0.85f, 0.85f, 1f);

        // Propriedades para acesso controlado externo
        public bool IsHovered => _isHovered;

        // Campos privados para o estado interno da classe
        private Collider2D _collider;
        private Camera _camera;
        private Color _originalColor;
        private bool _isHovered;

        #region Métodos do ciclo de vida da Unity
        private void Awake() {
            _collider = GetComponent<Collider2D>();
            _camera = Camera.main;
            _originalColor = spriteRenderer.color;
        }

        private void Update() {
            if (Mouse.current == null) { return; }

            Vector2 worldPos = UtilsClass.GetMouseWorldPosition(_camera);

            bool isOver = _collider.OverlapPoint(worldPos);
            if (isOver == _isHovered) { return; }

            _isHovered = isOver;
            if (_isHovered) {
                OnHoverEnter();

            } else {
                OnHoverExit();
            }
        }

        private void OnDisable() {
            if (_isHovered) {
                _isHovered = false;
                OnHoverExit();
            }
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        private void OnHoverEnter() {
            spriteRenderer.color = hoverColor;
        }

        private void OnHoverExit() {
            spriteRenderer.color = _originalColor;
        }
        #endregion
    }
}