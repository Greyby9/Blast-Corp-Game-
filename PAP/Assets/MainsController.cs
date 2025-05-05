using UnityEngine;

public class MainsController : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuOpciones;

    public void AbrirOpciones()
    {
        menuPrincipal.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void VolverAlMenuPrincipal()
    {
        menuOpciones.SetActive(false);
        menuPrincipal.SetActive(true);
    }
}
