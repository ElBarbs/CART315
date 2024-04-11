using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCScript : MonoBehaviour
{
    public Guid ID { get; set; }
    public GameObject MovingTarget { get; set; }

    public GameObject vomitPrefab;
    
    private Vector3 _target;
    private NavMeshAgent _agent;

    private SpriteRenderer _emoticon;
    
    private float _waitTime, _currentTime;
    private float _drunknessLevel, _happinessLevel;
    private float _range, _speed, _acceleration;

    private bool _isLeaving;
    private Coroutine _findNextTarget, _displayEmoticon;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        if (RandomPoint(transform.position, _range, out var point))
        {
            _target = point;
        }

        _emoticon = transform.GetChild(0).GetComponent<SpriteRenderer>();

        _waitTime = Random.Range(0f, 5f);
        _currentTime = 0f;

        _range = 10f;
        _speed = Random.Range(2.5f, 4.5f);
        _acceleration = Random.Range(7.5f, 9f);

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
                _findNextTarget = StartCoroutine(FindNextTarget());
                
                _currentTime = 0f;
                _waitTime = Random.Range(0f, 5f);
            } else
            {
                _currentTime += Time.deltaTime;
            }
        }

        if (Random.Range(1f, 100f) > 99.95f)
        {
            _displayEmoticon = StartCoroutine(DisplayEmoticon());
        }

        if (Random.Range(1f, 100f) > 100f - (0.075f * _drunknessLevel))
        {
            Instantiate(vomitPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateMeter("Trash", -5f);
        }
    }

    private void OnDestroy()
    {
        StopCoroutines();
    }

    private void StopCoroutines()
    {
        if (_findNextTarget != null)
        {
            StopCoroutine(_findNextTarget);
        }

        if (_displayEmoticon != null)
        {
            StopCoroutine(_displayEmoticon);
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
    
    private IEnumerator FindNextTarget()
    {
        if (RandomPoint(transform.position, _range, out var point))
        {
            _target = point;
        }
        
        yield return true;
    }
    
    private IEnumerator DisplayEmoticon()
    {
        _emoticon.sprite = NPCManager.Instance.GetRandomEmote();
        
        // Ensure the sprite is visible
        _emoticon.enabled = true;

        // Wait for the specified amount of seconds
        yield return new WaitForSeconds(2f);

        // Hide the sprite
        _emoticon.enabled = false;
    }
}