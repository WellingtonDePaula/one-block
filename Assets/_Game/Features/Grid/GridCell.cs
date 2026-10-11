using OneBlock.Features.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features.Grid {
    public class GridCell {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private Stack<CellLayer> _layers;
        private Vector2Int _gridPos;

        // Propriedades para acesso controlado externo
        public bool IsHovered { get; private set; } = false;
        public Vector2Int GridPos => _gridPos;
        public event Action OnHoverEnter;
        public event Action OnHoverExit;

        // Construtores
        public GridCell(Vector2Int gridPos) {
            _layers = new Stack<CellLayer>();
            _gridPos = gridPos;
        }

        #region Métodos públicos e privados da lógica da classe
        public void AddLayer(Transform parent, GameObject blockPrefab, float cellSize) {
            GameObject blockObject = GameObject.Instantiate(blockPrefab, parent);
            blockObject.transform.localPosition = (Vector2) _gridPos * cellSize;

            var block = blockObject.GetComponent<BlockController>();
            block.Setup(_gridPos.x, _gridPos.y);

            OnHoverEnter += block.HoverEnter;
            OnHoverExit += block.HoverExit;
        }
        public void SetHover(bool hover) {
            if (IsHovered == hover) { return; }
            IsHovered = hover;

            if (IsHovered) {
                OnHoverEnter?.Invoke();
            } else {
                OnHoverExit?.Invoke();
            }
        }
        #endregion
    }
}
