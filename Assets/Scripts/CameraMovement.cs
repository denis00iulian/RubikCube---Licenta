using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform target;
    

    private Vector3 previousPosition;

    private void Update()
    {
            var distanceToTarget = Vector3.Distance(_camera.transform.position, target.position);
            if (Input.GetMouseButtonDown(1))
            {
                previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 newPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;


                float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
                float rotationAroundXAxis = direction.y * 180; // camera moves vertically

                _camera.transform.position = target.position;

                _camera.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
                _camera.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

                _camera.transform.Translate(new Vector3(0, 0, -distanceToTarget));

                previousPosition = newPosition;
            }
    }
}
