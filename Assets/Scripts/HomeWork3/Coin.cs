using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinCount _count;

    private void OnEnable()
    {
        _count = FindObjectOfType<CoinCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PhysicsMovement>(out PhysicsMovement player))
        {
            _count.OnCoinCollected();
            Destroy(this.gameObject);
        }
    }
}
