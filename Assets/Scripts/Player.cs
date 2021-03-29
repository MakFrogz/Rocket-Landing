using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _thrustForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ParticleSystem _thrustParticle;
    [SerializeField] private AudioClip _thrustSound;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Player_Input _inputActions;

    private bool _isMove;
    private Vector3 _rotation;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _inputActions = new Player_Input();
        _inputActions.Player.Thrust.performed += ctx => OnThrust();
        _inputActions.Player.Thrust.canceled += ctx => OnThrust();
        _inputActions.Player.Rotation.performed += ctx => OnRotation(ctx);
        _inputActions.Player.Rotation.canceled += ctx => OnRotation(ctx);
        _isMove = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();
    }

    private void Move()
    {
        if (!_isMove)
        {
            _thrustParticle.Stop();
            AudioManager.Instance.StopSFX();
            return;
        }
        _thrustParticle.Play();
        AudioManager.Instance.PlaySFX(_thrustSound);
        _rigidbody.AddRelativeForce(Vector3.up * _thrustForce * Time.deltaTime);
    }

    private void Rotation()
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(_rotation * _rotationSpeed * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

    private void OnThrust()
    {
        _isMove = !_isMove;
    }

    private void OnRotation(InputAction.CallbackContext ctx)
    {
        _rotation = new Vector3(0f, 0f, ctx.ReadValue<float>());
    }

    private void OnEnable() => _inputActions.Player.Enable();
    private void OnDisable()
    {
        _thrustParticle.Stop();
        AudioManager.Instance.StopSFX();
        _inputActions.Player.Disable();
    }
}
