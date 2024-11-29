using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool isMoving)
    {
        _animator.SetBool("1_Move", isMoving);
    }
}
