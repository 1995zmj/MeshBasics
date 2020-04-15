using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Cube : MonoBehaviour {

    // 代表边长
    public int xSize, ySize, zSize;

    private Mesh mesh;
    private Vector3[] vertices;
    private static int
        SetQuad (int[] triangles, int i, int v00, int v10, int v01, int v11) {
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }
    private void Awake () {
        Generate();
    }

    private void  Generate () {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Cube";
        CreateVertices();
        CreateTriangles();
        
    }
    
    private void CreateVertices () {
        // WaitForSeconds wait = new WaitForSeconds(0.05f);
        int cornerVertices = 8;
        int edgeVertices = (xSize + ySize + zSize - 3) * 4;
        int faceVertices = (
            (xSize - 1) * (ySize - 1) +
            (xSize - 1) * (zSize - 1) +
            (ySize - 1) * (zSize - 1)) * 2;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];

        int v = 0;
        for (int y = 0; y <= ySize; y++) {
            for (int x = 0; x <= xSize; x++) {
                vertices[v++] = new Vector3(x, y, 0);
                // yield return wait;
            }
            for (int z = 1; z <= zSize; z++) {
                vertices[v++] = new Vector3(xSize, y, z);
                // yield return wait;
            }
            for (int x = xSize - 1; x >= 0; x--) {
                vertices[v++] = new Vector3(x, y, zSize);
                // yield return wait;
            }
            for (int z = zSize - 1; z > 0; z--) {
                vertices[v++] = new Vector3(0, y, z);
                // yield return wait;
            }
        }
        
        for (int z = 1; z < zSize; z++) {
            for (int x = 1; x < xSize; x++) {
                vertices[v++] = new Vector3(x, ySize, z);
                // yield return wait;
            }
        }
        for (int z = 1; z < zSize; z++) {
            for (int x = 1; x < xSize; x++) {
                vertices[v++] = new Vector3(x, 0, z);
                // yield return wait;
            }
        }
        mesh.vertices = vertices;
    }

    
    private void CreateTriangles () {
    }

    private void OnDrawGizmos () {
        if (vertices == null) {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}