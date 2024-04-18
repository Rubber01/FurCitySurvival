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
    private void Update()
    {
        Debug.Log("Is moving :" + _animator.GetBool("IsMoving"));
        Debug.Log("Is attacking :" + _animator.GetBool("IsAttacking"));
    }
    public void PlayIdle()
    {
        _animator.SetBool("IsMoving", false);
        //_animator.Play("Idle");
    }

    public void PlayRun()
    {
        _animator.SetBool("IsMoving", true);
        //_animator.Play("Run");
    }
    public void PlayHit()
    {
        _animator.SetBool("IsAttacking", true);
/*
        if (Random.Range(0, 1) == 0)
        {
            _animator.Play("LowHit");
        }
        else
        {
            _animator.Play("HighHit");
        }*/
    }
    public void PlayStopHit()
    {
        _animator.SetBool("IsAttacking", false);

    }
}