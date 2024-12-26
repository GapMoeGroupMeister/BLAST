using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LineDrawer : Image
{
    [Tooltip("중간 지점들")]
    public RectTransform[] points;

    [Header("Texture")]
    [Tooltip("사용할 Sprite")]
    public Sprite targetSprite;
    [Tooltip("사용할 Material")]
    public Material targetMaterial;
    [Tooltip("틴트 색상")]
    public Color tintColor = Color.white;

    [Header("Mesh")]
    [Tooltip("두께")]
    [Min(0f)] public float thickness = 50f;
    [Tooltip("선의 면 개수")]
    [Min(1)] public int lineSegment = 5;
    [Tooltip("곡선 사용여부")]
    public bool useCurve = true;
    [Tooltip("곡선 면 개수 (곡선 사용 시 적용)")]
    [Min(1)] public int curveSegment = 10;

    [Header("UV")]
    [Tooltip("UV 반복하는 거리")]
    public float uvDistance = 50f;
    //[Tooltip("UV 흐르는 속도")]
    //public float uvFlowSpeed = 1f;

    [Header("Option")]
    [Tooltip("최소 소수점(선택 사항)")]
    [Min(0f)] public float epsilon = 0.001f;

    // Mesh
    private List<(Vector2 pos, Color32 color, Vector2 uv)> _vertices;
    private List<(int idx, int idx2, int idx3)> _triangles;

    // UV
    private float _uv;

    //__________________________________________________________________________ Draw
    protected override void OnPopulateMesh(VertexHelper vh) // UI 요소에 정점을 생성할 때 실행되는 함수
    {
        base.OnPopulateMesh(vh);
        if (this.points == null) return;

        // 정점을 재정의 하기 위해 기존 정점을 모두 제거한다.
        vh.Clear();

        // 정점과 삼각형 데이터를 담을 리스트들을 초기화한다.
        if (_vertices == null) _vertices = new List<(Vector2 pos, Color32 color, Vector2 uv)>();
        if (_triangles == null) _triangles = new List<(int idx, int idx2, int idx3)>();

        // 현재 Transform 기준으로 포인트들을 변환하고, 겹치는 포인트들을 제거한다.
        Vector2[] points = getLinePoints();

        // 선의 시작 지점과 끝 지점을 담는 튜플로써, 왼쪽 지점과 오른쪽 지점을 담는다.
        (Vector2 left, Vector2 right) start, end = default;

        // 포인트들을 반복하면서, 시작 지점과 끝 지점을 구해 라인을 쌓아준다.
        for (int i = 0, l = points.Length - 1; i < l; ++i)
        {
            start = GetLineSide(points[i], points[i + 1], 0f, thickness);
            // 이전의 끝 지점과 현재의 시작 지점으로 커브를 쌓아준다.
            if (useCurve && i > 0) stackMeshCurve(end, start);
            end = GetLineSide(points[i], points[i + 1], 1f, thickness);

            stackMeshLine(start, end);
        }

        // 현재까지 쌓은 데이터로 정점을 재구성한다.
        applyMesh(vh);
    }
    private Vector2[] getLinePoints() // 포인트를 정리하는 함수
    {
        // 포인트의 위치를 현재 Transform 기준으로 변환한다.
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < this.points.Length; ++i)
        {
            if (this.points[i]) points.Add(InverseRectTransformPoint(transform, this.points[i].position));
        }

        // 겹치는 포인트는 제거한다.
        for (int i = 0; i < points.Count - 1; ++i)
        {
            if (Approximately(points[i], points[i + 1], epsilon))
            {
                points.RemoveAt(i + 1);
                --i;
            }
        }

        return points.ToArray();
    }

    //__________________________________________________________________________ Mesh
    private void stackMeshLine((Vector2 left, Vector2 right) start, (Vector2 left, Vector2 right) end) // 라인을 쌓는 함수
    {
        // 선의 면 개수만큼 나누어, 시작 지점과 끝 지점을 쌓는다.
        for (int i = 0; i < lineSegment; ++i)
        {
            Vector2 startLeft = Vector2.Lerp(start.left, end.left, (float)i / lineSegment);
            Vector2 startRight = Vector2.Lerp(start.right, end.right, (float)i / lineSegment);
            Vector2 endLeft = Vector2.Lerp(start.left, end.left, ((float)i + 1) / lineSegment);
            Vector2 endRight = Vector2.Lerp(start.right, end.right, ((float)i + 1) / lineSegment);

            stackMeshSquare((startLeft, startRight), (endLeft, endRight));
        }
    }
    private void stackMeshCurve((Vector2 left, Vector2 right) start, (Vector2 left, Vector2 right) end) // 커브를 쌓는 함수
    {
        // 커브 지점의 두 방향벡터가 같으면 커브가 필요없으므로 종료한다.
        Vector2 startDir = (start.right - start.left).normalized;
        Vector2 endDir = (end.right - end.left).normalized;
        if (Approximately(startDir, endDir, epsilon)) return;

        // 두 방향벡터의 각도를 미리 계산한다.
        float signedAngle = Vector2.SignedAngle(startDir, endDir);
        float angleSegment = signedAngle / curveSegment;

        // 시작 지점의 오른쪽을 바라보는 각도를 기준 각도로 설정한다.
        Quaternion baseLookRot = Quaternion.LookRotation(startDir, -Vector3.forward);
        Vector2 center = (start.left + start.right) * 0.5f;

        // 곡선의 면 개수만큼 반복하면서, 시작 지점과 끝 지점을 쌓아준다.
        for (int i = 0; i < curveSegment; ++i)
        {
            // 두 방향벡터의 중점을 미리 담아둔다.
            Vector2 startLeft, startRight, endLeft, endRight;
            startLeft = startRight = endLeft = endRight = center;

            // 기준 각도의 정면이 오른쪽이므로, 정면은 오른쪽, 후면은 왼쪽이 된다.
            Quaternion startLookRot = baseLookRot * Quaternion.Euler(0f, -angleSegment * i, 0f);
            startLeft += (Vector2)(startLookRot * Vector3.back * thickness * 0.5f);
            startRight += (Vector2)(startLookRot * Vector3.forward * thickness * 0.5f);

            Quaternion endLookRot = baseLookRot * Quaternion.Euler(0f, -angleSegment * (i + 1), 0f);
            endLeft += (Vector2)(endLookRot * Vector3.back * thickness * 0.5f);
            endRight += (Vector2)(endLookRot * Vector3.forward * thickness * 0.5f);

            stackMeshSquare((startLeft, startRight), (endLeft, endRight));
        }
    }
    private void stackMeshSquare((Vector2 left, Vector2 right) start, (Vector2 left, Vector2 right) end) // 사각형을 쌓는 함수
    {
        // 거리에 따라 UV를 조절하여, 새로운 UV를 정의한다.
        float distance = (Vector2.Distance(start.left, end.left) + Vector2.Distance(start.right, end.right)) * 0.5f;
        float newUV = _uv + distance / uvDistance;
        Debug.Log(distance + " " + uvDistance);

        // 정점과 색상을 쌓는다. (UV Flow 적용 포함)
        _vertices.Add((start.left, color, new Vector2(0f, _uv)));
        _vertices.Add((end.left, color, new Vector2(0f, newUV)));
        _vertices.Add((end.right, color, new Vector2(1f, newUV)));
        _vertices.Add((start.right, color, new Vector2(1f, _uv)));

        // 삼각형을 쌓는다. (왼쪽 아래에서 시계방향으로)
        int count = _vertices.Count;
        _triangles.Add((count - 4, count - 3, count - 2));
        _triangles.Add((count - 2, count - 1, count - 4));

        // 새로운 UV를 저장한다.
        _uv = newUV;
    }

    private void applyMesh(VertexHelper vh) // 정점을 적용하는 함수
    {
        // 현재까지 쌓은 데이터를 바탕으로 정점을 적용한다.
        for (int i = 0, l = _vertices.Count; i < l; ++i)
            vh.AddVert(_vertices[i].pos, _vertices[i].color, _vertices[i].uv);
        for (int i = 0, l = _triangles.Count; i < l; ++i)
            vh.AddTriangle(_triangles[i].idx, _triangles[i].idx2, _triangles[i].idx3);

        // 데이터를 쌓은 변수들을 초기화한다.
        _vertices.Clear();
        _triangles.Clear();
        _uv = 0f;
    }

    //__________________________________________________________________________ Util
    public static bool Approximately(Vector2 a, Vector2 b, float epsilon) // 두 변수가 거의 일치하는지 판단하는 함수(Vector2)
    {
        return Approximately(a.x, b.x, epsilon) && Approximately(a.y, b.y, epsilon);
    }
    public static bool Approximately(float a, float b, float epsilon) // 두 변수가 거의 일치하는지 판단하는 함수(Float)
    {
        // Epsilon 이내의 차이이면 같은 것으로 판단한다.
        return Mathf.Abs(a - b) <= epsilon;
    }
    public static Vector2 InverseRectTransformPoint(Transform tr, Vector2 world) // Transform의 Local Point으로 변경하는 함수
    {
        // Canvas의 크기가 변경될 수 있으므로, LossyScale을 나눠준다.
        Vector2 scale = tr.lossyScale;
        world -= (Vector2)tr.position;
        world.x /= scale.x;
        world.y /= scale.y;
        return world;
    }
    public static (Vector2 left, Vector2 right) GetLineSide(Vector2 start, Vector2 end, float t, float thickness) // 라인의 양쪽 지점을 구하는 함수
    {
        // 방향벡터를 통해 각도를 구한다.
        Vector3 dir = end - start;
        Quaternion lookRot = Quaternion.LookRotation(dir, -Vector3.forward);

        // 중간 지점에서 각각 -90, 90도 회전하여 왼쪽 지점와 오른쪽 지점을 구한다.
        Vector2 center = Vector2.Lerp(start, end, t);
        Vector2 left = center + (Vector2)(lookRot * Quaternion.Euler(0f, -90f, 0f) * Vector3.forward * thickness * 0.5f);
        Vector2 right = center + (Vector2)(lookRot * Quaternion.Euler(0f, 90f, 0f) * Vector3.forward * thickness * 0.5f);

        return (left, right);
    }

    //__________________________________________________________________________ Update
    protected virtual void Update()
    {
        // OnPopulateMesh는 UI 요소에 변경될 때만(크기, 피봇, 앵커 등) 실행되므로,
        // 매 프레임 실행하기 위해 SetVerticesDirty 함수를 실행한다.
        SetVerticesDirty();
    }

    //__________________________________________________________________________ Editor
    protected override void OnValidate() // 인스펙터 창에서 프로퍼티를 변경할 때 실행되는 함수
    {
        base.OnValidate();

        // Sprite를 적용한다.
        if (targetSprite != sprite)
            sprite = targetSprite;

        if (material != targetMaterial)
            material = targetMaterial;

        // WrapMode를 Repeat로 한다.
        if (targetSprite && targetSprite.texture.wrapMode != TextureWrapMode.Repeat)
            targetSprite.texture.wrapMode = TextureWrapMode.Repeat;

        // Color를 적용한다.
        if (tintColor != color)
            color = tintColor;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(LineDrawer))]
public class LineDrawerInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI() // 인스펙터를 새로 정의하는 함수
    {
        // LineDrawer 클래스에서 사용하는 프로퍼티만을 표시한다.
        foreach (var field in typeof(LineDrawer).GetFields())
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty(field.Name), true);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif 