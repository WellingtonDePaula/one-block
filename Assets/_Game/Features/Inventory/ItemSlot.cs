using OneBlock.Features.Resources;
using System;
using UnityEngine;

namespace OneBlock.Features.Inventory {
    public class ItemSlot {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private ResourceData _resourceData;
        private int _amount;

        // Propriedades para acesso controlado externo

        // Construtores
        public ItemSlot(ResourceData resourceData, int amount) {
            _resourceData = resourceData;
            _amount = amount;
        }

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
