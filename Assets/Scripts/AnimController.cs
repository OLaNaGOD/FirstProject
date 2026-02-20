using UnityEngine;

public class AnimController : Singleton<AnimController>
{
    private GameObject _animObj;
    private Animator _animator;
    private readonly int hitId = Animator.StringToHash("HitTrigger");
    public GameObject AnimObj
    {
        get { return _animObj; }
        set
        {
            _animObj = value;
            GetAnimator();
        }
    }

    private void GetAnimator()
    {
        _animator = AnimObj.GetComponent<Animator>();
    }

    public void SetTrigger()
    {
        _animator.SetTrigger(hitId);
    }
}
