using UnityEngine;
using TMPro; // Necesario para usar textos

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform cam;
   
    // VARIABLES DE UI
    public TextMeshProUGUI textoPuntos; // Arrastra aquí tu "TextoPuntos"
    public GameObject panelVictoria;    // Arrastra aquí tu "Panel" de victoria

    private int latas = 0;
    private int latasTotales;

    void Start()
    {
        // Cuenta cuántas latas pusiste en el nivel automáticamente
        latasTotales = GameObject.FindGameObjectsWithTag("Item").Length;
        ActualizarTexto();
    }

    void Update()
    {
        // (Tu código de movimiento se mantiene igual)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDir.normalized * speed * Time.deltaTime, Space.World);
          
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            latas++;
            Destroy(other.gameObject);
            ActualizarTexto();
            verificarVictoria();
        }
    }

    void ActualizarTexto()
    {
        // Actualiza el contador en pantalla
        if (textoPuntos != null)
        {
            textoPuntos.text = "Latas: " + latas + " / " + latasTotales;
        }
    }

    void verificarVictoria()
    {
        // Si juntamos todas las latas...
        if (latas >= latasTotales)
        {
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true); // ¡Muestra el cartel de ganar!
                // Opcional: Detener el juego
                // Time.timeScale = 0f; 
            }
            Debug.Log("¡Juego Terminado!");
        }
    }
}