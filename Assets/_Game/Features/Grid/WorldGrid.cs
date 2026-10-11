using SoWell.Utils.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Grid {
    public class WorldGrid : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private GridLinesController gridLinesController;
        [SerializeField] private SpriteRenderer hoverCellRenderer;

        // Propriedades para acesso controlado externo
        public event Action<GridCell> OnHoverCell;

        // Campos privados para o estado interno da classe
        private int _width = 10;
        private int _height = 10;
        private float _cellSize = 1f;
        private GenericGrid<GridCell> _grid;
        private GridCell _hoverCell;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
        }
        private void Update() {
            UpdateHoverCell();
        }
        private void OnEnable() {
            OnHoverCell += UpdateHoverRenderer;
        }
        private void OnDisable() {
            OnHoverCell -= UpdateHoverRenderer;
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void CreateGrid(GameObject blockPrefab) {
            Vector3 centeredOrigin = transform.position - new Vector3(_width, _height) * _cellSize * 0.5f;

            _grid = new GenericGrid<GridCell>(_width, _height, _cellSize, transform.position, (GenericGrid<GridCell> g, int x, int y) => {
                var cell = new GridCell(new Vector2Int(x, y));
                //cell.AddLayer(transform, blockPrefab, g.CellSize);
                return cell;
            });

            gridLinesController.Setup(_grid.OriginPosition, _grid.CellSize);
        }
        private void UpdateHoverCell() {
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
                OnHoverCell?.Invoke(_hoverCell);
            }
        }

        private void UpdateHoverRenderer(GridCell cell) {
            if (cell == null && hoverCellRenderer.enabled) {
                hoverCellRenderer.enabled = false;
                return;
            }
            hoverCellRenderer.enabled = true;
            // Atualiza a posição do hoverCellRenderer para coincidir com a célula atual
            hoverCellRenderer.transform.localPosition = new Vector2(cell.GridPos.x * _grid.CellSize + 0.5f, cell.GridPos.y * _grid.CellSize + 0.5f);
            hoverCellRenderer.sortingOrder = -cell.GridPos.y;
        }
        #endregion
    }
}
