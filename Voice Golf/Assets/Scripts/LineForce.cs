using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private ForceFromMicrophone micforce;

    [SerializeField]
    private float stopVelocity = .05f;

    private bool isIdle;
    private bool isAiming;

    private Rigidbody body;

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(body.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        ProcessAim();
    }

    private void ProcessAim()
    {
        if(!isAiming || !isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);
        
        if (Input.GetMouseButtonUp(0))
        {
            Shoot(worldPoint.Value);
        }
    }

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = { 
            transform.position,
            worldPoint};

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void Stop()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        isIdle = true;
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        //body.AddForce(direction * strength * shootforce);
        body.AddForce(direction * micforce.force);
        isIdle = false;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        
        if(Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}
