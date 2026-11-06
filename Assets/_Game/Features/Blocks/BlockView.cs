using DG.Tweening;
using OneBlock.Core.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Blocks {
    public class BlockView : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Shake")]
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private Vector3 positionStrength = new Vector3(0.15f, 0.15f, 0f);
        [SerializeField] private float rotationStrength = 8f;
        [SerializeField] private int vibrato = 20;


        [Header("Impacto")]
        [SerializeField] private Vector3 strechAmount = new Vector3(-0.1f, -0.1f, 0f);
        [SerializeField] private Vector3 squashAmount = new Vector3(-0.1f, -0.1f, 0f);

        // Propriedades para acesso controlado externo
        public event Action<bool> OnHoverChanged;
        public event Action OnClick;

        // Campos privados para o estado interno da classe
        private Camera _mainCamera;
        private bool _isHovered;

        private Color _originalColor;
        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private Quaternion _originalRotation;

        private Sequence _hitSequence;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            _originalColor = spriteRenderer.color;
            _originalPosition = transform.position;
            _originalScale = transform.localScale;
            _originalRotation = transform.rotation;

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
            } else if (_isHovered != false) {
                _isHovered = false;
                OnHoverChanged?.Invoke(false);
            }
        }
        private void OnEnable() {
            InputManager.Instance.InputActions.Gameplay.Interact.performed += HandleOnGameplayInteractPerformed;

            OnHoverChanged += HandleOnHoverChanged;
            OnClick += HandleOnClick;
        }
        private void OnDisable() {
            InputManager.Instance.InputActions.Gameplay.Interact.performed -= HandleOnGameplayInteractPerformed;

            OnHoverChanged -= HandleOnHoverChanged;
            OnClick -= HandleOnClick;
        }
        private void OnDestroy() {
            _hitSequence?.Kill();
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        private void HandleOnHoverChanged(bool hover) {
        }
        private void HandleOnClick() {
            _hitSequence?.Kill();
            ResetTransform();

            _hitSequence = DOTween.Sequence().SetLink(gameObject);

            _hitSequence.Join(transform.DOShakePosition(
                duration, positionStrength, vibrato, 90f, false, true));

            _hitSequence.Join(transform.DOShakeRotation(
                duration, new Vector3(0f, 0f, rotationStrength), vibrato, 90f, true));

            float strechDuration, squashDuration;

            strechDuration = duration * 0.7f;
            squashDuration = duration * 0.3f;

            _hitSequence.Join(transform.DOPunchScale(
                strechAmount, strechDuration, 8, 0.5f));

            _hitSequence.Join(transform.DOPunchScale(
                squashAmount, squashDuration, 8, 0.5f));

            _hitSequence.OnComplete(ResetTransform);
        }
        private void ResetTransform() {
            transform.localPosition = _originalPosition;
            transform.localScale = _originalScale;
            transform.localRotation = _originalRotation;
        }
        private void HandleOnGameplayInteractPerformed(InputAction.CallbackContext context) {
            if (!_isHovered) { return; }

            OnClick?.Invoke();
        }
        #endregion
    }
}