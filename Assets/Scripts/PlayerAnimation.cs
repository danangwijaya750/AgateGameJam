using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour 
{
    public event Action OnAttackStart;
    public event Action OnSlashEnd;
    public event Action OnHit;
    public event Action OnSlashStart;
    public event Action OnAttackEnd;

    public void SlashEnd()
    {
        OnSlashEnd?.Invoke();
    }

    public void SlashStart()
    {
        OnSlashStart?.Invoke();
    }

    public void Hit()
    {
        OnHit?.Invoke();
    }

    public void AttackStart()
    {
        OnAttackStart?.Invoke();
    }

    public void AttackEnd()
    {
        OnAttackEnd?.Invoke();
    }
}