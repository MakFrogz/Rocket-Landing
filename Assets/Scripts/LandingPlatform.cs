using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandingPlatform : MonoBehaviour
{
    [SerializeField] private AudioClip _successSound;
    
    public UnityEvent Event;

    private void Start()
    {
        Event.AddListener(delegate { AudioManager.Instance.PlaySFX(_successSound); });
    }
    public void InvokeSequence()
    {
        Event.Invoke();
    }
}
