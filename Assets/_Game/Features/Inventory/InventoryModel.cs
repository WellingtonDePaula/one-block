using OneBlock.Features.Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features.Inventory {
    public class InventoryModel {
        // Campos estáticos e constantes

        // Campos privados para o estado interno da classe
        private List<ItemSlot> _slots;

        // Propriedades para acesso controlado externo

        // Construtores
        public InventoryModel() {
        }

        #region Métodos públicos e privados da lógica da classe
        public void AddItem(ResourceData resourceData, int amount = 1) {
            ItemSlot slot = null;
            for (int i = 0; i < _slots.Count; i++) {
                if (_slots[i].CheckData(resourceData)) {
                    slot = _slots[i];
                    break;
                }
            }
            if (slot == null) {
                slot = new ItemSlot(resourceData, amount);
                return;
            }
            slot.AddAmount(resourceData, amount);
        }
        #endregion
    }
}
