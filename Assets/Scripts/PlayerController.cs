using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Pontuação inicial do jogador.")]
    public int playerScore = 0; // Pontuação inicial, editável no Inspector

    [Tooltip("Texto UI para exibir a pontuação.")]
    public Text scoreText; // Texto UI para exibir a pontuação, editável no Inspector

    [Tooltip("Quantidade de pontos por Consumable.")]
    public int pointsPerConsumable = 1; // Pontos ganhos por Consumable, editável no Inspector

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Consumable"))
        {
            // Incrementa a pontuação
            playerScore += pointsPerConsumable;

            // Atualiza o texto da pontuação
            UpdateScoreText();

            // Destrói o objeto consumível
            Destroy(collision.gameObject);

            Debug.Log("Player pegou um Consumable! Pontuação: " + playerScore);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore;
        }
        else
        {
            Debug.LogWarning("Texto de pontuação não configurado!");
        }
    }
}
