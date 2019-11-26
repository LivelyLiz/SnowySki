using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour, IResetable
{
    public Animator CharacterAnim;
    public string AnimTrigSuffix;

    private readonly string _hit = "isHit_";

    public void Reset()
    {
        CharacterAnim.SetBool(_hit + AnimTrigSuffix, false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            CharacterAnim.SetBool(_hit+AnimTrigSuffix, true);
            CharacterAnim.SetTrigger("hit_animation");
            GameStateManager.Instance.InitGameOver();
        }
    }
}
