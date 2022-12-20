using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.numberOfCoins++;
            FindObjectOfType<AudioManager>().PlaySound("Coin");
            Destroy(gameObject);
        }
    }
}
