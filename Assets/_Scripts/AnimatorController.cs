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
        Debug.Log("Is dying :" + _animator.GetBool("IsDying"));
        Debug.Log("Is happy :" + _animator.GetBool("IsAttacking"));
    }
    public void PlayIdle()
    {
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsAttacking", false);
        _animator.SetBool("IsDying", false);
        _animator.SetBool("IsHappy", false);

        //_animator.Play("Idle");
    }

    public void PlayRun()
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetBool("IsAttacking", false);
        _animator.SetBool("IsDying", false);
        _animator.SetBool("IsHappy", false);
        //_animator.Play("Run");
    }
    public void PlayHit()
    {
        _animator.SetBool("IsAttacking", true);
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsDying", false);
        _animator.SetBool("IsHappy", false);
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
        _animator.SetBool("IsDying", false);
        _animator.SetBool("IsHappy", false);

    }
    public void PlayIsHappy()
    {
        _animator.SetBool("IsHappy", true);
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsDying", false);
        _animator.SetBool("IsAttacking", false);

    }
    public void PlayIsDying()
    {
        _animator.SetBool("IsHappy", false);
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsDying", true);
        _animator.SetBool("IsAttacking", false);

    }
}