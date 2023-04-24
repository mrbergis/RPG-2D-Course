using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player _player;
    
    void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void AnimationTrigger()
    {
        _player.AttackOver();
    }
}
