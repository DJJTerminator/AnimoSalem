using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothness = 0.6f;
	public float maxZoom = 10.0f;
	public float minZoom = 8.0f;
	bool canZoom = true;
    [SerializeField]
    AudioSource zoomIn;
    [SerializeField]
    AudioSource zoomOut;

    private Vector3 dir= Vector3.zero;

	void Update () {
		//getting the target position
		Vector3 targetPos = target.position;
		//getting the x and z pos of target and the y position of the camera itself
		targetPos.y = transform.position.y;
		//moving to the goal, the target position x and z
		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref dir, smoothness);

		if (Input.GetKeyDown (KeyCode.LeftControl) && canZoom == true && GetComponent<Camera>().orthographicSize >= maxZoom) 
		{
            //zoom in
            zoomIn.Stop();
            zoomOut.Play();
            canZoom = false;
            StopCoroutine("ZoomOut");
            StartCoroutine (ZoomIn(.01f));
        }
		else if (Input.GetKeyDown (KeyCode.LeftControl) && canZoom == false && GetComponent<Camera>().orthographicSize <= minZoom)
        {
            //zoom out
            zoomIn.Stop();
            zoomOut.Play();
            canZoom = true;
            StopCoroutine("ZoomIn");
            StartCoroutine(ZoomOut(.01f));
        }		
	}

   IEnumerator ZoomIn(float waitTime)
    {
        //zoom in
        while (GetComponent<Camera>().orthographicSize > minZoom)
        {
            GetComponent<Camera>().orthographicSize -= .2f;
            yield return new WaitForSeconds(waitTime);
        }
        if (GetComponent<Camera>().orthographicSize <= minZoom)
            GetComponent<Camera>().orthographicSize = minZoom;
    }
    IEnumerator ZoomOut(float waitTime)
    {
        //zoom in
        while (GetComponent<Camera>().orthographicSize < maxZoom)
        {
            GetComponent<Camera>().orthographicSize += .2f;
            yield return new WaitForSeconds(waitTime);
        }
        if (GetComponent<Camera>().orthographicSize <= maxZoom)
            GetComponent<Camera>().orthographicSize = maxZoom;
    }
}
