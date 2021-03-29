using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 _movementVector;
    [SerializeField] private float _period;

    const float tau = Mathf.PI* 2;

    private Vector3 _startPosition;
    private Vector3 _offset;
    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if(_period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / _period;
        float multiplier = Mathf.Sin(cycles * tau);
        float movementFactor = (multiplier + 1) / 2f;
        _offset = _movementVector * movementFactor;
    }

    private void FixedUpdate()
    {
        transform.position = _startPosition + _offset;
    }
}
