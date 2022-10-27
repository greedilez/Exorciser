using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnimator : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private PlayerDead _playerDead;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() => TurnWalkingAnimationOnWalking();

    public void TurnWalkingAnimationOnWalking() {
        if (!_playerDead.IsPlayerDead) {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            _animator.SetBool("Walking", (horizontal != 0 || vertical != 0));
        }
        else _animator.SetBool("Walking", false);
    }
}
