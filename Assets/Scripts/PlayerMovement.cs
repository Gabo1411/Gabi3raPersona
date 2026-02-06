using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cam; // Arrastra aquí la Main Camera

    void Update()
    {
        // 1. Obtener inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 2. Calcular dirección (independiente de la rotación actual)
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // 3. Si nos estamos moviendo...
        if (direction.magnitude >= 0.1f)
        {
            // Calcular el ángulo hacia donde mira la cámara + el input
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // Rotar al personaje inmediatamente hacia esa dirección
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Calcular la dirección de avance correcta
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Mover usando Translate (sin CharacterController)
            transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}