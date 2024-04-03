using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.linalg._2d;
using MathLib.utils;

namespace MathLib.geometry._2d
{
    public class AABB
    {
        public AABB()
        {
            SetEmpty();
        }
        public AABB(Vec2 pos)
        {
            Init(pos);
        }
        public AABB(Vec2 bb_min, Vec2 bb_max)
        {
            this.Min = new Vec2(bb_min);
            this.Max = new Vec2(bb_max);
        }

        public static AABB CopyFrom(AABB other)
        {
            return new AABB(other.Min.Copy(), other.Max.Copy());
        }
        public AABB Copy()
        {
            return CopyFrom(this);
        }

        public Vec2 Min
        {
            get;
            set;
        }
        public Vec2 Max
        {
            get;
            set;
        }

        public void SetEmpty()
        {
            this.Min = new Vec2(float.PositiveInfinity, float.PositiveInfinity);
            this.Max = new Vec2(float.NegativeInfinity, float.NegativeInfinity);
        }
        public void SetInfinity()
        {
            this.Min = new Vec2(float.NegativeInfinity, float.NegativeInfinity);
            this.Max = new Vec2(float.PositiveInfinity, float.PositiveInfinity);
        }

        public void Init(Vec2 pos)
        {
            this.Min = new Vec2(pos);
            this.Max = new Vec2(pos);
        }

        public void AddPoint(Vec2 pos)
        {
            if (pos.X < this.Min.X)
                this.Min.X = pos.X;
            if (pos.X > this.Max.X)
                this.Max.X = pos.X;

            if (pos.Y < this.Min.Y)
                this.Min.Y = pos.Y;
            if (pos.Y > this.Max.Y)
                this.Max.Y = pos.Y;
        }

        public void AddAABB(AABB other)
        {
            AddPoint(other.Min);
            AddPoint(other.Max);
        }

        public Vec2 Size
        {
            get { return this.Max - this.Min; }
        }

        public bool OverlapsOnAxis(AABB other, int axis)
        {
            if (other.Max[axis] < this.Min[axis]) return false;
            if (other.Min[axis] > this.Max[axis]) return false;
            return true;
        }

        public bool Overlaps(AABB other)
        {
            return OverlapsOnAxis(other, 0) &&
                   OverlapsOnAxis(other, 1);
        }

        public bool OverlapsExpandedPoint(Vec2 pos, float radius)
        {
            if (pos[0] + radius < this.Min[0]) return false;
            if (pos[0] - radius > this.Max[0]) return false;

            if (pos[1] + radius < this.Min[1]) return false;
            if (pos[1] - radius > this.Max[1]) return false;

            return true;
        }

        public bool Contains(Vec2 pos)
        {
            if (pos[0] < this.Min[0]) return false;
            if (pos[0] > this.Max[0]) return false;

            if (pos[1] < this.Min[1]) return false;
            if (pos[1] > this.Max[1]) return false;

            return true;
        }

        public bool Contains(AABB aabb)
        {
            if (aabb.Min[0] < this.Min[0]) return false;
            if (aabb.Max[0] > this.Max[0]) return false;

            if (aabb.Min[1] < this.Min[1]) return false;
            if (aabb.Max[1] > this.Max[1]) return false;

            return true;
        }

        public bool OverlapsCircle(Vec2 circle_centre, float circle_radius)
        {
            float circle_radius_sq = circle_radius * circle_radius;

            for (int vtx_idx = 0; vtx_idx < 4; vtx_idx++)
                if (Vec2.DistanceSquared(GetVertex(vtx_idx), circle_centre) <= circle_radius_sq)
                    return true;
            return false;
        }

        // Returns squared distance from pos to closest point on AABB, or 0 if pos is inside AABB.
        public float PointDistanceSquared(Vec2 pos)
        {
            // Code below is adapted from Christer Ericsson's book "Real-Time Collision Detection".

            float dist_sq = 0.0f;

            // For each axis count any excess distance outside box extents.

            if (pos[0] < this.Min[0]) dist_sq += UtilsFloat.Sqr(this.Min[0] - pos[0]);
            else if (pos[0] > this.Max[0]) dist_sq += UtilsFloat.Sqr(pos[0] - this.Max[0]);

            if (pos[1] < this.Min[1]) dist_sq += UtilsFloat.Sqr(this.Min[1] - pos[1]);
            else if (pos[1] > this.Max[1]) dist_sq += UtilsFloat.Sqr(pos[1] - this.Max[1]);

            return dist_sq;
        }

        // Returns squared distance from pos to closest point on AABB, or negative squared distance if pos is inside AABB.
        public float PointDistanceSquaredSigned(Vec2 pos)
        {
            // Code below is adapted from Christer Ericsson's book "Real-Time Collision Detection".

            float dist_sq = 0.0f;

            if (this.Contains(pos))
            {
                // For each axis count any excess distance outside box extents.

                float dist_min, dist_max;

                dist_min = pos[0] - this.Min[0];
                dist_max = this.Max[0] - pos[0];
                dist_sq -= (dist_min < dist_max) ? UtilsFloat.Sqr(dist_min) : UtilsFloat.Sqr(dist_max);

                dist_min = pos[1] - this.Min[1];
                dist_max = this.Max[1] - pos[1];
                dist_sq -= (dist_min < dist_max) ? UtilsFloat.Sqr(dist_min) : UtilsFloat.Sqr(dist_max);
            }
            else
            {
                // For each axis count any excess distance outside box extents.

                if (pos[0] < this.Min[0]) dist_sq += UtilsFloat.Sqr(this.Min[0] - pos[0]);
                else if (pos[0] > this.Max[0]) dist_sq += UtilsFloat.Sqr(pos[0] - this.Max[0]);

                if (pos[1] < this.Min[1]) dist_sq += UtilsFloat.Sqr(this.Min[1] - pos[1]);
                else if (pos[1] > this.Max[1]) dist_sq += UtilsFloat.Sqr(pos[1] - this.Max[1]);
            }

            return dist_sq;
        }

        public void ExtrudeAxis(float offset, int axis)
        {
            this.Min[axis] -= offset;
            this.Max[axis] += offset;
        }

        public void Extrude(float offset)
        {
            Extrude(offset, offset);
        }

        public void Extrude(float offset_x, float offset_y)
        {
            ExtrudeAxis(offset_x, 0);
            ExtrudeAxis(offset_y, 1);
        }

        public void Extrude(Vec2 offset)
        {
            this.Min -= offset;
            this.Max += offset;
        }

        //////////////////////////////////////////////////////////////////////////        
        // Inflates axis by fraction.
        // If fraction = 1 the length along the axis will be doubled. Corresponds to scale(fraction + 1) if axis is around origin.
        // Inflation means centering axis around origin, scaling and then translating back:
        //   c = aabb.centroid(); aabb.translate(-c); aabb.scale(fraction + 1); aabb.translate(+c);
        // Both sides will be extruded an equal amount as opposed to general scaling.
        // 0 <= fraction.
        public void InflateAxis(float fraction, int axis)
        {
            float offset = (Max[axis] - Min[axis]) * 0.5f * fraction;
            ExtrudeAxis(offset, axis);
        }

        // Inflates box by fraction. 
        // If fraction = 1 the size of the box will be doubled. Corresponds to scale(fraction + 1) if box is around origin.
        // Inflation means centering box around origin, scaling and then translating back:
        //   c = aabb.centroid(); aabb.translate(-c); aabb.scale(fraction + 1); aabb.translate(+c);
        // 0 <= fraction.
        public void Inflate(float fraction)
        {
            Inflate(fraction, fraction);
        }

        // Inflates each axis by fraction_i.
        // If fraction_i = 1 the length along the axis will be doubled. Corresponds to scale(fraction + 1) if box is around origin.
        // Inflation means centering box around origin, scaling and then translating back:
        //   c = aabb.centroid(); aabb.translate(-c); aabb.scale(fraction + 1); aabb.translate(+c);
        // 0 <= fraction_i.
        public void Inflate(float fraction_x, float fraction_y)
        {
            InflateAxis(fraction_x, 0);
            InflateAxis(fraction_y, 1);
        }

        // Inflates each axis by fraction[i].
        // If fraction[i] is 1 the box will double in length along that axis. Corresponds to scale(fraction + 1) if box is around origin.
        // Inflation means centering box around origin, scaling and then translating back:
        //   c = aabb.centroid(); aabb.translate(-c); aabb.scale(fraction + 1); aabb.translate(+c);
        // 0 <= fraction[i]
        public void Inflate(Vec2 fraction)
        {
            Inflate(fraction[0], fraction[1]);
        }

        //////////////////////////////////////////////////////////////////////////

        // Scales axis by scale_factor.
        // If scale_factor = 2 the length along the axis will be doubled. Corresponds to inflate(scale_factor - 1) if axis is around origin.
        // 0 <= scale_factor.
        public void ScaleAxis(float scale_factor, int axis)
        {
            Min[axis] *= scale_factor;
            Max[axis] *= scale_factor;
        }

        // Scales box by scale_factor. 
        // If scale_factor = 2 the size of the box will be doubled. Corresponds to inflate(scale_factor - 1) if box is around origin.
        // 0 <= scale_factor.
        public void Scale(float scale_factor)
        {
            Scale(scale_factor, scale_factor);
        }

        // Scales each axis by scale_factor_i. 
        // If scale_factor_i = 2 the length along the axis will be doubled. Corresponds to inflate(scale_factor - 1) if box is around origin.
        // 0 <= scale_factor_i.
        public void Scale(float scale_factor_x, float scale_factor_y)
        {
            ScaleAxis(scale_factor_x, 0);
            ScaleAxis(scale_factor_y, 1);
        }

        // Scales each axis by scale_factor[i].
        // If scale_factor[i] is 2 the box will double in length along that axis. Corresponds to inflate(scale_factor - 1) if box is around origin.
        // 0 <= scale_factor[i]
        public void Scale(Vec2 scale_factor)
        {
            Inflate(scale_factor[0], scale_factor[1]);
        }

        //////////////////////////////////////////////////////////////////////////

        public void TranslateAxis(float offset, int axis)
        {
            Min[axis] += offset;
            Max[axis] += offset;
        }

        public void Translate(Vec2 offset)
        {
            TranslateAxis(offset[0], 0);
            TranslateAxis(offset[1], 1);
        }

        //////////////////////////////////////////////////////////////////////////

        public float Centroid(int axis)
        {
            return (Min[axis] + Max[axis]) * 0.5f;
        }

        public Vec2 Centroid()
        {
            return (Min + Max) * 0.5f;
        }

        //////////////////////////////////////////////////////////////////////////

        // Boolean Union
        public static AABB operator +(AABB bbA, AABB bbB)
        {
            AABB bb_union = bbA.Copy();

            if (bb_union.Min[0] > bbB.Min[0]) bb_union.Min[0] = bbB.Min[0];
            if (bb_union.Min[1] > bbB.Min[1]) bb_union.Min[1] = bbB.Min[1];

            if (bb_union.Max[0] < bbB.Max[0]) bb_union.Max[0] = bbA.Max[0];
            if (bb_union.Max[1] < bbB.Max[1]) bb_union.Max[1] = bbA.Max[1];

            return bb_union;
        }

        // Boolean Intersection
        public static AABB operator *(AABB bbA, AABB bbB)
        {
            AABB bb_inter = bbA.Copy();

            if (bb_inter.Min[0] < bbB.Min[0]) bb_inter.Min[0] = bbB.Min[0];
            if (bb_inter.Min[1] < bbB.Min[1]) bb_inter.Min[1] = bbB.Min[1];

            if (bb_inter.Max[0] > bbB.Max[0]) bb_inter.Max[0] = bbB.Max[0];
            if (bb_inter.Max[1] > bbB.Max[1]) bb_inter.Max[1] = bbB.Max[1];

            return bb_inter;
        }


        public Vec2 GetEdgeVertex0(int edge_idx)
        {
            switch (edge_idx)
            {
                case 0: return GetVertex(0);
                case 1: return GetVertex(1);
                case 2: return GetVertex(2);
                case 3: return GetVertex(3);
            }
            return Vec2.NaN;
        }

        public Vec2 GetEdgeVertex1(int edge_idx)
        {
            switch (edge_idx)
            {
                case 0: return GetVertex(1);
                case 1: return GetVertex(2);
                case 2: return GetVertex(3);
                case 3: return GetVertex(0);
            }
            return Vec2.NaN;
        }

        public Vec2 GetVertex(int vtx_idx)
        {
            switch (vtx_idx)
            {
                case 0: return Min;
                case 1: return new Vec2(Max.X, Min.Y);
                case 2: return Max;
                case 3: return new Vec2(Min.X, Max.Y);
            }
            return Vec2.NaN;
        }
    }
}