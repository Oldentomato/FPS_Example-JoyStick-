using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Header("PlayerStat")]
    public float f_RunSpeed;
    public float f_WalkSpeed;
    public float f_JumpPower;
    public float f_Gravity;
    public float f_CrouchSpeed;
    public float f_AimSpeed;
    public int i_MaxHealth;

    private CharacterController GetCharCtrl;
    private Vector3 moveDirection;

    private float f_PlayerSpped;
    private float h, v;

    private void Initial()
    {
        GetCharCtrl = GetComponent<CharacterController>();
        f_WalkSpeed = 3f;
        f_JumpPower = 4f;
        f_Gravity = 9.81f;
        f_RunSpeed = 6f;
        f_CrouchSpeed = 1f;
        moveDirection = Vector3.zero;
        h = 0f;
        v = 0f;
        f_PlayerSpped = 0f;
    }

    public bool isMoving()
    {
        if (h == 0 && v == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool isCrouching()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            f_PlayerSpped = f_CrouchSpeed;
            return true;
        }
        else
        {
            f_PlayerSpped = f_WalkSpeed;
            return false;
        }
    }

    public bool isRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isMoving())
        {
            f_PlayerSpped = f_RunSpeed;
            return true;
        }
        else
        {
            f_PlayerSpped = f_WalkSpeed;
            return false;
        }
    }

    private void Move()
    {
        if (GetCharCtrl.isGrounded.Equals(true))
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= f_PlayerSpped;
            Jump();
        }
        moveDirection.y -= f_Gravity * Time.deltaTime;
        GetCharCtrl.Move(moveDirection * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = f_JumpPower;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initial();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
