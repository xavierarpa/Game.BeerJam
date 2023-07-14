using UnityEngine;

public class ParedController : MonoBehaviour
{
    public Vector2 direction = Vector2.right; // Dirección en la que la pelota rebotará al colisionar con la pared

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión fue con la pelota
        if (collision.gameObject.CompareTag("Pelota"))
        {
            // Obtener la dirección actual de la pelota
            Vector2 ballDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            
            // Calcular la nueva dirección de la pelota utilizando la dirección de la pared y la dirección actual de la pelota
            Vector2 newBallDirection = Vector2.Reflect(ballDirection, direction).normalized;
            
            // Asignar la nueva dirección a la pelota
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = newBallDirection * collision.gameObject.GetComponent<PelotaController>().speed;
        }
    }
}
