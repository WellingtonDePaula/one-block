using SoWell.Utils.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features {
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridLinesController : MonoBehaviour {
        // Campos estáticos e constantes
        private static readonly int GridOriginId = Shader.PropertyToID("_GridOrigin");
        private static readonly int CellSizeId = Shader.PropertyToID("_CellSize");
        private static readonly int MouseWorldId = Shader.PropertyToID("_MouseWorld");
        private static readonly int FadeRadiusId = Shader.PropertyToID("_FadeRadius");

        // Campos expostos no Inspector
        [Tooltip("Distância do fade, medida em células.")]
        [SerializeField] private float fadeCells = 2f;

        // Campos privados para o estado interno da classe
        private Material _material;
        private bool _isReady;

        #region Métodos do ciclo de vida da Unity
        private void Awake() {
            // .material cria uma instância, então não altera o asset original
            _material = GetComponent<SpriteRenderer>().material;
        }

        private void Update() {
            if (!_isReady || Mouse.current == null) { return; }

            Vector2 mouseWorld = UtilsClass.GetMouseWorldPosition();
            _material.SetVector(MouseWorldId, mouseWorld);
        }

        private void OnDestroy() {
            // Instâncias de material criadas via .material não são limpas sozinhas
            if (_material != null) { Destroy(_material); }
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        /// <summary>
        /// Envia os dados que não mudam por frame. Chame depois de criar o grid.
        /// </summary>
        public void Setup(Vector3 origin, float cellSize) {
            _material.SetVector(GridOriginId, (Vector2)origin);
            _material.SetFloat(CellSizeId, cellSize);
            _material.SetFloat(FadeRadiusId, fadeCells * cellSize);
            _isReady = true;
        }
        #endregion
    }
}