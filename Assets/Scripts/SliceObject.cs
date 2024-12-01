using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material carrotMat;
    public Material cabbageMat;
    public float sidePushForce = 2f;
    public float rotationForce = 5f;
    public bool gravity = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CanSlice"))
        {
            SlicedHull sliceobj = Slice(other.gameObject, carrotMat);
            if (sliceobj != null)
            {
                GameObject SlicedObjTop = sliceobj.CreateUpperHull(other.gameObject, carrotMat);
                GameObject SlicedObjDown = sliceobj.CreateLowerHull(other.gameObject, carrotMat);

                Destroy(other.gameObject);

                ApplyForceToSlice(SlicedObjTop, Vector3.right);
                ApplyForceToSlice(SlicedObjDown, Vector3.left);
            }
        }
        if (other.gameObject.CompareTag("CanSliceCabbage"))
        {
            SlicedHull sliceobj = Slice(other.gameObject, cabbageMat);
            if (sliceobj != null)
            {
                GameObject SlicedObjTop = sliceobj.CreateUpperHull(other.gameObject, cabbageMat);
                GameObject SlicedObjDown = sliceobj.CreateLowerHull(other.gameObject, cabbageMat);

                Destroy(other.gameObject);

                ApplyForceToSliceC(SlicedObjTop, Vector3.right);
                ApplyForceToSliceC(SlicedObjDown, Vector3.left);
            }
        }
    }

    private void ApplyForceToSlice(GameObject obj, Vector3 forceDirection)
    {
        if (obj == null) return;

        var collider = obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;

        rigidbody.AddForce(forceDirection * sidePushForce, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.forward * rotationForce, ForceMode.Impulse);

        obj.tag = "CanSlice";
    }
    private void ApplyForceToSliceC(GameObject obj, Vector3 forceDirection)
    {
        if (obj == null) return;

        var collider = obj.AddComponent<BoxCollider>();
        var rigidbody = obj.AddComponent<Rigidbody>();
        rigidbody.useGravity = gravity;

        rigidbody.AddForce(forceDirection * sidePushForce, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.forward * rotationForce, ForceMode.Impulse);

        obj.tag = "CanSliceCabbage";
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        Vector3 sliceDirection = transform.forward;
        return obj.Slice(transform.position, sliceDirection, mat);
    }
}
