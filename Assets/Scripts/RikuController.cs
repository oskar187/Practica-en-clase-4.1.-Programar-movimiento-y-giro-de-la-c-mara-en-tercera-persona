using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RikuController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private Animator animator;
    //public float fuerzaDeSalto = 8f;
    //Vectores de direccion camara
    public Vector3 forward;
    public Vector3 up;
    public Vector3 player;
    public Vector3 resultado;

    public float AlturaCam = 1.0f;  //Altura
    public float DistanciaCam = 1.0f;  //Distancia

    //Giro Camara
    public float AlturaGiro = 1.0f;

    //Posicion de la camara
    public Transform posCamara;

    //Rayo
    public float LongRay = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //Camara
        player = transform.position;
        up = transform.up;
        forward = transform.forward;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        
        float desplazamiento = translation * speed * Time.deltaTime;
        float rotacion = rotation * rotationSpeed * Time.deltaTime;

        // Detener al Personaje
        if(Input.GetKeyUp(KeyCode.UpArrow) ||
        Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("Correr", false); 
        }
        // Dejar de Saltar
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Saltar", false); 
        }
        // Dejar de Caminar hacia Atras
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("CaminarAtras", false); 
        }


        // Correr hacia delante y hacia atrás
        if(Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Correr", true);
            transform.Translate(0, 0, desplazamiento);
        }

        // Girar hacia izquierda o la derecha
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Correr", true);
            transform.Rotate(0, rotacion, 0);
        }

        // Saltar
        if(Input.GetKey(KeyCode.Space))
         {
            animator.SetBool("Saltar", true);            
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("CaminarAtras", true); 
        }

        //Camara
        player = transform.position;
        up = transform.up;
        forward = transform.forward;
        
        resultado = player + up * AlturaCam - forward * DistanciaCam;
        posCamara.position = resultado;

        //Rotacion Camara 
        Vector3 CamaraForwardNew = player + up * AlturaGiro - posCamara.position;
        posCamara.rotation = Quaternion.LookRotation(CamaraForwardNew);

        //Dibujar Rayo
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction*LongRay, Color.red);
        
    }

    
}
