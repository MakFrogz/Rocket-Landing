using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _delayLoadLevel;
    [SerializeField] private ParticleSystem _crashParticle;
    [SerializeField] private AudioClip _crashSFX;

    private AudioSource _audioSource;
    private bool _isAlive;

    private Vector3 _offset;
    private GameObject _platform;
    private void Awake()
    {
        _isAlive = true;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 45 || Mathf.Abs(transform.position.y) > 45 && _isAlive)
        {
            Debug.Log("Obstacle!");
            CrashSequence();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Respawn!");
                break;
            case "Obstacle":
                Debug.Log("Obstacle!");
                CrashSequence();
                break;
            default:
                Debug.Log("Unknown object!");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finish!");
            FinishSequence();
            LandingPlatform padHandler = other.GetComponent<LandingPlatform>();
            if (padHandler != null)
            {
                padHandler.InvokeSequence();
            }
            _offset = transform.position - other.transform.position;
            _platform = other.gameObject;
        }
    }

    private void FinishSequence()
    {
        if (!_isAlive)
        {
            return;
        }
        GetComponent<Player>().enabled = false;
        Invoke("NextLevel", _delayLoadLevel);
    }

    private void CrashSequence()
    {
        if (!_isAlive)
        {
            return;
        }
        GetComponent<Player>().enabled = false;
        _crashParticle.Play();
        AudioManager.Instance.PlaySFX(_crashSFX);
        Invoke("ReloadLevel", _delayLoadLevel);
        _isAlive = false;
    }

    private void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }

    private void ReloadLevel()
    {
        GameManager.Instance.ReloadLevel();
    }

    private void LateUpdate()
    {
        if(_platform != null)
        {
            transform.position = _platform.transform.position + _offset;
        }
    }
}
