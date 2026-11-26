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
        public bool CheckData(ResourceData resourceData) {
            return _resourceData == resourceData;
        }
        public void AddAmount(ResourceData data, int amount) {
            if (_resourceData != data) {
                throw new ArgumentException("Resource data does not match the slot's resource data.");
            }
            _amount += amount;
        }
        #endregion
    }
}
