using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  // Start is called before the first frame update
  public int totalScore;
  public Text scoreText;

  public static GameController instance;

  void Start()
  {
    instance = this;
  }

  public void UpdateScoreText(int score)
  {
    this.totalScore += score;
    scoreText.text = totalScore.ToString();
  }
}
