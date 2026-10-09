using SoWell.Utils.Core;
using System;
using UnityEngine;

namespace OneBlock.Features {
    public class WorldGrid : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private GenericGrid<GridCell> _grid;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void CreateGrid(GameObject blockPrefab) {
            var width = 10;
            var height = 10;

            _grid = new GenericGrid<GridCell>(width, height, 1f, Vector3.zero, (GenericGrid<GridCell> g, int x, int y) => {
                GameObject blockObject = Instantiate(blockPrefab, this.transform);
                blockObject.transform.localPosition = new Vector3(x, y) * g.CellSize;

                var block = blockObject.GetComponent<BlockController>();
                block.Setup(x, y);
                return new GridCell(block);
            });
        }
        #endregion
    }
}
