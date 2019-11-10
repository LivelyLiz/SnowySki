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
            updateJump();
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

        if (TurnOffAddTrans != null && TurnOffPosFoll != null)
        {
            TurnOffAddTrans.enabled = false;
            TurnOffPosFoll.enabled = false;
        }

        _velocity = JumpParams.JumpVelocity * Vector3.up;
        CharacterAnim.SetTrigger(_jump + AnimTrigSuffix);

        _isGrounded = false;
    }

    private void updateJump()
    {
        _velocity -= JumpParams.Gravity * Time.smoothDeltaTime * Vector3.up;

        if (_velocity.y > 0)
        {
            transform.Translate(_velocity * Time.smoothDeltaTime, Space.World);
        }
        else
        {
            transform.Translate(_velocity*JumpParams.FallMultiplier * Time.smoothDeltaTime, Space.World);
        }
    }

    private void ground()
    {
        if (_isGrounded) { return; }

        if (TurnOffAddTrans != null && TurnOffPosFoll != null)
        {
            TurnOffAddTrans.enabled = true;
            TurnOffPosFoll.enabled = true;
        }

        _velocity = Vector3.zero;
        CharacterAnim.SetTrigger(_land + AnimTrigSuffix);

        _isGrounded = true;
    }
}
