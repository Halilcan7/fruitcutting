using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float zPosition = -7f;
    public float sensitivity = 0.1f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCamera.transform.position.z - zPosition)));
        worldPos.z = zPosition;
        transform.position = Vector3.Lerp(transform.position, worldPos, sensitivity);
    }
}

