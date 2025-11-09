using UnityEngine;

public class SphereGen : MonoBehaviour
{
    public Material material;
    public float radius = 3f;
    public Vector3 spherePos = Vector3.zero;
    public float zPos = 10;
    public int horizontalSegments = 12; 
    public int verticalSegments = 8;  

    private void OnRenderObject()
    {
        DrawSphere();
    }

    public void DrawSphere()
    {
        if (material == null)
        {
            return;
        }

        if (horizontalSegments < 6)
        {
            horizontalSegments = 6;
        }

        if (verticalSegments < 6)
        {
            verticalSegments = 6;
        }

        GL.PushMatrix();
        material.SetPass(0);
        GL.Begin(GL.LINES);

        for (int lat = 0; lat <= verticalSegments; lat++)
        {
            float phi = Mathf.PI * lat / verticalSegments;
            float y = Mathf.Cos(phi) * radius;
            float ringRadius = Mathf.Sin(phi) * radius;

            for (int lon = 0; lon < horizontalSegments; lon++)
            {
                float theta1 = 2 * Mathf.PI * lon / horizontalSegments;
                float theta2 = 2 * Mathf.PI * (lon + 1) / horizontalSegments;

                Vector3 p1 = new Vector3(
                    ringRadius * Mathf.Cos(theta1),
                    y,
                    ringRadius * Mathf.Sin(theta1)
                ) + spherePos;

                Vector3 p2 = new Vector3(
                    ringRadius * Mathf.Cos(theta2),
                    y,
                    ringRadius * Mathf.Sin(theta2)
                ) + spherePos;

                var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + p1.z);
                var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + p2.z);

                GL.Vertex(new Vector3(p1.x * perspective1, p1.y * perspective1, 0));
                GL.Vertex(new Vector3(p2.x * perspective2, p2.y * perspective2, 0));
            }
        }

        for (int lon = 0; lon < horizontalSegments; lon++)
        {
            float theta = 2 * Mathf.PI * lon / horizontalSegments;

            for (int lat = 0; lat < verticalSegments; lat++)
            {
                float phi1 = Mathf.PI * lat / verticalSegments;
                float phi2 = Mathf.PI * (lat + 1) / verticalSegments;

                Vector3 p1 = new Vector3(
                    radius * Mathf.Sin(phi1) * Mathf.Cos(theta),
                    radius * Mathf.Cos(phi1),
                    radius * Mathf.Sin(phi1) * Mathf.Sin(theta)
                ) + spherePos;

                Vector3 p2 = new Vector3(
                    radius * Mathf.Sin(phi2) * Mathf.Cos(theta),
                    radius * Mathf.Cos(phi2),
                    radius * Mathf.Sin(phi2) * Mathf.Sin(theta)
                ) + spherePos;

                var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + p1.z);
                var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + p2.z);

                GL.Vertex(new Vector3(p1.x * perspective1, p1.y * perspective1, 0));
                GL.Vertex(new Vector3(p2.x * perspective2, p2.y * perspective2, 0));
            }
        }

        GL.End();
        GL.PopMatrix();
    }
}