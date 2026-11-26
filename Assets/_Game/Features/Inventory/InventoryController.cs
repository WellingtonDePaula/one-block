using OneBlock.Features.Resources;
using UnityEngine;

namespace OneBlock.Features.Inventory {
    public class InventoryController : MonoBehaviour {
        // Campos estáticos e constantes

        // Campos expostos no Inspector

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private InventoryModel _model;

        #region Métodos do ciclo de vida da Unity (Awake, OnEnable, Start, OnDisable)
        private void Awake() {
            _model = new InventoryModel();
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public void AddItem(ResourceData resourceData, int amount) {
            _model.AddItem(resourceData, amount);
        }
        #endregion
    }
}