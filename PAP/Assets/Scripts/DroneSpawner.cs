using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform player;
    public float spawnInterval = 10f;
    public float spawnYOffset = 3f;
    public float spawnXOffset = 10f;

    private float timer;
    private GameObject currentDrone;

    private string entrada = "left";
    private string salida = "right";

    public AudioClip soundFly;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        player = Player.instance.transform;
        if (currentDrone != null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnDrone();
            timer = spawnInterval;
        }
    }

    void SpawnDrone()
    {
        // Determinar posición de spawn usando la variable "entrada"
        float xOffset = (entrada == "left") ? -spawnXOffset : spawnXOffset;
        Vector2 spawnPos = new Vector2(player.position.x + xOffset, player.position.y + spawnYOffset);

        currentDrone = Instantiate(dronePrefab, spawnPos, Quaternion.identity);
        soundController.Instace.exeSound(soundFly);

        Drone droneScript = currentDrone.GetComponent<Drone>();
        if (droneScript != null)
        {
            droneScript.player = player;
            droneScript.SetExitSide(salida); // Le decimos por qué lado debe salir

            // Cuando el dron termine y salga, lo notificamos
            droneScript.onDroneExit = (string sideUsed) =>
            {
                // Intercambiar entrada y salida
                entrada = salida;
                salida = (salida == "right") ? "left" : "right";

                currentDrone = null;
            };
        }
    }
}
