using System.Collections;
using UnityEngine;

namespace Game.scripts.enemes
{
    public class EnemyVisual : MonoBehaviour
    {
        private Material _baseMaterial;
        private Enemy _enemy;
        private Color _baseColor;
        [SerializeField] private Color takeDamageColor;
        [SerializeField] [Range(0, 2)] private float flashDuration;
        private Material _tempMaterial;
        private Material _originMaterial;
        private WaitForSeconds _waitForSeconds;
        private Coroutine _coroutine;
        
        private void Awake()
        {
            _enemy = gameObject.GetComponentInParent<Enemy>();
            _baseMaterial = GetComponent<Renderer>().material;
            _waitForSeconds = new WaitForSeconds(flashDuration);
        }

        private void Start()
        {
            _baseColor = _baseMaterial.color;
            _originMaterial = _baseMaterial;
            _tempMaterial = new Material(_baseMaterial)
            {
                color = takeDamageColor
            };
            _enemy.OnDamageTaken += OnTakeDamageVisual;
        }
        
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                _waitForSeconds = new WaitForSeconds(flashDuration);
            }
        }

        public void OnTakeDamageVisual()
        {
            if (_coroutine != null) return;

            _coroutine = StartCoroutine(TakeDamageVisualRoutine());
        }
        
        private IEnumerator TakeDamageVisualRoutine()
        {
            _baseMaterial = _tempMaterial;
            yield return _waitForSeconds;
            _baseMaterial = _originMaterial;

            _coroutine = null;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _enemy.OnDamageTaken -= OnTakeDamageVisual;
        }
    }
}