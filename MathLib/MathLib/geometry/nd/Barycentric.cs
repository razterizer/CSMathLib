using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.linalg._2d;
using MathLib.linalg._3d;
using MathLib.linalg._4d;
using MathLib.utils;

namespace MathLib.geometry.nd
{
    public class Barycentric
    {
        #region LINES
        public static Vec2 BaryCoord(Vec3 P, Vec3 v0, Vec3 v1)
        {
            float u = Vec3.Dot(P - v0, v1 - v0) / Vec3.DistanceSquared(v0, v1);
            return U2Bary(u);
        }

        public static Vec2 BaryCoord(Vec2 P, Vec2 v0, Vec2 v1)
        {
            float u = Vec2.Dot(P - v0, v1 - v0) / Vec2.DistanceSquared(v0, v1);
            return U2Bary(u);
        }

        public static Vec2 BaryCoord(float P, float v0, float v1)
        {
            return U2Bary((P - v0) / (v1 - v0));
        }

        public static Vec4 BaryInterpolate(Vec2 bc, Vec4 v0, Vec4 v1)
        { return bc[0] * v0 + bc[1] * v1; }
        public static Vec3 BaryInterpolate(Vec2 bc, Vec3 v0, Vec3 v1)
        { return bc[0] * v0 + bc[1] * v1; }
        public static Vec2 BaryInterpolate(Vec2 bc, Vec2 v0, Vec2 v1)
        { return bc[0] * v0 + bc[1] * v1; }
        public static float BaryInterpolate(Vec2 bc, float v0, float v1)
        { return bc[0] * v0 + bc[1] * v1; }
        #endregion

        #region TRIANGLES
        // bary(...)
        // Notes:
        //   Calculates the barycentric coordinates for the point P that is inside
        //   the triangle v0,v1,v2
        // Return Values:
        //   Contains the barycentric coordinates that are organized as follows
        //     return[0] = (1-u-v)
        //     return[1] = u
        //     return[2] = v
        //   These values can then be used for linear interpolation, example:
        //     P = return[0]*v0 + return[1]*v1 + return[2]*v2
        //   Currently the only way to discover an error is to check if all returned
        //   components are set to 0.
        //   Generally you can check the following condition as well:
        //     return[0] + return[1] + return[2] = 1
        // Dev Notes:
        //   Investigate what happens if P is outside the triangle.
        public static Vec3 BaryCoord(Vec3 P, Vec3 v0, Vec3 v1, Vec3 v2)
        {
            //////////////////////////////////////////////////////////////////////////

            // Other robust and smart algorithm (slightly smarter than above) from http://www.blackpawn.com/texts/pointinpoly/default.html.
            // Points can be outside the triangle boundary.
            // Can points be outside the triangle plane?

            // Compute vectors
            Vec3 e0 = v2 - v0;
            Vec3 e1 = v1 - v0;
            Vec3 e2 = P - v0;

            // Compute dot products
            float dot00 = Vec3.Dot(e0, e0);
            float dot01 = Vec3.Dot(e0, e1);
            float dot02 = Vec3.Dot(e0, e2);
            float dot11 = Vec3.Dot(e1, e1);
            float dot12 = Vec3.Dot(e1, e2);

            // Compute barycentric coordinates
            float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            float u = (dot00 * dot12 - dot01 * dot02) * invDenom;
            float v = (dot11 * dot02 - dot01 * dot12) * invDenom;

            // Check if point is in triangle
            //return (u >= 0) && (v >= 0) && (u + v < 1);

            return UV2Bary(u, v);
        }

        public static Vec4 BaryInterpolate(Vec3 bc, Vec4 v0, Vec4 v1, Vec4 v2)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2; }
        public static Vec3 BaryInterpolate(Vec3 bc, Vec3 v0, Vec3 v1, Vec3 v2)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2; }
        public static Vec2 BaryInterpolate(Vec3 bc, Vec2 v0, Vec2 v1, Vec2 v2)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2; }
        public static float BaryInterpolate(Vec3 bc, float v0, float v1, float v2)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2; }
        #endregion

        #region TETRAS
#if false
  public static Vec4 baryCoord(Vec3 P, Vec3 t0, Vec3 t1, Vec3 t2, Vec3 t3)
  {
    // http://www.gamedev.net/topic/604338-fasted-algorithm-for-calculating-4d-barycentric-coordinates/

    Mtx3 T;
    Vec3 q = P - t3;
    Vec3 q0 = t0 - t3;
    Vec3 q1 = t1 - t3;
    Vec3 q2 = t2 - t3;
    T.setColumnVec3(0, q0);
    T.setColumnVec3(1, q1);
    T.setColumnVec3(2, q2);

    float det = T.determinant();

    Vec4 lambda;

    T.setColumnVec3(0, q);
    lambda[0] = T.determinant();

    T.setColumnVec3(0, q0); T.setColumnVec3(1, q);
    lambda[1] = T.determinant();

    T.setColumnVec3(1, q1); T.setColumnVec3(2, q);
    lambda[2] = T.determinant();

    if (!(Math.Abs(det) < float.Epsilon))
      lambda /= det;

    lambda[3] = 1.0f - lambda[0] - lambda[1] - lambda[2];

    return lambda;
  }
#endif

        public static Vec4 BaryInterpolate(Vec4 bc, Vec4 v0, Vec4 v1, Vec4 v2, Vec4 v3)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2 + bc[3] * v3; }
        public static Vec3 BaryInterpolate(Vec4 bc, Vec3 v0, Vec3 v1, Vec3 v2, Vec3 v3)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2 + bc[3] * v3; }
        public static Vec2 BaryInterpolate(Vec4 bc, Vec2 v0, Vec2 v1, Vec2 v2, Vec2 v3)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2 + bc[3] * v3; }
        public static float BaryInterpolate(Vec4 bc, float v0, float v1, float v2, float v3)
        { return bc[0] * v0 + bc[1] * v1 + bc[2] * v2 + bc[3] * v3; }
        #endregion

        #region N-DIMENSIONAL
        Vec4 BaryInterpolate(float[] bc, Vec4[] v)
        {
            if (bc.Length != v.Length)
                throw new IndexOutOfRangeException("Barycentric coord 'bc' must reflect the points in vector 'v'!");
            Vec4 p = new Vec4();
            for (int v_idx = 0; v_idx < v.Length; v_idx++)
                p += bc[v_idx] * v[v_idx];
            return p;
        }
        Vec3 BaryInterpolate(float[] bc, Vec3[] v)
        {
            if (bc.Length != v.Length)
                throw new IndexOutOfRangeException("Barycentric coord 'bc' must reflect the points in vector 'v'!");
            Vec3 p = new Vec3();
            for (int v_idx = 0; v_idx < v.Length; v_idx++)
                p += bc[v_idx] * v[v_idx];
            return p;
        }
        Vec2 BaryInterpolate(float[] bc, Vec2[] v)
        {
            if (bc.Length != v.Length)
                throw new IndexOutOfRangeException("Barycentric coord 'bc' must reflect the points in vector 'v'!");
            Vec2 p = new Vec2();
            for (int v_idx = 0; v_idx < v.Length; v_idx++)
                p += bc[v_idx] * v[v_idx];
            return p;
        }
        float BaryInterpolate(float[] bc, float[] v)
        {
            if (bc.Length != v.Length)
                throw new IndexOutOfRangeException("Barycentric coord 'bc' must reflect the points in vector 'v'!");
            float p = 0.0f;
            for (int v_idx = 0; v_idx < v.Length; v_idx++)
                p += bc[v_idx] * v[v_idx];
            return p;
        }
        #endregion

        #region UTILS
        public static Vec2 U2Bary(float u)
        {
            return new Vec2(1.0f - u, u);
        }

        public static Vec3 UV2Bary(float u, float v)
        {
            return new Vec3(1.0f - u - v, u, v);
        }

        public static Vec4 UVW2Bary(float u, float v, float w)
        {
            return new Vec4(1.0f - u - v - w, u, v, w);
        }
        #endregion

        #region EXTRA FUNCTIONS
#if false
  // Returns clamped barycentric coordinate from point in triangle.
  public static Vec3 BaryCoordClampPoint(Vec3 P, Vec3 t0, Vec3 t1, Vec3 t2)
  {
    float s, t;
    TriangleHelper::PointTriangleDistanceSquared(P, t0, t1, t2, out s, out t);
    Vec3 bary_coord = UV2Bary(s, t);
    return bary_coord;
  }
#endif

        // Returns clamped barycentric coordinate from previously calculated barycentric coordinate.
        public static Vec2 BaryCoordClampBC(Vec2 bc)
        {
            float nbc = bc[1];
            UtilsFloat.ClampSet(ref nbc, 0.0f, 1.0f);
            return U2Bary(nbc);
        }

        // Returns clamped barycentric coordinate from previously calculated barycentric coordinate.
        public static Vec3 BaryCoordClampBC(Vec3 bc)
        {
            Vec2 nbc = new Vec2(bc[1], bc[2]);
            nbc.Clamp(0.0f, 1.0f);
            float sum = nbc[0] + nbc[1];
            if (sum > 1.0f)
            {
                nbc *= 1.0f / sum;
                sum = 1.0f;
            }
            //m = 1.0f - sum;
            return UV2Bary(nbc[0], nbc[1]);
        }

        // Originally from Tetra3d.
        // Constrain barycentric coord to lie inside or on face of tetra...
        // Returns clamped barycentric coordinate from previously calculated barycentric coordinate.
        public static Vec4 BaryCoordClampBC(Vec4 bc)
        {
            Vec3 nbc = new Vec3(bc[1], bc[2], bc[3]);
            nbc.Clamp(0.0f, 1.0f);
            float sum = nbc[0] + nbc[1] + nbc[2];
            if (sum > 1.0f)
            {
                nbc *= 1.0f / sum;
                sum = 1.0f;
            }
            //m = 1.0f - sum;
            return UVW2Bary(nbc[0], nbc[1], nbc[2]);
        }

#if false
  /// Make sure that a point never gets outside of a triangle.
  /// Use the closed triangle (coordinate) set.
  public static Vec3 ConstrainPointInTriangleClosed(Vec3 P, Vec3 t0, Vec3 t1, Vec3 t2)
  {
    Vec3 bary_coord = BaryCoordClampPoint(P, t0, t1, t2);
    Vec3 p_in_closed_triangle_set = BaryInterpolate(bary_coord, t0, t1, t2);
    return p_in_closed_triangle_set;
  }
#endif
        #endregion

    }
}
