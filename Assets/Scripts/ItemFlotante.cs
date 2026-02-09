using UnityEngine;

public class ItemFlotante : MonoBehaviour
{
    // Variables para configurar el movimiento
    public float velocidadGiro = 50f;
    public float amplitudFlote = 0.2f; // Qué tan alto sube y baja
    public float velocidadFlote = 1f;  // Qué tan rápido sube y baja

    private Vector3 posInicial;

    void Start()
    {
        // Guardamos la posición donde pusiste la lata para que flote sobre ese lugar
        posInicial = transform.position;
    }

    void Update()
    {
        // 1. Girar sobre su propio eje (Y)
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime);

        // 2. Calcular la nueva altura usando Seno (baja y sube suavemente)
        float nuevoY = posInicial.y + Mathf.Sin(Time.time * velocidadFlote) * amplitudFlote;

        // 3. Aplicar la posición
        transform.position = new Vector3(transform.position.x, nuevoY, transform.position.z);
    }
}