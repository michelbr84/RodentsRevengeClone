using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : MonoBehaviour
{
    [Tooltip("Tamanho de cada bloco da grade.")]
    public float gridSize = 1.0f; // Tamanho da grade, editável no Inspector

    [Tooltip("Tag do bloco que o player pode mover.")]
    public string blockTag = "Block"; // Tag do bloco, editável no Inspector

    private Vector2 moveDirection;

    void Update()
    {
        // Captura a entrada do usuário
        moveDirection = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
        }

        // Se houver uma entrada de movimento
        if (moveDirection != Vector2.zero)
        {
            Vector3 targetPosition = transform.position + (Vector3)moveDirection * gridSize;

            // Verificar se há blocos na direção do movimento
            List<Collider2D> blocksToMove = new List<Collider2D>();
            Vector3 checkPosition = targetPosition;

            while (true)
            {
                Collider2D hit = Physics2D.OverlapCircle(checkPosition, 0.1f);
                if (hit != null && hit.CompareTag(blockTag))
                {
                    blocksToMove.Add(hit);
                    checkPosition += (Vector3)moveDirection * gridSize;
                }
                else
                {
                    break;
                }
            }

            // Verifica se a última posição está livre para mover todos os blocos
            Collider2D blockHit = Physics2D.OverlapCircle(checkPosition, 0.1f);
            if (blockHit == null)
            {
                // Move todos os blocos de trás para frente
                for (int i = blocksToMove.Count - 1; i >= 0; i--)
                {
                    blocksToMove[i].transform.position += (Vector3)moveDirection * gridSize;
                }

                // Move o Player
                transform.position = targetPosition;
            }
            else
            {
                Debug.Log("Movimento bloqueado. O caminho está obstruído.");
            }
        }
    }
}
