using OneBlock.Features.Blocks;
using SoWell.Utils.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Grid {
    public class WorldGrid : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private GridLinesController gridLinesController;


        // Campos privados para o estado interno da classe
        private int _width = 10;
        private int _height = 10;
        private float _cellSize = 1f;
        private GenericGrid<GridCell> _grid;
        private GridCell _hoverCell;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            transform.position = Vector3.zero;
        }
        private void Update() {
            HandleHoverCell();
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void CreateGrid(GameObject blockPrefab) {
            _grid = new GenericGrid<GridCell>(_width, _height, _cellSize, transform.position, (GenericGrid<GridCell> g, int x, int y) => {
                GameObject blockObject = Instantiate(blockPrefab, this.transform);
                blockObject.transform.localPosition = new Vector3(x, y) * g.CellSize;

                var block = blockObject.GetComponent<BlockController>();
                block.Setup(x, y);
                return new GridCell(block);
            });
            gridLinesController.Setup(_grid.OriginPosition, _grid.CellSize);
        }
        private void HandleHoverCell() {
            if (Mouse.current == null) { return; }
            if (_grid == null) { return; }

            Vector2 worldPos = UtilsClass.GetMouseWorldPosition();
            Vector2 inversedPos = transform.InverseTransformPoint(worldPos);

            Vector2Int gridPos = _grid.GetGridPosition(inversedPos);

            var newCell = _grid.GetValue(gridPos.x, gridPos.y);

            if (_hoverCell != newCell) {
                _hoverCell?.SetHover(false);
                _hoverCell = newCell;
                _hoverCell?.SetHover(true);
            }
        }
        #endregion
    }
}
