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

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _movingToTarget = true;
    private bool _isMoving = false;

    void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + new Vector3(horizontalMoveDistance, verticalMoveHeight, depthMoveDistance);
        StartCoroutine(StartMovementAfterDelay());
    }

    void Update()
    {
        if (!_isMoving) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _movingToTarget ? _targetPosition : _startPosition, step);

        if (Vector3.Distance(transform.position, _movingToTarget ? _targetPosition : _startPosition) < 0.01f)
        {
            _movingToTarget = !_movingToTarget;
        }
    }

    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        _isMoving = true;
    }

    // Detect when player steps onto the platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    // Remove player from platform when they leave
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
