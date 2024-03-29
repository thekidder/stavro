﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Shape {
    void UpdateShape(); // called to sync cached data
    Rect AABB();
    HashSet<Vector2> ProjectingAxes();
    Vector2 Project(Vector2 axis);
}
