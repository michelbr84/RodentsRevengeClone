using UnityEngine;

public class MouseBoundaries : MonoBehaviour
{
    public float minX = -5f;  // Limite mínimo para X
    public float maxX = 5f;   // Limite máximo para X
    public float minY = -5f;  // Limite mínimo para Y
    public float maxY = 5f;   // Limite máximo para Y

    private Vector3 position;  // Para armazenar a posição do rato

    void Update()
    {
        // Captura a posição atual do rato
        position = transform.position;

        // Aplica os limites de X e Y
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Atualiza a posição do rato
        transform.position = position;
    }
}
