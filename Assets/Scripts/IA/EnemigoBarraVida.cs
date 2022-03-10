using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{
    [SerializeField] private Image barraVida;

    private float saludActual;
    private float saludMax;

    private void Update()
    {
        //Actualizamos la salud del enemigo referenciada referenciada 
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount,
            saludActual / saludMax, 10f * Time.deltaTime);
    }

    public void ModificarSalud(float pSaludActual, float pSaludMax)
    {
        //Obtenemos la referencia de la vida del enemigo
        saludActual = pSaludActual;
        saludMax = pSaludMax;
    }
}
