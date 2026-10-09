using OneBlock.Features;
using UnityEngine;

namespace OneBlock.Core.Game {
    public class GameManager : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        [SerializeField] private WorldGrid worldGrid;
        [SerializeField] private GameObject blockPrefab;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void CreateGrid() {
            worldGrid.CreateGrid(blockPrefab);
        }
        #endregion
    }
}
