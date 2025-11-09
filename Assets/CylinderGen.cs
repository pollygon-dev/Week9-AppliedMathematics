using UnityEngine;

public class CylinderGen : MonoBehaviour
{
    public Material material;
    public float radius = 2.5f;
    public float height = 5f;
    public Vector3 cylinderPos = Vector3.zero;
    public float zPos = 10;
    public int segments = 16;

    private void OnRenderObject()
    {
        DrawCylinder();
    }

    public void DrawCylinder()
    {
        if (material == null)
        {
            return;
        }

        if (segments < 6)
        {
            segments = 6;
        }

        GL.PushMatrix();
        material.SetPass(0);
        GL.Begin(GL.LINES);

        Vector3[] topCircle = new Vector3[segments];
        Vector3[] bottomCircle = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            topCircle[i] = new Vector3(x, height * 0.5f, z) + cylinderPos;
            bottomCircle[i] = new Vector3(x, -height * 0.5f, z) + cylinderPos;
        }
        for (int i = 0; i < segments; i++)
        {
            var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + topCircle[i].z);
            var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + topCircle[(i + 1) % segments].z);

            GL.Vertex(new Vector3(topCircle[i].x * perspective1, topCircle[i].y * perspective1, 0));
            GL.Vertex(new Vector3(topCircle[(i + 1) % segments].x * perspective2, topCircle[(i + 1) % segments].y * perspective2, 0));
        }
        for (int i = 0; i < segments; i++)
        {
            var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + bottomCircle[i].z);
            var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + bottomCircle[(i + 1) % segments].z);

            GL.Vertex(new Vector3(bottomCircle[i].x * perspective1, bottomCircle[i].y * perspective1, 0));
            GL.Vertex(new Vector3(bottomCircle[(i + 1) % segments].x * perspective2, bottomCircle[(i + 1) % segments].y * perspective2, 0));
        }
        for (int i = 0; i < segments; i++)
        {
            var topPerspective = PerpectiveCamera.Instance.GetPerspective(zPos + topCircle[i].z);
            var bottomPerspective = PerpectiveCamera.Instance.GetPerspective(zPos + bottomCircle[i].z);

            GL.Vertex(new Vector3(topCircle[i].x * topPerspective, topCircle[i].y * topPerspective, 0));
            GL.Vertex(new Vector3(bottomCircle[i].x * bottomPerspective, bottomCircle[i].y * bottomPerspective, 0));
        }

        GL.End();
        GL.PopMatrix();
    }
}