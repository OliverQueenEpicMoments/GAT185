using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour {
    [SerializeField] Transform Target;
    [SerializeField, Range(2, 20)] private float Distance;
    [SerializeField, Range(20, 80)] private float Pitch;
    [SerializeField, Range(0.1f, 5)] private float Sensitivity;

    private float Yaw = 0;

    void LateUpdate() {
        if (Target == null) return;

        Yaw += Input.GetAxis("Mouse X") * Sensitivity;

        Quaternion QYaw = Quaternion.AngleAxis(Yaw, Vector3.up);
        Quaternion QPitch = Quaternion.AngleAxis(Pitch, Vector3.right);
        Quaternion Rotation = QYaw * QPitch;

        Vector3 Offset = Rotation * Vector3.back * Distance;

        transform.position = Target.position + Offset;
        transform.rotation = Quaternion.LookRotation(-Offset);
    }

    public void SetTarget(Transform Target) { 
        this.Target = Target;
        Yaw = Target.rotation.eulerAngles.y;
    }
}
