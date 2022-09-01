using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  private CircleCollider2D circleCollider;

  public GameObject collected;
  public int score;

  // Start is called before the first frame update
  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    circleCollider = GetComponent<CircleCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.tag == "Player")
    {
      spriteRenderer.enabled = false;
      circleCollider.enabled = false;
      collected.SetActive(true);

      Destroy(gameObject, 0.2f);

      GameController.instance.UpdateScoreText(score);
    }
  }
}
