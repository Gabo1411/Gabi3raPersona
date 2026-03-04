using UnityEngine;
using TMPro;

public class TextoFlotante : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidadSubida = 2f;
    public float tiempoVida = 1.5f;

    void Start()
    {
        // El texto se autodestruye para no llenar la memoria
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        // 1. Mover hacia arriba suavemente
        transform.Translate(Vector3.up * velocidadSubida * Time.deltaTime);

        // 2. Mirar SIEMPRE a la cámara (Efecto Billboard)
        // Esto evita que el texto salga de espaldas o invisible
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    // Esta función recibe el mensaje (ej: "+5s") y lo pone en el componente de texto
    public void SetTexto(string mensaje)
    {
        // Intenta buscar el componente de Texto 3D (World)
        TextMeshPro texto3D = GetComponent<TextMeshPro>();
        if (texto3D != null)
        {
            texto3D.text = mensaje;
        }
        else
        {
            // Si no es 3D, intenta buscar el de UI (por si acaso)
            TextMeshProUGUI textoUI = GetComponent<TextMeshProUGUI>();
            if (textoUI != null)
            {
                textoUI.text = mensaje;
            }
        }
    }
}