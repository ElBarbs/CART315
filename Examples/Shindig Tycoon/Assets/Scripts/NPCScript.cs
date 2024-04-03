using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCScript : MonoBehaviour
{
    public Guid ID { get; set; }
    public GameObject MovingTarget { get; set; }
    
    private Vector3 _target;
    private NavMeshAgent _agent;
    
    private float _waitTime, _currentTime;
    private float _drunknessLevel, _happinessLevel;
    private float _range, _speed, _acceleration;

    private bool _isLeaving;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        if (RandomPoint(transform.position, _range, out var point))
        {
            _target = point;
        }

        _waitTime = Random.Range(0f, 5f);
        _currentTime = 0f;

        _range = 10f;
        _speed = 3.5f;
        _acceleration = 8f;

        _drunknessLevel = Random.Range(0f, 0.6f);
        _happinessLevel = Random.Range(0.5f, 1f);

        _isLeaving = false;
    }
    
    private void Update()
    {
        _agent.speed = _speed - (_drunknessLevel * 2);
        _agent.acceleration = _acceleration - (_drunknessLevel * 4);

        if (_happinessLevel <= 0f && !_isLeaving)
        {
            _isLeaving = true;
            _target = new Vector3(-26, -17);
        }

        _target = MovingTarget && !_isLeaving ? MovingTarget.transform.position : _target;

        _agent.SetDestination(_target);

        if (Vector3.Distance(transform.position, _target) <= 1.75)
        {
            if (_isLeaving)
            {
                Destroy(gameObject);
            }

            if (MovingTarget is not null)
            {
                MovingTarget = null;
            }
            
            if (_currentTime >= _waitTime)
            {
                if (RandomPoint(transform.position, _range, out var point))
                {
                    _target = point;
                }
                
                _currentTime = 0f;
                _waitTime = Random.Range(0f, 5f);
            } else
            {
                _currentTime += Time.deltaTime;
            }
        }
    }
    
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            var randomPoint = center + Random.insideUnitSphere * range;
            
            if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        
        result = Vector3.zero;
        return false;
    }
    
    
}