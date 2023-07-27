using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.Log("Collider is null on pressure pad");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(other.transform.position, transform.position) < 0.05f && other.CompareTag("Box"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb == null || rb.isKinematic)
            {
                return;
            }
            rb.isKinematic = true;
            _meshRenderer.material.color = Color.blue;
            _collider.enabled = false;
        }
    }
}
