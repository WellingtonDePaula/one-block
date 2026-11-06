using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OneBlock.Features.Blocks {
    [RequireComponent(typeof(Collider2D), typeof(BlockView))]
    public class BlockController : MonoBehaviour {
        // Campos expostos no Inspector
        [SerializeField] private Rigidbody2D resourcePrefab;
        [SerializeField] private float force;

        // Propriedades para acesso controlado externo

        // Campos privados para o estado interno da classe
        private BlockView _view;

        #region Métodos do ciclo de vida da Unity
        private void Awake() {
            _view = GetComponent<BlockView>();
        }
        private void OnEnable() {
            _view.OnClick += SpawnResource;
        }
        private void OnDisable() {
            _view.OnClick -= SpawnResource;
        }
        #endregion

        #region Métodos públicos e privados da lógica da classe
        private void SpawnResource() {
            Rigidbody2D resourceBody = Instantiate(resourcePrefab, transform.position, transform.rotation);

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.7f, 1f)).normalized;
            resourceBody.gravityScale = 0.7f/direction.y;

            resourceBody.AddForce(direction*force, ForceMode2D.Impulse);
        }
        #endregion
    }
}