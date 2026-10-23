using System.Collections;
using Game.scripts.character;
using UnityEngine;
using UnityEngine.AI;

namespace Game.scripts.enemes
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        UseSkill
    }
    
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private float chaseDistance;
        [SerializeField] private float timeForIdle;
        [SerializeField] private float patrolRadius;
        [SerializeField] private float attackDistance;
        [SerializeField] private float maxTimePatrol;
        
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private bool enemyActive = true; 
        public EnemyState CurState { get; private set; }

        private float _patrolTimer;
        private WaitForSeconds _idleWait;

        private void Awake()
        {
            _idleWait = new WaitForSeconds(timeForIdle);
        }

        private void Start()
        {
            CurState = EnemyState.Idle;
            _patrolTimer = maxTimePatrol;
            StartCoroutine(StateLogic());
        }

        private void ChangeState(EnemyState enemyState)
        {
            if (CurState == enemyState)
                return;
            
            CurState = enemyState;
        }

        private IEnumerator StateLogic()
        {
            while (enemyActive)
            {
                switch (CurState)
                {
                    case EnemyState.Idle:
                        yield return Idle();
                        break;
                    case EnemyState.Patrol:
                        yield return Patrol();
                        break;
                    case EnemyState.Chase:
                        yield return Chase();
                        break;
                    case EnemyState.Attack:
                        yield return Attack();
                        break;
                    case EnemyState.UseSkill:
                        //yield return UseSkill();
                        break;
                }
            }
        }

        private IEnumerator Idle()
        {
            navMeshAgent.ResetPath();
            yield return _idleWait;
            
            if (IsPlayerNear(chaseDistance))
                ChangeState(EnemyState.Chase);
            else 
                ChangeState(EnemyState.Patrol);
        }

        private IEnumerator Patrol()
        {
            Vector3 randomPoint = transform.position + Utils.GetRandomDir() * patrolRadius;
            if (NavMesh.SamplePosition(randomPoint, out var hit, patrolRadius, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position);
                _patrolTimer = maxTimePatrol;
                
                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
                {
                    _patrolTimer -= Time.deltaTime;
                    if (_patrolTimer <= 0)
                    {
                        var state = Utils.ToFlipACoin() ? EnemyState.Patrol : EnemyState.Idle;
                        ChangeState(state);
                        yield break;
                    }
                    
                    if (IsPlayerNear(chaseDistance))
                    {
                        ChangeState(EnemyState.Chase);
                        yield break;
                    }
                    yield return null;
                }
            }

            var nextState = Utils.ToFlipACoin() ? EnemyState.Patrol : EnemyState.Idle;
            ChangeState(nextState);
        }
        
        private IEnumerator Chase()
        {
            while (enemyActive)
            {
                if (!IsPlayerNear(chaseDistance * 1.5f)) 
                {
                    ChangeState(EnemyState.Idle);
                    yield break;
                }

                navMeshAgent.SetDestination(Character.C.transform.position);

                if (IsPlayerNear(attackDistance))
                {
                    ChangeState(EnemyState.Attack);
                    yield break;
                }

                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            navMeshAgent.ResetPath();
            yield return new WaitForSeconds(1f);
            ChangeState(EnemyState.Chase);
        }

        private bool IsPlayerNear(float nessesaryDistance)
        {
            var distanceToPlayer = Vector3.Distance(transform.position, Character.C.transform.position);
            if (distanceToPlayer <= nessesaryDistance)
                return true;
            
            return false;
        }
    }
}