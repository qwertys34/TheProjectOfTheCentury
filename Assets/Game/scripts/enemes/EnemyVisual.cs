    using System.Collections;
    using UnityEngine;

    namespace Game.scripts.enemes
    {
        public class EnemyVisual : MonoBehaviour
        {
            [SerializeField] private Color takeDamageColor;
            [SerializeField] [Range(0, 2)] private float flashDuration;
            private Enemy _enemy;
            private Renderer _renderer;
            private Material _baseMaterial;
            private Material _tempMaterial;
            private WaitForSeconds _waitForSeconds;
            private Coroutine _coroutine;
            public Color color;
            private void Awake()
            {
                _enemy = gameObject.GetComponentInParent<Enemy>();
                _renderer = GetComponent<Renderer>();
                _baseMaterial = _renderer.material;
                _waitForSeconds = new WaitForSeconds(flashDuration);
                //_renderer.material.color = color;
            }

            private void Start()
            {
                /*_tempMaterial = new Material(_baseMaterial)
                {
                    color = takeDamageColor
                };*/
                _tempMaterial = new Material(_baseMaterial);
                _tempMaterial.SetColor("_BaseColor", takeDamageColor);
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
                _renderer.material = _tempMaterial;
                yield return _waitForSeconds;
                _renderer.material = _baseMaterial;

                _coroutine = null;
            }

            private void OnDestroy()
            {
                StopAllCoroutines();
                _enemy.OnDamageTaken -= OnTakeDamageVisual;
            }
        }
    }