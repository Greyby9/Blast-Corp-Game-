using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class BlockZone : MonoBehaviour
{
    public static BlockZone instance;
    public CinemachineCamera virtualCamera;
    public Collider2D originalBoundingShape;
    public Collider2D bossBoundingShape;
    private CinemachineConfiner2D confiner;
    public BoxCollider2D ColliderRight;
    public BoxCollider2D ColliderLeft;
    private float time = 0;
    public float timeOfBlock;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        if (confiner != null && originalBoundingShape != null)
        {
  
            confiner.BoundingShape2D = originalBoundingShape;
            confiner.InvalidateBoundingShapeCache();
        }
        else
        {
            Debug.LogWarning("Error");
        }
    }
    void Update()
    {
        if(confiner.BoundingShape2D == bossBoundingShape)
        time += Time.deltaTime;
        Debug.Log(time);
        if (time > timeOfBlock)
        {
            SwitchToOriginalBounds();
        }
    }
    public void SwitchToBossBounds()
    {
        confiner.BoundingShape2D = bossBoundingShape;
        confiner.InvalidateBoundingShapeCache();
        ColliderLeft.enabled = true;
        ColliderRight.enabled = true;
    }
    public void SwitchToOriginalBounds()
    {
        confiner.BoundingShape2D = originalBoundingShape;
        confiner.InvalidateBoundingShapeCache();
        ColliderLeft.enabled = false;
        ColliderRight.enabled = false;
    }
}
