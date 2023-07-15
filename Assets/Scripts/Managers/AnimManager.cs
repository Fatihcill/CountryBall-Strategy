using UnityEngine;

public static class AnimationTypes
{
    public static readonly string Walk = "Walk";
    public static readonly string Idle = "Idle";
    public static readonly string Attack = "Attack";
}

public class AnimManager
{
    private Animator _animator;
    
    public AnimManager(Animator animator)
    {
        _animator = animator;
    }

    public void SetAnim(string type, bool run)
    {
        _animator.SetBool(type, run);
    }

    public void PlayOne(string type)
    {
        _animator.Play(type);
    }
}
