using System.Collections;
using UnityEngine;

public class QuadMeshDrawer : MonoBehaviour
{
    private float[] _prevFillAmount;
    private MeshFilter _meshFilter;

    private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _backgroundMeshFilter;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        DrawBackGround();
    }

    private void DrawBackGround()
    {
        Mesh mesh = new Mesh(); 

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1f, 0f),
            new Vector3(0f, 1f),
            new Vector3(1f, 0f),
            new Vector3(0f, -1f)
        };
        Vector2[] uv = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        _backgroundMeshFilter.mesh = mesh;
    }

    public void ShowQuadGraph(float[] values, float duration)
    {
        if (values.Length < 4)
        {
            Debug.LogError("Can't Draw Quad. Value is not Enough");
            return;
        }
        StartCoroutine(ShowQuadGraphCoroutine(values, duration));
    }

    private IEnumerator ShowQuadGraphCoroutine(float[] values, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float ratio = currentTime / duration;
            DrawQuadGraph(new float[4]{
                Mathf.Lerp(0,values[0], ratio),
                Mathf.Lerp(0,values[1], ratio),
                Mathf.Lerp(0,values[2], ratio),
                Mathf.Lerp(0,values[3], ratio)
            });
            
            yield return null;
        }
    }

    /**
     * <param name="values">
     * 0f ~ 1f 사이의 float로 이루어진 길이 4 배열을 넘기면 됨
     * </param>
     * <summary>
     * 마름모 그래프 그려줌, 점 순서는 서, 북, 동, 남 순서임
     * </summary>
     */
    public void DrawQuadGraph(float[] values)
    {
        if (values.Length < 4)
        {
            Debug.LogWarning("[Can't Draw Quad] 사각형을 그릴 값이 부족합니다");
            return;
        }

        Mesh mesh = new Mesh(); 

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-values[0], 0f),
            new Vector3(0f, values[1]),
            new Vector3(values[2], 0f),
            new Vector3(0f, -values[3])
        };
        Vector2[] uv = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        _meshFilter.mesh = mesh;
    }
    
    
    
    

}
