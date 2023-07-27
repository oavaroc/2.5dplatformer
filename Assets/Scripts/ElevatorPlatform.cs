using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform[] _points;
    [SerializeField]
    private float _speed;

    private Transform _destination;

    private void Start()
    {
        _destination = _points[0];
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination.position, Time.deltaTime * _speed);
    }

    public void SwapDestination(int destinationIndex)
    {
        _destination = _points[destinationIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision with: " + other.name);
        if (other.CompareTag("Player"))
        {
            if (Keyboard.current.eKey.isPressed )
            {
                SwapDestination(0);

            }
        }
    }
}
