using UnityEngine;

namespace OneBlock.Features.Resources {
    [CreateAssetMenu(fileName = "ResourceData", menuName = "OneBlock/ResourceData")]
    public class ResourceData : ScriptableObject {
        // Campos estáticos e constantes

        // Campos expostos no Inspector
        public string Name;
        public Sprite Icon;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe

        #region Métodos do ciclo de vida do ScriptableObject (OnEnable, OnDisable, OnDestroy)
        #endregion

        #region Métodos públicos e privados da lógica da classe
        #endregion
    }
}
