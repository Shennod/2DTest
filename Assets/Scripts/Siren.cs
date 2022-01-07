using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Siren : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private float _sirenVolume = 0;
    private bool _thiefInHouse = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 1f;
    }

    private void Update()
    {
        if (_thiefInHouse)
        {
            if (_audioSource.volume == 0f)
            {
                _sirenVolume = 1f;
            }
            else if (_audioSource.volume == 1f)
            {
                _sirenVolume = 0f;
            }

            PlaySound();
        }
    }

    public void PlaySound()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _sirenVolume, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PhysicsMovement>(out PhysicsMovement player))
        {
            _thiefInHouse = true;
            _audioSource.PlayOneShot(_audioClip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PhysicsMovement>(out PhysicsMovement player))
        {
            _thiefInHouse = false;
            _audioSource.Stop();
        }
    }
}
