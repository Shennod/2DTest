using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Siren : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private float _targetVolume = 0;
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
            StartCoroutine(PlaySound());
        }
    }

    private IEnumerator PlaySound()
    {
        if (_audioSource.volume == 0f)
        {
            _targetVolume = 1f;
        }
        else if (_audioSource.volume == 1f)
        {
            _targetVolume = 0f;
        }

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, Time.deltaTime);

        yield return null;
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
            StopCoroutine(PlaySound());
            _audioSource.Stop();
        }
    }
}
