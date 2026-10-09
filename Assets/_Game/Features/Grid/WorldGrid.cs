using SoWell.Utils.Core;
using UnityEngine;

namespace OneBlock.Features {
    public class WorldGrid : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private GenericGrid<GridCell> grid;

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            grid = new GenericGrid<GridCell>(10, 10, 1f, Vector3.zero, (GenericGrid<GridCell> g, int x, int y) => new GridCell(x, y));

            grid.ForEach((x, y, g) => {
                UtilsClass.CreateWorldText(g.GetConcatenatedLayerText(), localPosition: new Vector2(x * grid.CellSize, y * grid.CellSize), fontSize: 6);
            });
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
