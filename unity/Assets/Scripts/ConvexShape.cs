using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConvexShape : MonoBehaviour, Shape {

    public List<Vector2> points = new List<Vector2> {
        new Vector2(-0.5f, -0.5f),
        new Vector2(-0.5f,  0.5f),
        new Vector2( 0.5f,  0.5f),
        new Vector2( 0.5f, -0.5f)
    };

    private Rect aabb = new Rect();
    private HashSet<Vector2> axes = new HashSet<Vector2>();
    private Vector3 lastPosition;
    private Quaternion lastRotation;

	// Use this for initialization
	void Start () {
        CalculateAABB();
        CalculateAxes();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Vector3 start, end;
        for (int i = 1; i < points.Count; ++i) {
            start = new Vector3(points[i - 1].x, points[i - 1].y, 0f);
            end = new Vector3(points[i].x, points[i].y, 0f);
            Gizmos.DrawLine(transform.TransformPoint(start), transform.TransformPoint(end));
        }

        start = new Vector3(points[0].x, points[0].y, 0f);
        end = new Vector3(points[points.Count - 1].x, points[points.Count - 1].y, 0f);
        Gizmos.DrawLine(transform.TransformPoint(start), transform.TransformPoint(end));
    }

    public void UpdateShape() {
        if (transform.position != lastPosition || transform.rotation != lastRotation) {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            CalculateAABB();
            CalculateAxes();
        }
    }

    public Rect AABB() {
        return aabb;
    }

    public HashSet<Vector2> ProjectingAxes() {
        return axes;
    }

    public Vector2 Project(Vector2 axis) {
        float min = float.MaxValue;
        float max = float.MinValue;
        foreach (Vector2 point in points) {
            float dot = Vector2.Dot(transform.TransformPoint(point), axis);
            if (dot < min) {
                min = dot;
            }
            if (dot > max) {
                max = dot;
            }
        }
        return new Vector2(min, max);
    }

    private void CalculateAABB() {
        Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 max = new Vector2(float.MinValue, float.MinValue);

        foreach (Vector2 point in points) {
            Vector2 worldPoint = transform.TransformPoint(point);

            if (worldPoint.x < min.x) {
                min.x = worldPoint.x;
            } else if (worldPoint.x > max.x) {
                max.x = worldPoint.x;
            }

            if (worldPoint.y < min.y) {
                min.y = worldPoint.y;
            } else if (worldPoint.y > max.y) {
                max.y = worldPoint.y;
            }
        }

        aabb.xMin = min.x;
        aabb.yMin = min.y;
        aabb.xMax = max.x;
        aabb.yMax = max.y;
    }

    private void CalculateAxes() {
        axes.Clear();
        Vector2 axis;
        for (int i = 1; i < points.Count; ++i) {
            axis = transform.TransformPoint(points[i]) - transform.TransformPoint(points[i - 1]);
            axes.Add(axis.normalized);
        }
        axis = transform.TransformPoint(points[points.Count - 1]) - transform.TransformPoint(points[0]);
        axes.Add(axis.normalized);
    }
}
