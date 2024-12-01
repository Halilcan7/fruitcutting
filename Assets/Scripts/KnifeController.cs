using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public float zPosition = -7f; // Bıçağın Z ekseni pozisyonu sabit
    public float sensitivity = 0.1f; // Mouse hareket hassasiyeti
    private Camera mainCamera; // Ana kamera referansı

    void Start()
    {
        // Kamerayı al
        mainCamera = Camera.main;

        // Fare imlecini gizle
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse pozisyonunu ekrana göre al
        Vector3 mousePos = Input.mousePosition;

        // Mouse pozisyonunu dünya koordinatlarına çevir
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCamera.transform.position.z - zPosition)));

        // Z eksenini sabit tut
        worldPos.z = zPosition;

        // Bıçağın pozisyonunu mouse pozisyonuna göre güncelle
        transform.position = Vector3.Lerp(transform.position, worldPos, sensitivity);
    }
}
