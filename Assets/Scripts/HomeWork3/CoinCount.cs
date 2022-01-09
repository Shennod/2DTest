using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private int _count = 0;

    private void Start()
    {
        _text.text += _count;
    }

    public void OnCoinCollected()
    {
        _count++;
        _text.text = "COIN: " + _count.ToString();
    }
}
