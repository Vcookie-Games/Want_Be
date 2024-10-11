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
    [SerializeField] private float offsetY;

    private Transform _transform;
    private int hashIsJumpAnimator;

    public UnityEvent onDoneJump;

    public bool IsJumping { get; private set; }
    public float JumpDuration { get { return jumpDuration; } }
    public Transform Transform => _transform;
    private Coroutine jumpInPlaceProcess;


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

    public void JumpInPlace(Block block)
    {
        IsJumping = true;
        _animator.SetBool(hashIsJumpAnimator, IsJumping);
        if (jumpInPlaceProcess != null)
        {
            StopCoroutine(jumpInPlaceProcess);
            jumpInPlaceProcess = null;
        }
        jumpInPlaceProcess = StartCoroutine(JumpInPlaceProcess(jumpDuration, offsetY, _transform.position, block.Top));
    }

    IEnumerator JumpInPlaceProcess(float duration, float height,Vector3 from, Transform to)
    {
        IsJumping = true;
        float time = 0f;
        _transform.position = from;
       
        
        while (time < duration)
        {
            time += Time.deltaTime;

            var middle = (from + to.position) / 2;
            middle.y += height;
            _transform.position = QuadraticLerpLockYAxis(from, to.position, time / duration, middle);
            yield return null;
        }

        _transform.position = to.position;
        onDoneJump?.Invoke();

        IsJumping = false;
        

        _animator.SetBool(hashIsJumpAnimator, IsJumping);
    }

    public void JumpEnd()
    {
        IsJumping = false;
        _animator.SetBool(hashIsJumpAnimator, IsJumping);
    }
    private Vector3 QuadraticLerpLockYAxis(Vector3 from, Vector3 to, float speed,Vector3 P1)
    {
        speed = Mathf.Clamp(speed, 0, 1);
        return (1 - speed) * (1 - speed) * from + 2 * (1 - speed) * speed * P1 + speed * speed * to;
    }
    public void UpdatePositionAccordingToBlock(Block block)
    {
        transform.position = block.Top.position;
    }

    public void ResetJumpInPlace()
    {
        if (jumpInPlaceProcess != null)
        {
            StopCoroutine(jumpInPlaceProcess);
            jumpInPlaceProcess = null;
        }

        IsJumping = false;
        _animator.SetBool(hashIsJumpAnimator, IsJumping);
    }

    private void DisableJump()
    {
        IsJumping = false;
        _animator.SetBool(hashIsJumpAnimator, IsJumping);
    }
        
}
