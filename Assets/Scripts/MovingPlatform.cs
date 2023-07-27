using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points;
    [SerializeField]
    private float _speed;

    private int _destinationIndex = 0;

    private Transform _destination;

    private bool _keepMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        _destination = _points[_destinationIndex];
        StartCoroutine(MoveRoutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);
    }

    IEnumerator MoveRoutine()
    {
        while (_keepMoving)
        {
            yield return new WaitForSeconds(5f);
            SwapDestination();

        }
    }

    private void SwapDestination()
    {
        _destinationIndex = (_destinationIndex + 1) % _points.Length;
        _destination = _points[_destinationIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;

    }
}
