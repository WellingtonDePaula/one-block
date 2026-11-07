using UnityEngine;

namespace OneBlock.Features.Resources {
    public class ResourcePickup : MonoBehaviour {
        private enum Phase { Waiting, Rising, Falling, Landing, Limbo }

        [Header("Aterrissagem")]
        [SerializeField] private float _landingLerpSpeed = 6f;
        [SerializeField] private float _stopThreshold = 0.05f;
        [SerializeField] private float _squashAmount = 0.15f;
        [SerializeField] private float _squashRecoverSpeed = 8f;

        [Header("Limbo")]
        [SerializeField] private float _limboShrinkSpeed = 2f;

        private Phase _phase = Phase.Waiting;
        private bool _active = false;
        private bool _isGround = false;
        private float _riseTime = 0f;
        private float _fallTimer = 0f;
        private Vector2 _originalDirection;
        private Vector3 _baseScale;
        private Rigidbody2D _body;

        private void Awake() {
            _body = GetComponent<Rigidbody2D>();
            _baseScale = transform.localScale;
        }

        private void FixedUpdate() {
            if (!_active) { return; }

            switch (_phase) {
                case Phase.Waiting:
                    if (_body.linearVelocity.y > 0f) {
                        _phase = Phase.Rising;
                        _riseTime = Time.fixedDeltaTime;
                    }
                    break;

                case Phase.Rising:
                    if (_body.linearVelocity.y > 0f) {
                        _riseTime += Time.fixedDeltaTime;
                    } else {
                        _fallTimer = _riseTime * Mathf.Abs(_originalDirection.x);
                        _phase = Phase.Falling;
                    }
                    break;

                case Phase.Falling:
                    _fallTimer -= Time.fixedDeltaTime;
                    if (_fallTimer <= 0f) { EndFlight(); }
                    break;

                case Phase.Landing:
                    _body.linearVelocity = Vector2.Lerp(
                        _body.linearVelocity, Vector2.zero,
                        _landingLerpSpeed * Time.fixedDeltaTime);

                    if (_body.linearVelocity.sqrMagnitude <= _stopThreshold * _stopThreshold) {
                        _active = false;
                    }
                    break;
            }
        }

        private void Update() {
            if (!_active) { return; }

            if (_phase == Phase.Landing) {
                transform.localScale = Vector3.Lerp(
                    transform.localScale, _baseScale,
                    _squashRecoverSpeed * Time.deltaTime);
            } else if (_phase == Phase.Limbo) {
                transform.localScale -= _baseScale * (_limboShrinkSpeed * Time.deltaTime);

                if (transform.localScale.x <= 0f) {
                    Destroy(gameObject);
                }
            }
        }

        public void Setup(Vector2 originalDirection) {
            _originalDirection = originalDirection;
            _riseTime = 0f;
            _fallTimer = 0f;
            _phase = Phase.Waiting;
            _active = true;
        }

        public void SetGround(bool isGround) {
            _isGround = isGround;
        }

        private void EndFlight() {
            _body.gravityScale = Mathf.Abs(_originalDirection.y);

            if (_isGround) {
                transform.localScale = new Vector3(
                    _baseScale.x * (1f + _squashAmount),
                    _baseScale.y * (1f - _squashAmount),
                    _baseScale.z);
                _phase = Phase.Landing;
            } else {
                _phase = Phase.Limbo;
            }
        }
    }
}