using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración del Jugador")]
    public float speed = 5f;
    public Transform cam;
    public AudioSource audioJugador;

    [Header("Interfaz (UI)")]
    public TextMeshProUGUI textoPuntos;
    public GameObject panelVictoria;

    [Header("Configuración del Tiempo")]
    public TextMeshProUGUI textoTiempo;
    public GameObject panelDerrota;
    public float tiempoLimite = 60f;
    public float segundosGanados = 5f;

    [Header("Efectos Visuales")]
    public GameObject prefabTextoFlotante; // <--- ARRASTRA AQUÍ TU PREFAB "EfectoPuntos"

    private bool juegoTerminado = false;
    private int latas = 0;
    private int latasTotales;

    void Start()
    {
        // Contamos cuántas latas hay en el nivel al empezar
        latasTotales = GameObject.FindGameObjectsWithTag("Item").Length;
        ActualizarTexto();

        Cursor.lockState = CursorLockMode.Locked; // Lo fija al centro
        Cursor.visible = false;
    }

    void Update()
    {
        if (juegoTerminado) return;

        // --- LÓGICA DEL RELOJ ---
        tiempoLimite -= Time.deltaTime;

        // Formato bonito 00:00
        float minutos = Mathf.FloorToInt(tiempoLimite / 60);
        float segundos = Mathf.FloorToInt(tiempoLimite % 60);

        if (textoTiempo != null)
        {
            if (tiempoLimite < 10) textoTiempo.color = Color.red; // Alerta roja
            else textoTiempo.color = Color.white;

            textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

        if (tiempoLimite <= 0)
        {
            tiempoLimite = 0;
            PerderJuego();
        }

        // --- MOVIMIENTO ---
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
            if (audioJugador != null) audioJugador.Play();

            // 1. Sumar tiempo
            tiempoLimite += segundosGanados;

            // 2. Crear el texto flotante visual (+5s)
            if (prefabTextoFlotante != null)
            {
                // Aparece un poco más arriba de la lata
                Vector3 posicionSpawn = other.transform.position + Vector3.up * 1.5f;

                // Creamos el objeto
                GameObject textoObj = Instantiate(prefabTextoFlotante, posicionSpawn, Quaternion.identity);

                // Le cambiamos el texto para que diga cuántos segundos ganaste
                TextoFlotante script = textoObj.GetComponent<TextoFlotante>();
                if (script != null)
                {
                    script.SetTexto("+" + segundosGanados + "s");
                }
            }

            // 3. Lógica de recolección
            latas++;
            Destroy(other.gameObject); // Destruir la lata
            ActualizarTexto();
            verificarVictoria();
        }
    }

    void ActualizarTexto()
    {
        if (textoPuntos != null)
            textoPuntos.text = "Latas: " + latas + " / " + latasTotales;
    }

    void verificarVictoria()
    {
        if (latas >= latasTotales)
        {
            juegoTerminado = true;
            if (panelVictoria != null) panelVictoria.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Lo soltamos
            Cursor.visible = true;
        }
    }

    void PerderJuego()
    {
        juegoTerminado = true;
        if (textoTiempo != null) textoTiempo.text = "00:00";
        if (panelDerrota != null) panelDerrota.SetActive(true);

        Cursor.lockState = CursorLockMode.None; // Lo soltamos
        Cursor.visible = true;
    }
}