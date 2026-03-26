using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class YvanController : MonoBehaviour
{
    private UnityEvent _OnGameEvent, _OnRestEvent, _OnEatEvent, _OnRelieveEvent;
    
    [SerializeField] private Transform _entertainmentStation, _bathroomStation, _hungerStation, _sleepStation;
    [field: SerializeField] public float Entertainment { get; private set; }
    [field: SerializeField] public float MaxEntertainment { get; private set; }
    [field: SerializeField] public float Bladder { get; private set; }
    [field: SerializeField] public float MaxBladder { get; private set; }
    [field: SerializeField] public float Stomach { get; private set; }
    [field: SerializeField] public float MaxStomach { get; private set; }
    [field: SerializeField] public float Exhaustion { get; private set; }
    [field: SerializeField] public float MaxExhaustion { get; private set; }
    
    private GameDirector _gameDirector;
    private NavMeshAgent _navMeshAgent;
    private Blackboard _blackboard;
    
    private void OnEnable()
    {
        // Subscribe to GameDirector's events instead
        if (_gameDirector != null)
        {
            _gameDirector.OnGameEvent?.AddListener(Game);
            _gameDirector.OnRestEvent?.AddListener(Rest);
            _gameDirector.OnEatEvent?.AddListener(Eat);
            _gameDirector.OnRelieveEvent?.AddListener(RelieveSelf);
        }
    }
    
    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (_gameDirector != null)
        {
            _gameDirector.OnGameEvent?.RemoveListener(Game);
            _gameDirector.OnRestEvent?.RemoveListener(Rest);
            _gameDirector.OnEatEvent?.RemoveListener(Eat);
            _gameDirector.OnRelieveEvent?.RemoveListener(RelieveSelf);
        }
    }

    private void Awake()
    {
        SetStats();
        
        _gameDirector = FindAnyObjectByType<GameDirector>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _blackboard = new Blackboard(this, _entertainmentStation, _bathroomStation, _hungerStation, _sleepStation);
        
        _gameDirector.AddBlackboard(_blackboard);
    }

    private void Update()
    {
        
    }

    private void SetStats()
    {
        Entertainment = 100f;
        MaxEntertainment = 100f;
        Stomach = 100f;
        MaxStomach = 100f;
        
        Bladder = 0f;
        MaxBladder = 100f;
        Exhaustion = 0f;
        MaxExhaustion = 100f;
    }
    
    #region Unity Events
    private void Game()
    {
        _navMeshAgent.destination = _entertainmentStation.position;
    }
    private void Rest()
    {
        _navMeshAgent.destination = _sleepStation.position;
    }
    private void Eat()
    {
        _navMeshAgent.destination = _hungerStation.position;
    }
    private void RelieveSelf() 
    {
        _navMeshAgent.destination = _bathroomStation.position;
    }
    
    #endregion
}
