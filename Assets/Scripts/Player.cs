using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
  public float Speed;
  public float JumpForce;


  public bool isJumping;
  public bool doubleJump;

  private Rigidbody2D rig;
  private Animator anim;

  private string[] resetJumpTags = { "Ground", "Bricks" };

  // Start is called before the first frame update
  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Jump();
  }

  void Move()
  {
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * Speed;

    if (Input.GetAxis("Horizontal") > 0f)
    {
      anim.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    if (Input.GetAxis("Horizontal") < 0f)
    {
      anim.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    if (Input.GetAxis("Horizontal") == 0f)
    {
      anim.SetBool("walk", false);
    }
  }

  void Jump()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (!isJumping || doubleJump)
      {
        anim.SetBool("jump", true);
        this.ForceToJump();
        doubleJump = !doubleJump;
      }
    }
  }

  void ForceToJump()
  {
    float CalculateJumpForce = JumpForce;

    if (doubleJump) CalculateJumpForce *= 1.2f;
    rig.AddForce(new Vector2(0f, CalculateJumpForce), ForceMode2D.Impulse);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    bool isResetingJumpTag = -1 < Array.IndexOf(this.resetJumpTags, collision.gameObject.tag);

    if (isResetingJumpTag)
    {
      isJumping = false;
      anim.SetBool("jump", false);
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    bool isResetingJump = -1 < Array.IndexOf(this.resetJumpTags, collision.gameObject.tag);

    if (isResetingJump)
    {
      isJumping = true;
    }
  }
}
