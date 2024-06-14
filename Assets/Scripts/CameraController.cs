using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraSpec {
    public Cinemachine.CinemachineVirtualCamera camera;
    public Vector3 minDistance;
    public Vector3 maxDistance;

    public bool CanBeUsed(Transform target)
    {
        Vector3 distance = camera.transform.position - target.position;
        distance.x = Mathf.Abs(distance.x);
        distance.y = Mathf.Abs(distance.y);
        distance.z = Mathf.Abs(distance.z);
        if (minDistance.x > 0 && distance.x < minDistance.x) return false;
        if (minDistance.y > 0 && distance.y < minDistance.y) return false;
        if (minDistance.z > 0 && distance.z < minDistance.z) return false;
        if (maxDistance.x > 0 && distance.x > maxDistance.x) return false;
        if (maxDistance.y > 0 && distance.y > maxDistance.y) return false;
        if (maxDistance.z > 0 && distance.z > maxDistance.z) return false;
        return true;
    }
}

public enum CameraMode
{
    Idle,
    HasDestination,
}

public class CameraController : MonoBehaviour
{
    public CameraMode mode = CameraMode.Idle;
    public Transform target;
    public Cinemachine.CinemachineVirtualCamera currentCamera;
    public List<CameraSpec> idleCameras;
    public float minShotDuration = 5.0f;
    public float maxShotDuration = 10.0f;

    private float nextShotTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var spec in idleCameras)
        {
            spec.camera.gameObject.SetActive(false);
        }
        NextShot();
    }

    public void NextShot()
    {
        nextShotTime = Time.time + Random.Range(minShotDuration, maxShotDuration);
        List<CameraSpec> prospects = new List<CameraSpec>();
        foreach (CameraSpec spec in idleCameras)
        {
            if (spec.CanBeUsed(target))
            {
                prospects.Add(spec);
                Debug.Log("prospect: " + spec.camera.name);
            }
        }
        if (prospects.Count == 0)
        {
            return;
        }
        Debug.Log("prospects: " + prospects.Count);
        if (currentCamera != null) {
            currentCamera.gameObject.SetActive(false);
        }
        currentCamera = prospects[Random.Range(0, prospects.Count)].camera;
        currentCamera.gameObject.SetActive(true);
    }

    public bool CanSeeTarget()
    {
        if (currentCamera == null)
        {
            return false;
        }
        var screenPoint = Camera.main.WorldToViewportPoint(target.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShotTime)
        {
            NextShot();
        }
    }
}
