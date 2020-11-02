using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float viewAngle;
    public int rayCount;
    public float viewDistance;

    [SerializeField] public LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    public float startingAngle;

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    public static float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVector(aimDirection) + viewAngle / 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    void LateUpdate() { 
        float angle = startingAngle;
        float angleIncrease = viewAngle / rayCount;

        Vector3[] vertices = new Vector3[rayCount +1 +1];   //raycount + origin + 0 ray
        Vector2[] uv = new Vector2[vertices.Length];
        int[] tris = new int[rayCount *3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i=0; i<= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if(hit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                tris[triangleIndex + 0] = 0;
                tris[triangleIndex + 1] = vertexIndex - 1;
                tris[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            angle -= angleIncrease;

            vertexIndex++;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = tris;
    }
}
