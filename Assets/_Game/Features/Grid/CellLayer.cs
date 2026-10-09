using UnityEngine;

namespace OneBlock.Features {
    public class CellLayer {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private string blockTextTest;

        // Propriedades para acesso controlado externo
        public string BlockTextTest => blockTextTest;

        // Construtores
        public CellLayer(string text) {
            blockTextTest = text;
        }

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
