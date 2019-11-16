using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour
{
    public AddativeTransform TurnOffAddTrans;
    public PositionFollow TurnOffPosFoll;

    public KeyCode JumpKey;

    public JumpParameters JumpParams;

    public Animator CharacterAnim;
    public string AnimTrigSuffix;

    public Rigidbody Rb;

    public Collider JumpResetCollider;

    private readonly string _jump = "jump_";
    private readonly string _air = "air_";
    private readonly string _land = "land_";

    private bool _isGrounded = true;

    private Vector3 _velocity = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(JumpKey))
        {
            jump();
        }

        if (!_isGrounded)
        {
            updateJump(Time.smoothDeltaTime);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col == JumpResetCollider)
        {
            ground();
        }
    }

    private void jump()
    {
        if (!_isGrounded) { return; }

        if (TurnOffAddTrans != null)
        {
            TurnOffAddTrans.enabled = false;
        }

        if (TurnOffPosFoll != null)
        {
            TurnOffPosFoll.enabled = false;
        }

        _velocity = JumpParams.JumpVelocity * Vector3.up;
        CharacterAnim.SetTrigger(_jump + AnimTrigSuffix);

        _isGrounded = false;
    }

    private void updateJump(float deltaTime)
    {
        _velocity -= JumpParams.Gravity * deltaTime * Vector3.up;

        if (_velocity.y > 0)
        {
            transform.Translate(_velocity * deltaTime, Space.World);
        }
        else
        {
            transform.Translate(_velocity*JumpParams.FallMultiplier * deltaTime, Space.World);
        }
    }

    private void ground()
    {
        if (_isGrounded) { return; }

        if (TurnOffAddTrans != null)
        {
            TurnOffAddTrans.enabled = true;
        }

        if (TurnOffPosFoll != null)
        {
            TurnOffPosFoll.enabled = true;
        }


        _velocity = Vector3.zero;
        CharacterAnim.SetTrigger(_land + AnimTrigSuffix);

        _isGrounded = true;
    }
}
