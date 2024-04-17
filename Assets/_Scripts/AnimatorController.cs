using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    /*private void Awake()
    {
        _animator = GetComponent<Animator>();
    }*/

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayRun()
    {
        _animator.Play("Run");
    }
    public void PlayHit()
    {
        if (Random.Range(0, 1) == 0)
        {
            _animator.Play("LowHit");
        }
        else
        {
            _animator.Play("HighHit");
        }
    }
}