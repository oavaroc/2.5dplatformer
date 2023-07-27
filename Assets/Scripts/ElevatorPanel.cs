using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _lightMat;

    [SerializeField]
    private int _coinsRequired;

    [SerializeField]
    private ElevatorPlatform _elevatorPlatform;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision with: "+other.name);
        if (other.CompareTag("Player"))
        {
            if (Keyboard.current.eKey.isPressed && UIManager.Instance.GetCoins() >= _coinsRequired)
            {
                Debug.Log("E key pressed");
                _lightMat.material.color = Color.green;
                StartCoroutine(CallElevatorRoutine());

            }
        }
    }

    IEnumerator CallElevatorRoutine()
    {
        yield return new WaitForSeconds(1f);
        _elevatorPlatform.SwapDestination(1);
        yield return new WaitForSeconds(5f);
        _lightMat.material.color = Color.red;
    }
}
