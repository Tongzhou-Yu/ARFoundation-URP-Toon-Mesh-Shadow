using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class PlaceOnMesh : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private ARMeshManager arMeshManager;
    [SerializeField]
    private LayerMask layersToInclude;

    [SerializeField]
    [Tooltip("Instantiates this prefab on the mesh at the touch location.")]
    GameObject m_Object;

    public GameObject spawnedObject { get; private set; }

    void Update()
    {
        var ray = arCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//…‰œﬂ
        var hasHit = Physics.Raycast(ray, out var hit, float.PositiveInfinity, layersToInclude);
        Quaternion newObjectRoation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            var touchPhase = touch.phase;

            if (touchPhase == TouchPhase.Began)
            {
                spawnedObject = Instantiate(m_Object, hit.point, newObjectRoation);
            }
        }
    }
}