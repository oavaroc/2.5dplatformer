using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinImported : MonoBehaviour
{
    //OnTriggerEnter
    //give the player a coin
    //destroy this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerImported player = other.GetComponent<PlayerImported>();

            if (player != null)
            {
                player.AddCoins();
            }

            Destroy(this.gameObject);
        }
    }

}
