using UnityEngine;

public class characterController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;

    private Rigidbody2D rigidbody;
    private bool enElSuelo;
    private bool mirandoDerecha = true;

    void Awake()
    {
        // Obtener referencia al Rigidbody2D
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    void ProcesarMovimiento()
    {
        // Movimiento horizontal
        float inputMovimiento = Input.GetAxis("Horizontal");
        rigidbody.linearVelocity = new Vector2(inputMovimiento * velocidad, rigidbody.linearVelocity.y);

        // invocamos la orientacion
        GestionarOrientacion(inputMovimiento);
    }

    void ProcesarSalto()
    {
        // Detectar salto con la tecla "Espacio"
        if (Input.GetKeyDown(KeyCode.Space) && enElSuelo)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, fuerzaSalto);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el personaje est� tocando el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Detectar cuando el personaje deja de tocar el suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = false;
        }
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // Si el personaje mira a la derecha y el jugador quiere moverse a la izquierda o viceversa
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            // Ejecutar el código para voltear al personaje
            mirandoDerecha = !mirandoDerecha;

            // Cambiar la escala local para voltear el sprite
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
