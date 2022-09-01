using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float Speed;
  public float JumpForce;


  public bool isJumping;
  public bool doubleJump;

  private Rigidbody2D rig;
  private Animator anim;

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
    Debug.Log(Input.GetButtonDown("Jump"));
    Debug.Log(isJumping);
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
    Debug.Log(collision.gameObject.tag);
    Debug.Log(isJumping);
    if (collision.gameObject.tag == "Ground")
    {
      isJumping = false;
      anim.SetBool("jump", false);
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ground")
    {
      isJumping = true;
    }
  }
}
