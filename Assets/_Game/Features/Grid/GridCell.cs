using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features {
    public class GridCell {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private Stack<CellLayer> layers;

        // Propriedades para acesso controlado externo

        // Construtores
        public GridCell(int x, int y) {
            layers = new Stack<CellLayer>();

            layers.Push(new CellLayer("Initial Layer"));
        }

        #region Métodos públicos e privados da lógica da classe
        public string GetConcatenatedLayerText() {
            string concatenatedText = "";
            foreach (var layer in layers) {
                concatenatedText += layer.BlockTextTest + " ";
            }
            return concatenatedText.Trim();
        }
        #endregion
    }
}
