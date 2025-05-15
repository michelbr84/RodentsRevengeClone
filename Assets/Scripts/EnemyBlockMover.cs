using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Tamanho de cada bloco da grade.")]
    public float gridSize = 1.0f; // Tamanho da grade, editável no Inspector

    [Tooltip("Tempo entre cada movimento aleatório (em segundos).")]
    public float moveInterval = 1.0f; // Intervalo de tempo entre os movimentos, editável no Inspector

    [Tooltip("Tempo antes de se transformar se o movimento for bloqueado (em segundos).")]
    public float blockedTimeLimit = 3.0f; // Tempo antes da transformação, editável no Inspector

    [Tooltip("Objeto em que o Enemy se transforma após ficar bloqueado.")]
    public GameObject transformIntoObject; // Objeto no qual o Enemy se transforma, editável no Inspector

    private Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private float blockedTime = 0f; // Tempo que o Enemy ficou bloqueado

    private void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);

            // Escolhe uma direção aleatória
            Vector2 randomDirection = directions[Random.Range(0, directions.Length)];
            Vector3 targetPosition = transform.position + (Vector3)randomDirection * gridSize;

            // Verifica se a posição destino está livre e não contém o Player ou um Block
            Collider2D hit = Physics2D.OverlapCircle(targetPosition, 0.1f);

            if (hit == null || (!hit.CompareTag("Block") && !hit.CompareTag("Player")))
            {
                // Se a posição está livre, move o Enemy
                transform.position = targetPosition;
                blockedTime = 0f; // Reseta o tempo bloqueado
                Debug.Log("Enemy moveu para: " + targetPosition);
            }
            else
            {
                // Incrementa o tempo bloqueado se não puder se mover
                blockedTime += moveInterval;
                Debug.Log("Movimento bloqueado por " + hit.tag + " em: " + targetPosition);

                // Se o tempo bloqueado exceder o limite, transformar o Enemy
                if (blockedTime >= blockedTimeLimit)
                {
                    TransformIntoObject();
                    yield break; // Encerra a coroutine após a transformação
                }
            }
        }
    }

    private void TransformIntoObject()
    {
        if (transformIntoObject != null)
        {
            Instantiate(transformIntoObject, transform.position, transform.rotation);
            Destroy(gameObject); // Remove o objeto Enemy após a transformação
            Debug.Log("Enemy se transformou em " + transformIntoObject.name);
        }
        else
        {
            Debug.LogWarning("Objeto de transformação não configurado!");
        }
    }
}
