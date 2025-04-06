using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float verticalMoveHeight = 3f;
    [SerializeField] private float horizontalMoveDistance = 3f;
    [SerializeField] private float depthMoveDistance = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float pauseTimeAtEnds = 0.5f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _movingToTarget = true;
    private bool _isMoving = false;
    private Transform _playerOnPlatform;

    private Vector3 _lastPosition;

    void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + new Vector3(horizontalMoveDistance, verticalMoveHeight, depthMoveDistance);
        _lastPosition = _startPosition;
        StartCoroutine(StartMovementAfterDelay());
    }

    void Update()
    {
        if (!_isMoving) return;

        float step = speed * Time.deltaTime;
        Vector3 destination = _movingToTarget ? _targetPosition : _startPosition;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        // Move player with platform manually
        if (_playerOnPlatform != null)
        {
            Vector3 platformDelta = transform.position - _lastPosition;
            _playerOnPlatform.position += platformDelta;
        }

        _lastPosition = transform.position;

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            _isMoving = false;
            StartCoroutine(PauseAtEndBeforeSwitching());
        }
    }

    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        _isMoving = true;
    }

    private IEnumerator PauseAtEndBeforeSwitching()
    {
        yield return new WaitForSeconds(pauseTimeAtEnds);
        _movingToTarget = !_movingToTarget;
        _isMoving = true;
    }

    // Detect when player steps onto the platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerOnPlatform = other.transform;
        }
    }

    // Remove player from platform when they leave
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _playerOnPlatform == other.transform)
        {
            _playerOnPlatform = null;
        }
    }
}
