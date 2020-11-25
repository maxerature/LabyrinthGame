using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLightPaths : MonoBehaviour
{

    public float viewDistance;
    public float viewAngle;
    public bool posChanged;

    public GameObject obstacles;
    public Vector3[] corners = new Vector3[4];

    public List<Vector2> currentCasts;

    // Function to get vector angle from float angle
    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }


    // Start is called before the first frame update
    void Start()
    {
        posChanged = true;
    }


    // Update is called once per frame
    void Update()
    {
        currentCasts.Clear();

        posChanged = false;
        Collider2D[] litColliders = Physics2D.OverlapCircleAll(transform.position, viewDistance);
        RaycastHit2D hit;
        foreach (Collider2D litCollider in litColliders)
        {
            //Vector3[] corners = new Vector3[4];

            corners[0] = litCollider.transform.gameObject.GetComponent<LightingObstacle>().topLeftCorner;
            corners[1] = litCollider.transform.gameObject.GetComponent<LightingObstacle>().topRightCorner;
            corners[2] = litCollider.transform.gameObject.GetComponent<LightingObstacle>().bottomLeftCorner;
            corners[3] = litCollider.transform.gameObject.GetComponent<LightingObstacle>().bottomRightCorner;

            // Check if the corners of the box are in the vision radius


            for(int i=0; i<4; i++)
            {
                if (Vector3.Distance(corners[i], transform.position) <= viewDistance)
                {

                    //float angle = Vector2.Angle(transform.position, corners[i]);

                    //Angle of corner
                    //hit = Physics2D.Raycast(transform.position, GetDirectionVector2D(angle), viewDistance);
                    hit = Physics2D.Raycast(transform.position, (corners[i] - transform.position).normalized, viewDistance);
                    currentCasts.Add(hit.point);
                    Debug.Log("Hit at " + hit.point);
                    Debug.Log("hit the " + hit.collider);
                    Debug.DrawRay(transform.position, hit.point, Color.blue, 20f, false);

                    ////Get light to look past corner
                    ////hit = Physics2D.Raycast(transform.position, GetDirectionVector2D(angle-0.0001f), viewDistance);
                    //hit = Physics2D.Raycast(transform.position, (transform.position - corners[i]).normalized, viewDistance);
                    //currentCasts.Add(hit.point);
                    //Debug.Log("Hit at " + hit.point);
                    //Debug.DrawLine(transform.position, hit.point, Color.white, 20f, false);
                    ////hit = Physics2D.Raycast(transform.position, GetDirectionVector2D(angle+0.0001f), viewDistance);
                    //currentCasts.Add(hit.point);
                    //Debug.Log("Hit at " + hit.point); 
                    //Debug.DrawLine(transform.position, hit.point, Color.white, 20f, false);



                    //Debug.Log("Hit at " + corners[i]);
                }
            }

        }

        ////Generate non-corner points (to create mesh)
        //for(float i=-viewAngle/2; i<viewAngle/2; i+=viewAngle/20f)
        //{
        //    hit = Physics2D.Raycast(transform.position, GetDirectionVector2D(i), viewDistance);
        //    currentCasts.Add(hit.point);
        //    Debug.Log("Hit at " + hit.point);
        //    Debug.DrawLine(transform.position, hit.point, Color.white, 20f, false);
        //}
        ////Make sure final angle is hit
        //hit = Physics2D.Raycast(transform.position, GetDirectionVector2D(viewAngle/2), viewDistance);
        //currentCasts.Add(hit.point);
        //Debug.Log("Hit at " + hit.point);
        //Debug.DrawLine(transform.position, hit.point, Color.white, 20f, false);
    }
}
