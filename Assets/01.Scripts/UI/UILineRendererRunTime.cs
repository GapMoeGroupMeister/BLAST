using UnityEngine;
using UnityEngine.UI;

namespace GGM.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRendererRunTime : Graphic
    {
        public Vector2[] points;
        public Vector2[] subPoints;

        public float thickness = 1f;
        public bool center = true;
        public Color lineColor;

        private Mesh _mesh;
        private CanvasRenderer _canvasRenderer;
        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;


        protected override void Awake()
        {
            _canvasRenderer = GetComponent<CanvasRenderer>();
        }

        private void LateUpdate()
        {
            CreateMeshByPoints();
        }

        public void SetFillAmount(float amount)
        {

        }

        public void CreateMeshByPoints()
        {
            _mesh = new Mesh();

            if (points == null || points.Length < 2) return;

            int cnt = (points.Length - 1) * 5;
            _triangles = new int[cnt];
            _vertices = new Vector3[cnt];

            for (int i = 0; i < points.Length - 1; i++)
            {
                CreateLineSegment(points[i], points[i + 1], i);
                int index = i * 5;

                _triangles[i * 3] = index;
                _triangles[i * 3 + 1] = index + 1;
                _triangles[i * 3 + 2] = index + 3;

                _triangles[(i + 1) * 3] = index + 3;
                _triangles[(i + 1) * 3 + 1] = index + 2;
                _triangles[(i + 1) * 3 + 2] = index;

                if (i != 0)
                {
                    _triangles[(i + 2) * 3] = index - 1;
                    _triangles[(i + 2) * 3 + 1] = index - 2;
                    _triangles[(i + 2) * 3 + 2] = index + 1;

                    _triangles[(i + 3) * 3] = index - 1;
                    _triangles[(i + 3) * 3 + 1] = index;
                    _triangles[(i + 3) * 3 + 2] = index - 3;
                }
            }

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            //_mesh.uv = _uv;

        }

        private void CreateLineSegment(Vector3 point1, Vector3 point2, int idx)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta * 0.5f) : Vector2.zero;
            Vector3 position;

            Quaternion point1Rot = Quaternion.Euler(0, 0, RotatePointToward(point1, point2) + 90f);
            position = point1Rot * new Vector3(-thickness * 0.5f, 0f);
            position += point1 - offset;
            _vertices[idx * 5] = position; 
            position = point1Rot * new Vector3(thickness * 0.5f, 0f);
            position += point1 - offset;
            _vertices[idx * 5 + 1] = position;

            Quaternion point2Rot = Quaternion.Euler(0, 0, RotatePointToward(point2, point1) - 90f);
            position = point2Rot * new Vector3(-thickness * 0.5f, 0f);
            position += point2 - offset;
            _vertices[idx * 5 + 2] = position;
            position = point2Rot * new Vector3(thickness * 0.5f, 0f);
            position += point2 - offset;
            _vertices[idx * 5 + 3] = position;

            position = point2 - offset;
            _vertices[idx * 5 + 4] = position;
        }

        private float RotatePointToward(Vector3 vert, Vector3 target)
            => Mathf.Atan2(target.y - vert.y, target.x - vert.x) * Mathf.Rad2Deg;

        public void SetPoints(Vector2[] points)
            => this.points = points;
    }
}


