using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Animator CharacterAnim;
    public string AnimTrigSuffix;

    private readonly string _hit = "isHit_";

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            CharacterAnim.SetBool(_hit+AnimTrigSuffix, true);
            GameStateManager.Instance.InitGameOver();
        }
    }
}
