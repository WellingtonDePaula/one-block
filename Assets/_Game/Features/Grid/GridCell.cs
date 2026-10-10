using OneBlock.Features.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features.Grid {
    public class GridCell {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private Stack<CellLayer> _layers;

        // Propriedades para acesso controlado externo
        public bool IsHovered { get; private set; } = false;
        public event Action OnHoverEnter;
        public event Action OnHoverExit;

        // Construtores
        public GridCell() {
            _layers = new Stack<CellLayer>();
        }

        #region Métodos públicos e privados da lógica da classe
        public void AddLayer(Transform parent, GameObject blockPrefab, int x, int y, float cellSize) {
            GameObject blockObject = GameObject.Instantiate(blockPrefab, parent);
            blockObject.transform.localPosition = new Vector3(x, y) * cellSize;

            var block = blockObject.GetComponent<BlockController>();
            block.Setup(x, y);

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
