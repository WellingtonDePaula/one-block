namespace OneBlock.Features {
    public class CellLayer {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private BlockController _block;

        // Propriedades para acesso controlado externo
        public BlockController Block => _block;

        // Construtores
        public CellLayer(BlockController block) {
            _block = block;
        }

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
