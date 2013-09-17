﻿using UnityEngine;
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
        if (transform.position != lastPosition || transform.rotation != lastRotation) {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            CalculateAABB();
            CalculateAxes();
        }
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
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, 0f);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, 0f);

        foreach (Vector2 point in points) {
            if (point.x < min.x) {
                min.x = point.x;
            } else if (point.x > max.x) {
                max.x = point.x;
            }

            if (point.y < min.y) {
                min.y = point.y;
            } else if (point.y > max.y) {
                max.y = point.y;
            }
        }

        min = transform.TransformPoint(min);
        max = transform.TransformPoint(max);

        aabb.xMin = min.x;
        aabb.yMin = min.y;
        aabb.xMax = max.x;
        aabb.yMax = max.y;
    }

    private void CalculateAxes() {
        axes.Clear();
        for (int i = 1; i < points.Count; ++i) {
            axes.Add(transform.TransformPoint(points[i] - points[i - 1]).normalized);
        }
        axes.Add(transform.TransformPoint(points[points.Count - 1] - points[0]).normalized);
    }
}