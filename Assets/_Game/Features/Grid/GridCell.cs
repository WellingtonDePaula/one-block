using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features {
    public class GridCell {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private Stack<CellLayer> _layers;

        // Propriedades para acesso controlado externo

        // Construtores
        public GridCell() {
            _layers = new Stack<CellLayer>();
        }
        public GridCell(GameObject block) {
            _layers = new Stack<CellLayer>();

            BlockController controller = block.GetComponent<BlockController>();
            _layers.Push(new CellLayer(controller));
        }

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
