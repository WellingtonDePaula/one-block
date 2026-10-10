using UnityEngine;

namespace OneBlock.Features.Blocks {
    [RequireComponent(typeof(Collider2D))]
    public class BlockController : MonoBehaviour {
        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer spriteRenderer;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private Color _originalColor;
        private Vector2 _gridPos;

        #region Métodos do ciclo de vida da Unity
        private void Awake() {
            _originalColor = spriteRenderer.color;
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void Setup(int x, int y) {
            _gridPos = new Vector2(x, y);

            spriteRenderer.sortingOrder = -y;
        }
        public void HoverEnter() {
            spriteRenderer.color = Color.yellow;
        }
        public void HoverExit() {
            spriteRenderer.color = _originalColor;
        }
        #endregion
    }
}