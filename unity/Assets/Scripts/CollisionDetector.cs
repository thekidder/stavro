using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public Vector2 MinTranslation(Shape lhs, Shape rhs) {
        lhs.UpdateShape();
        rhs.UpdateShape();

        if (!Intersects(lhs.AABB(), rhs.AABB())) {
            return Vector2.zero;
        }

        HashSet<Vector2> axes;
        axes = lhs.ProjectingAxes();
        axes.UnionWith(rhs.ProjectingAxes());
        
        Vector2 translation = Vector2.zero;
        float minOverlap = float.MaxValue;

        foreach(Vector2 axis in axes) {
            Vector2 l = lhs.Project(axis);
            Vector2 r = rhs.Project(axis);
            float overlap = Overlap(l, r);
            if (overlap <= 0) {
                return Vector2.zero;
            } else if (overlap < minOverlap) {
                translation = axis * overlap;
                minOverlap = overlap;

                if (l.x < r.x) {
                    translation = -translation;
                }
            }
        }
        return translation;
    }

    // given two ranges, find the overlap
    // negative means no overlap
    static private float Overlap(Vector2 lhs, Vector2 rhs) {
        if (lhs.x < rhs.x) {
            return lhs.y - rhs.x;
        } else {
            return rhs.y - lhs.x;
        }
    }

    static public bool Intersects(Rect lhs, Rect rhs) {
        return lhs.xMin < rhs.xMax
            && lhs.yMin < rhs.yMax
            && rhs.xMin < lhs.xMax
            && rhs.yMin < lhs.yMax;
    }
}
