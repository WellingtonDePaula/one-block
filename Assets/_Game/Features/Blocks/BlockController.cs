using UnityEngine;

namespace OneBlock.Features.Blocks {
    [RequireComponent(typeof(Collider2D))]
    public class BlockController : MonoBehaviour {
        // Campos expostos no Inspector
        [SerializeField] private SpriteRenderer spriteRenderer;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private Color _originalColor;

        #region Métodos do ciclo de vida da Unity
        private void Awake() {
            _originalColor = spriteRenderer.color;
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void HoverEnter() {
            spriteRenderer.color = Color.yellow;
        }
        public void HoverExit() {
            spriteRenderer.color = _originalColor;
        }
        public void Interact() {

        }
        #endregion
    }
}