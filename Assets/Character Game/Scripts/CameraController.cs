using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform FollowTargetTransform;
    [SerializeField] private float LookTurnRate;
    [SerializeField] private float YawLimit;
    [SerializeField] private float PitchLimit;

    [SerializeField] private InputRouter inputrouter;

    Vector2 InputAxis;

    public Transform FollowTransform { get; set; }

    void Start() {
        inputrouter.LookEvent += OnLook;
    }

    void Update() {
        // Rotate follow target transform
        Quaternion QYaw = Quaternion.AngleAxis(InputAxis.x * LookTurnRate, Vector3.up);
        Quaternion QPitch = Quaternion.AngleAxis(-InputAxis.y * LookTurnRate, Vector3.right);

        FollowTargetTransform.rotation *= (QYaw * QPitch);

        // Clamp rotation (Get euler angles, ignore roll rotation(Z))
        var Rotation = FollowTargetTransform.localEulerAngles;
        Rotation.z = 0;

        // Clamp pitch (Pitch is rotation around the X axis)
        float Pitch = Rotation.x;
        if (PitchLimit != 0) {
            if (Pitch > 180 && Pitch < (360 - PitchLimit)) Pitch = (360 - PitchLimit);
            else if (Pitch < 180 && Pitch > PitchLimit) Pitch = PitchLimit;
        }

        // Clamp yaw (Yaw is rotation around the Y angle) 
        float Yaw = Rotation.y;
        if (YawLimit != 0) {
            if (Yaw > 180 && Yaw < (360 - YawLimit)) Yaw = 360 - YawLimit;
            else if (Yaw < 180 && Yaw > YawLimit) Yaw = YawLimit;
        }

        Rotation.x = Pitch;
        Rotation.y = Yaw;

        FollowTargetTransform.transform.localEulerAngles = Rotation;
    }

    public void OnLook(Vector2 Axis) {
        InputAxis = Axis;
    }
}