using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ControladorMenu : MonoBehaviour
{
    // Función para cargar el juego (Nivel 1)
    public void Jugar()
    {
        SceneManager.LoadScene("Park"); // Asegúrate que tu escena de juego se llame así
    }

    // Función para reiniciar el nivel actual
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Función para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}