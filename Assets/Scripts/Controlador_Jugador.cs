using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Controlador_Jugador : MonoBehaviour
{
    private Rigidbody rb; 

    //Movimiento
    private float movimientoX;
    private float movimientoY;
    private bool tocaSuelo = true;
    public float velocidad = 0;
    public float fuerzaSalto = 0;

    void Start()
    {
        rb = GetComponent <Rigidbody>(); 
    }

    void Update(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movimientoX = movementVector.x; 
        movimientoY = movementVector.y; 
    }

    private void FixedUpdate() 
    {
        
    }

    void OnMove ()
    {
        Vector3 movement = new Vector3 (movimientoX, 0.0f, movimientoY);
        rb.AddForce(rb.position + movement.normalized * velocidad * Time.fixedDeltaTime);
    }

    void OnJump()
    {
        if (tocaSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            tocaSuelo = false;
        }
    }

}
