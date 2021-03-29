using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 _direstion;
    [SerializeField] private float _speed;
    [SerializeField] private Material _material;
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material; 
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset += _direstion * _speed * Time.deltaTime;
    }
}
