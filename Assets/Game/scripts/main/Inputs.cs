using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.scripts
{
    public class Inputs : MonoBehaviour
    {
        public static Inputs InputReader;

        public event Action OnRightAttack;
        public event Action OnLeftAttack;
        public event Action<WeaponNumber> OnTakeWeponInHand;
        
        public Vector3 Direction => _direction;
        public Vector2 LookDelta => _lookDelta;

        [Header("Buffer settings")] [SerializeField]
        private float bufferTimeToLife = 0.5f;

        public List<BufferedInput> _inputBuffer = new();
        
        private Vector3 _direction;
        private Vector2 _lookDelta;
        private InputSystem_Actions _inputSystemActions;

        private void Awake()
        {
            if (InputReader != null)
                return;
            
            InputReader = this;
            DontDestroyOnLoad(this);
            _inputSystemActions = new InputSystem_Actions();
        }

        private void Start()
        {
            _inputSystemActions.Player.Enable();

            // MOVEMENT
            _inputSystemActions.Player.Move.performed += MoveUpdatePerformed;
            _inputSystemActions.Player.Move.canceled += MoveUpdateCanceled;

            // JUMP
            _inputSystemActions.Player.Jump.performed += _ => BufferAction(InputActionType.Jump);
            
            // ROTATION
            _inputSystemActions.Player.Look.performed += LookChangedPerformed;
            _inputSystemActions.Player.Look.canceled += LookChangedCanceled;

            // FIRST ATTACK
            _inputSystemActions.Player.RightAttack.performed += RightAttackPerformed;
            //_inputSystemActions.Player.AttackBase.performed += _ => BufferAction(InputActionType.Attack);
            
            // SECOND ATTACK
            _inputSystemActions.Player.LeftAttack.performed += LeftAttackPerformed;
            
            // SWITCH WEAPON 
            _inputSystemActions.Player.TakeFirstWeaponInHand.performed += TakeFirstWeponInHead;
            _inputSystemActions.Player.TakeFirstWeaponInHand.performed += _ => BufferAction(InputActionType.TakeWeapon);
            // -----
            _inputSystemActions.Player.TakeSecondWeaponInHand.performed += TakeSecondWeponInHead;
            _inputSystemActions.Player.TakeSecondWeaponInHand.performed += _ => BufferAction(InputActionType.TakeWeapon);
            // -----
            _inputSystemActions.Player.TakeThirdWeaponInHand.performed += TakeThirdWeponInHead;
            _inputSystemActions.Player.TakeThirdWeaponInHand.performed += _ => BufferAction(InputActionType.TakeWeapon);
        }

        #region Buffer

        public bool ConsumeAction(InputActionType inputActionType)
        {
            // Ищем первое совпадение по типу действия в буфере
            for (int i = 0; i < _inputBuffer.Count; i++)
            {
                if (_inputBuffer[i].Type == inputActionType)
                {
                    _inputBuffer.RemoveAt(i); // Удаляем именно его
                    return true; // Возвращаем true персонажу
                }
            }
            return false;
        }
        
        private void BufferAction(InputActionType type)
        {
            _inputBuffer.Add(new BufferedInput(type));
        }

        private void ClearExpiredInputs()
        {
            while (_inputBuffer.Count > 0 && Time.time - _inputBuffer[0].Timestamp > bufferTimeToLife)
            {
                _inputBuffer.RemoveAt(0);
            }
        }
        #endregion

        #region Move

        private void MoveUpdatePerformed(InputAction.CallbackContext ctx)
        {
            var newDir = ctx.ReadValue<Vector2>();
            _direction = new Vector3(newDir.x, 0f, newDir.y);
        }
        
        private void MoveUpdateCanceled(InputAction.CallbackContext ctx) => _direction = Vector3.zero;

        #endregion

        #region Look

        private void LookChangedPerformed(InputAction.CallbackContext ctx)
        {
            _lookDelta = ctx.ReadValue<Vector2>();
        }

        private void LookChangedCanceled(InputAction.CallbackContext ctx) => _lookDelta = Vector3.zero;
        
        #endregion

        #region Attack

        private void RightAttackPerformed(InputAction.CallbackContext ctx)
        {
            OnRightAttack?.Invoke();
        }
        
        private void LeftAttackPerformed(InputAction.CallbackContext ctx)
        {
            //Debug.Log("Attack base");
            OnLeftAttack?.Invoke();
        }
        
        #endregion

        #region WeaponSwitching

        private void TakeFirstWeponInHead(InputAction.CallbackContext ctx)
        {
            Debug.Log("Taked first weapon");
            OnTakeWeponInHand?.Invoke(WeaponNumber.First);
        }
        
        private void TakeSecondWeponInHead(InputAction.CallbackContext ctx)
        {
            Debug.Log("Taked second weapon");
            OnTakeWeponInHand?.Invoke(WeaponNumber.Second);
        }
        
        private void TakeThirdWeponInHead(InputAction.CallbackContext ctx)
        {
            Debug.Log("Taked third weapon");
            OnTakeWeponInHand?.Invoke(WeaponNumber.Third);
        }

        #endregion

        
        private void Update()
        {
            ClearExpiredInputs();
        }

        private void OnDestroy()
        {
            if (_inputSystemActions == null) return;
            
            _inputSystemActions.Player.Move.performed -= MoveUpdatePerformed;
            _inputSystemActions.Player.Move.canceled -= MoveUpdateCanceled;
            
            _inputSystemActions.Player.Look.performed -= LookChangedPerformed;
            _inputSystemActions.Player.Look.canceled -= LookChangedCanceled;

            _inputSystemActions.Player.RightAttack.performed -= RightAttackPerformed;
            _inputSystemActions.Player.LeftAttack.performed -= LeftAttackPerformed;
            
            _inputSystemActions.Player.Disable();
        }
    }
}