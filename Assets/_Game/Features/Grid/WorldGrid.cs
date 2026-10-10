using SoWell.Utils.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features {
    public class WorldGrid : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private int _width = 10;
        private int _height = 10;
        private float _cellSize = 1f;
        private GenericGrid<GridCell> _grid;
        private Camera _camera;


        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Update() {
            if (Mouse.current == null) { return; }

            Vector2 worldPos = UtilsClass.GetMouseWorldPosition(_camera);

        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void CreateGrid(GameObject blockPrefab) {
            _grid = new GenericGrid<GridCell>(_width, _height, _cellSize, Vector3.zero, (GenericGrid<GridCell> g, int x, int y) => {
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
