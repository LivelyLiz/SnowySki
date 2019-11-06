using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator Anim;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetTrigger("jump_middle");
            Anim.SetTrigger("jump_bottom");
        }
    }
}
