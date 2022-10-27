using UnityEngine;

public class FlashLightCameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _targetCamera;

    private Vector3 _vectorOffset;

    [SerializeField] private float _followingSpeed = 5f;

    private void Start() => _vectorOffset = transform.position - _targetCamera.transform.position;

    private void Update() => RotateFlashLightToCamera();

    private void RotateFlashLightToCamera(){
        transform.position = _targetCamera.transform.position + _vectorOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetCamera.transform.rotation, _followingSpeed * Time.deltaTime);
    }
}
