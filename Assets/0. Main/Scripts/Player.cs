using DG.Tweening;
using QuanUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpDuration = .5f;
    [SerializeField] private float jumpPower = 3;
    [SerializeField] private Animator _animator;

    private Transform _transform;
    private int hashIsJumpAnimator;

    public UnityEvent onDoneJump;

    public bool IsJumping { get; private set; }
    public float JumpDuration { get { return jumpDuration; } }
    public Transform Transform => _transform;


    private void Awake()
    {
        IsJumping = false;
        _transform = transform;

        hashIsJumpAnimator = Animator.StringToHash("IsJumping");
    }

    public void JumpTo(Block block)
    {
        IsJumping = true;
        _animator.SetBool(hashIsJumpAnimator, IsJumping);

        _transform.DOJump(block.Top.position, jumpPower, 1, jumpDuration).OnComplete(() =>
        {
            onDoneJump?.Invoke();

            IsJumping = false;

            _transform.localScale = new Vector3
                (block.Direction == BlockDirection.Right ? 1 : -1,
                _transform.localScale.y, _transform.localScale.z);
            //_transform.DOScaleX(block.Direction == BlockDirection.Right ? 1 : -1, jumpDuration / 2f);

            _animator.SetBool(hashIsJumpAnimator, IsJumping);
        });
    }
}
