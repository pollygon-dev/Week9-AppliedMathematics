using UnityEngine;

public class RectangularColumnGen : MonoBehaviour
{
    public Material material;
    public float width = 3f;
    public float height = 8f;
    public float depth = 3f;
    public Vector3 columnPos = Vector3.zero;
    public float zPos = 10;

    private void OnRenderObject()
    {
        DrawRectangularColumn();
    }

    public void DrawRectangularColumn()
    {
        if (material == null)
        {
            return;
        }

        GL.PushMatrix();
        material.SetPass(0);
        GL.Begin(GL.LINES);

        Vector3[] topVerts = new Vector3[4];
        topVerts[0] = new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f);
        topVerts[1] = new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f);
        topVerts[2] = new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f);
        topVerts[3] = new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f);

        Vector3[] bottomVerts = new Vector3[4];
        bottomVerts[0] = new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f);
        bottomVerts[1] = new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f);
        bottomVerts[2] = new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f);
        bottomVerts[3] = new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f);

        for (int i = 0; i < 4; i++)
        {
            topVerts[i] += columnPos;
            bottomVerts[i] += columnPos;
        }
        for (int i = 0; i < 4; i++)
        {
            var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + topVerts[i].z);
            var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + topVerts[(i + 1) % 4].z);

            GL.Vertex(new Vector3(topVerts[i].x * perspective1, topVerts[i].y * perspective1, 0));
            GL.Vertex(new Vector3(topVerts[(i + 1) % 4].x * perspective2, topVerts[(i + 1) % 4].y * perspective2, 0));
        }
        for (int i = 0; i < 4; i++)
        {
            var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + bottomVerts[i].z);
            var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + bottomVerts[(i + 1) % 4].z);

            GL.Vertex(new Vector3(bottomVerts[i].x * perspective1, bottomVerts[i].y * perspective1, 0));
            GL.Vertex(new Vector3(bottomVerts[(i + 1) % 4].x * perspective2, bottomVerts[(i + 1) % 4].y * perspective2, 0));
        }
        for (int i = 0; i < 4; i++)
        {
            var topPerspective = PerpectiveCamera.Instance.GetPerspective(zPos + topVerts[i].z);
            var bottomPerspective = PerpectiveCamera.Instance.GetPerspective(zPos + bottomVerts[i].z);

            GL.Vertex(new Vector3(topVerts[i].x * topPerspective, topVerts[i].y * topPerspective, 0));
            GL.Vertex(new Vector3(bottomVerts[i].x * bottomPerspective, bottomVerts[i].y * bottomPerspective, 0));
        }

        GL.End();
        GL.PopMatrix();
    }
}