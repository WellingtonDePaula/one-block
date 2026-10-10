using OneBlock.Features.Blocks;
using System;
using System.Collections.Generic;

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
        public GridCell(BlockController block) {
            _layers = new Stack<CellLayer>();


            _layers.Push(new CellLayer(block));

            OnHoverEnter += block.HoverEnter;
            OnHoverExit += block.HoverExit;
        }

        #region Métodos públicos e privados da lógica da classe
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
