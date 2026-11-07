using UnityEngine;

namespace OneBlock.Features.Resources {
    public class ResourcePickup : MonoBehaviour {
        private enum Phase { Waiting, Rising, Falling, Done }

        private Phase _phase = Phase.Waiting;
        private bool _active = false;
        private float _riseTime = 0f;   // quanto tempo ficou subindo
        private float _fallTimer = 0f;  // contagem regressiva depois do ápice
        private Vector2 _originalDirection;
        private Rigidbody2D _body;

        private void Awake() {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (!_active) { return; }

            float vy = _body.linearVelocity.y;

            switch (_phase) {
                case Phase.Waiting:
                    if (vy > 0f) {
                        _phase = Phase.Rising;
                        _riseTime = Time.fixedDeltaTime;
                    }
                    break;

                case Phase.Rising:
                    if (vy > 0f) {
                        _riseTime += Time.fixedDeltaTime;
                    } else {
                        _fallTimer = (_riseTime * Mathf.Abs(_originalDirection.x));
                        _phase = Phase.Falling;
                    }
                    break;

                case Phase.Falling:
                    _fallTimer -= Time.fixedDeltaTime;
                    if (_fallTimer <= 0f) {
                        _body.gravityScale = 0f;
                        _body.linearVelocity = new Vector2(_originalDirection.x, _originalDirection.y * 0.5f);
                        //_body.linearVelocity = _originalDirection;
                        _body.linearDamping = 6f;
                        _phase = Phase.Done;
                    }
                    break;
            }
        }

        public void Setup(Vector2 originalDirection) {
            _originalDirection = originalDirection;
            _riseTime = 0f;
            _fallTimer = 0f;
            _phase = Phase.Waiting;
            _active = true;
        }
    }
}