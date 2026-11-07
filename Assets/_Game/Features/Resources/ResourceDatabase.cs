using System.Collections.Generic;
using UnityEngine;

namespace OneBlock.Features.Resources {
    [CreateAssetMenu(fileName = "ResourceDatabase", menuName = "OneBlock/ResourceDatabase")]
    public class ResourceDatabase : ScriptableObject {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        public List<ResourceData> Resources;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida do ScriptableObject (OnEnable, OnDisable, OnDestroy)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        public ResourceData GetRandom() {
            if (Resources.Count == 0) {
                Debug.LogWarning("ResourceDatabase is empty.");
                return null;
            }
            int randomIndex = Random.Range(0, Resources.Count);
            return Resources[randomIndex];
        }
        #endregion
    }
}
