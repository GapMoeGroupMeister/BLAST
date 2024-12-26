using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GGM.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRenderer : MaskableGraphic
    {
        public Vector2[] points;

        public float thickness = 1f;
        public bool center = true;
        public Color lineColor;

        private VertexHelper _vh;
        private Vector2 _zeroPos;
        private Vector2 _center;
        private Vector2 _size;
        private float _totalDistance;
        private float _distance = 0;

        private List<(Vector2 vert, float dist)> _vertices;

        protected override void OnEnable()
        {
            if (material != null)
                material = Instantiate(material);
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            GetTotalDistance();
            _vertices = new List<(Vector2 vert, float dist)>();

            _distance = 0;
            if (points == null || points.Length < 2) return;

            for (int i = 0; i < points.Length - 1; i++)
            {
                CreateLineSegments(points[i], points[i + 1], vh);

                int index = i * 5;

                vh.AddTriangle(index, index + 1, index + 3);
                vh.AddTriangle(index + 3, index + 2, index);

                if (i != 0)
                {
                    vh.AddTriangle(index - 1, index - 2, index + 1);
                    vh.AddTriangle(index - 1, index, index - 3);
                }
            }

            SetVerticesWithUV(vh);
        }

        private void SetVerticesWithUV(VertexHelper vh)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                float x = i % 5 == 4 ? 0.5f : i % 2 == 0 ? 0 : 1;
                float y = _vertices[i].dist;

                Vector4 uv = new Vector4(x, y);
                vh.AddVert(_vertices[i].vert, lineColor, uv);
            }
        }

        public float GetFillAmount()
        {
            return material.GetFloat("_DisolveValue");
        }

        public void SetFillAmount(float process)
        {
            material.SetFloat("_DisolveValue", process);
        }

        //버텍스 넣어주
        private void CreateLineSegments(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta * 0.5f) : Vector2.zero;

            Vector3 position = new Vector3();

            Quaternion point1Rot = Quaternion.Euler(0, 0, RotatePointToward(point1, point2) + 90f);
            position = point1Rot * new Vector3(-thickness * 0.5f, 0f);
            position += point1 - offset;
            _vertices.Add((position, _distance));

            position = point1Rot * new Vector3(thickness * 0.5f, 0f);
            position += point1 - offset;
            _vertices.Add((position, _distance));

            _distance += Vector2.Distance(point1, point2) / _totalDistance;

            Quaternion point2Rot = Quaternion.Euler(0, 0, RotatePointToward(point2, point1) - 90f);
            position = point2Rot * new Vector3(-thickness * 0.5f, 0f);
            position += point2 - offset;
            _vertices.Add((position, _distance));

            position = point2Rot * new Vector3(thickness * 0.5f, 0f);
            position += point2 - offset;
            _vertices.Add((position, _distance));

            //이놈은 가운데인
            position = point2 - offset;
            _vertices.Add((position, _distance));
        }

        private void GetTotalDistance()
        {
            _totalDistance = 0;
            if (points == null) return;


            for (int i = 0; i < points.Length - 1; i++)
                _totalDistance += Vector2.Distance(points[i], points[i + 1]);
        }

        private float RotatePointToward(Vector3 vert, Vector3 target)
            => Mathf.Atan2(target.y - vert.y, target.x - vert.x) * Mathf.Rad2Deg;

        protected virtual void Update()
        {
            // OnPopulateMesh는 UI 요소에 변경될 때만(크기, 피봇, 앵커 등) 실행되므로,
            // 매 프레임 실행하기 위해 SetVerticesDirty 함수를 실행한다.
            SetVerticesDirty();
        }

        protected override void OnValidate() // 인스펙터 창에서 프로퍼티를 변경할 때 실행되는 함수
        {
            base.OnValidate();
        }
    }
}
