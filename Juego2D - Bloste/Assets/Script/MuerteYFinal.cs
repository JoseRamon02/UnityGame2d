using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJugador : MonoBehaviour
{
    public Transform puntoFinal;
    public float limiteY = -6.25f; // Posición Y a partir de la cual se reinicia el juego

    void Update()
    {
        if (Vector3.Distance(transform.position, puntoFinal.position) < 0.1f)
        {
            PausarJuego();
        }


        // Comprueba si el jugador ha alcanzado el límite en el eje Y
        if (transform.position.y <= limiteY)
        {
            ReiniciarJuego();
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f; // Pausa el juego
    }

    void ReiniciarJuego()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene("SampleScene");
    }
}


