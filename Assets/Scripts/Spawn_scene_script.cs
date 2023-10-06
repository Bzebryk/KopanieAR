using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Spawn_scene_script : MonoBehaviour
{

    public GameObject scene_prefab;

    ARRaycastManager raycastManager;
    
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();   
    }

    void Update()
    {
        if (Input.touchCount > 0 && !scene_prefab.activeSelf)
        {

            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {

                Pose hitpose = hits[0].pose;

                scene_prefab.SetActive(true);
                scene_prefab.transform.position = hitpose.position;
                scene_prefab.transform.rotation = hitpose.rotation;
                ARPlaneManager plane_manager = GetComponent<ARPlaneManager>();
                
                foreach (var plane in plane_manager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                plane_manager.enabled = false;
            }

        }
    }
}
