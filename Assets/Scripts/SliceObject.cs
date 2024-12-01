using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material SlicedMat;
    public float sidePushForce = 2f; // Sağ tarafa doğru kuvvet
    public float rotationForce = 5f; // Dönme kuvveti
    public bool gravity = true; // Yerçekimi aktif mi?

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CanSlice"))
        {
            // Hedef nesneyi dilimle
            SlicedHull sliceobj = Slice(other.gameObject, SlicedMat);
            if (sliceobj != null)
            {
                // Üst ve alt dilimleri oluştur
                GameObject SlicedObjTop = sliceobj.CreateUpperHull(other.gameObject, SlicedMat);
                GameObject SlicedObjDown = sliceobj.CreateLowerHull(other.gameObject, SlicedMat);

                // Orijinal nesneyi yok et
                Destroy(other.gameObject);

                // Üst dilime sağa doğru yatma kuvveti uygula
                ApplyForceToSlice(SlicedObjTop, Vector3.right);

                // Alt dilime sadece fizik bileşenleri ekle, kuvvet uygulama
                AddPhysics(SlicedObjDown);
            }
        }
    }

    private void ApplyForceToSlice(GameObject obj, Vector3 forceDirection)
    {
        if (obj == null) return;

        // Fizik ve çarpışma bileşenlerini ekle
        var collider = obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;

        // Sağ tarafa doğru hafif bir kuvvet uygula
        rigidbody.AddForce(forceDirection * sidePushForce, ForceMode.Impulse);

        // Dönme kuvveti uygula
        rigidbody.AddTorque(Vector3.forward * rotationForce, ForceMode.Impulse);

        // Nesneyi yeniden dilimlenebilir yap
        obj.tag = "CanSlice";
    }

    private void AddPhysics(GameObject obj)
    {
        if (obj == null) return;

        // Fizik ve çarpışma bileşenlerini ekle
        obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;

        // Nesneyi yeniden dilimlenebilir yap
        obj.tag = "CanSlice";
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        // EzySlice ile dilimleme işlemini gerçekleştir
        // Bıçağın yönünü doğru ayarlamak için X eksenine doğru dilimleme yapıyoruz
        Vector3 sliceDirection = transform.forward; // Yatayda kesmek için right (X ekseni)
        return obj.Slice(transform.position, sliceDirection, mat);
    }
}
