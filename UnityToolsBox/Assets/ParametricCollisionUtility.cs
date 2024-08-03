using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCollisionUtility
{
    public static bool CheckSegmentPlanIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _planPoint, Vector3 _planNormal, ref float _t)
    {
        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float tNumerator = Vector3.Dot(_planNormal, _planPoint - _segmentAPointA);
        float tDenominator = Vector3.Dot(_planNormal, segmentDir);
        float epsilon = Mathf.Epsilon;

        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = tNumerator / tDenominator;
        float segmentDirMagitude = segmentDir.magnitude;
        if (t <= 0 || t >= segmentDirMagitude)
            return false;

        _t = t;
        return true;
    }

    public static bool CheckSegmentPlanXZIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, ref float _t0)
    {
        if (_segmentAPointA.y > 0 && _segmentAPointB.y > 0 || _segmentAPointA.y < 0 && _segmentAPointB.y < 0)
            return false;

        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float tDenominator = segmentDir.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        _t0 = -_segmentAPointA.y / tDenominator;

        return true;
    }

    public static bool CheckSegmentQuadIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref float _t0)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _segmentAPointA = MathUtility.WorldBaseChangementOptimised(_segmentAPointA, quadPointA, rotation, Vector3.one);
        _segmentAPointB = MathUtility.WorldBaseChangementOptimised(_segmentAPointB, quadPointA, rotation, Vector3.one);

        float t = 0.0f;
        if (CheckSegmentPlanXZIntersection(_segmentAPointA, _segmentAPointB, ref t))
        {
            Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
            Vector3 planResultPoint = _segmentAPointA + t * segmentDir;
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _t0 = t;
                return true;
            }
        }

        return false;
    }

    public static bool CheckSegmentCircleIntersection(Vector3 _segmentAPointA, Vector3 _segmentAPointB, Vector3 _circleOrigin, Vector3 _circleNormal, float _radius, ref float _t)
    {
        Vector3 segmentDir = (_segmentAPointB - _segmentAPointA);
        float t = 0;
        if (CheckSegmentPlanIntersection(_segmentAPointA, _segmentAPointB, _circleOrigin, _circleNormal, ref t))
        {
            Vector3 point = _segmentAPointA + t * segmentDir;
            if ((point - _circleOrigin).sqrMagnitude <= _radius * _radius)
            {
                _t = t;
                return true;
            }
        }

        return false;
    }

    public static int CheckSegmentSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _SphereOrigin, float _radius, ref float _t0, ref float _t1)
    {
        Vector3 segmentDir = _segmentPointB - _segmentPointA;
        Vector3 sphereDir = _segmentPointA - _SphereOrigin;
        float a = segmentDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(segmentDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        if (c <= 0)
            return 0;

        float delta = b * b - 4.0f * a * c;
        if (delta < 0)
            return 0;

        float epsilon = Mathf.Epsilon;
        float a2 = 2 * a;
        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0 && t < 1)
            {
                _t0 = t;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1sCondition = t1 >= 0 && t1 <= 1;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }
            return 2;
        }
        else if (t0Condition)
            _t0 = t0;
        else
            _t0 = t1;

        return 1;
    }

    public static int CheckSegmentCylinderWihtoutCapsIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {
        Vector3 cSegmentPointADir = _segmentPointA - _cylinderOrigin; // X, Y, Z
        Vector3 segmentDir = _segmentPointB - _segmentPointA; // u

        //cylinderUp // v

        Vector3 w1 = new Vector3(cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * cylinderUp.x, segmentDir.y * cylinderUp.y, segmentDir.z * cylinderUp.z);
        Vector3 w6 = new Vector3(cylinderUp.y * cylinderUp.y + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.y * cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);

        float cylinderUpSquareLength = cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, cylinderUp).sqrMagnitude / cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = Mathf.Epsilon;
        if (delta < epsilon)
            return 0;

        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0 && t < 1)
            {
                _t0 = t;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0 && t0 <= 1;
        bool t1Condition = t1 >= 0 && t1 <= 1;
        float halfHeight = _cylinderHeight / 2.0f;

        if (!t0Condition && !t1Condition)
            return 0;
        else if (t0Condition && t1Condition)
        {
            Vector3 pA = _segmentPointA + t0 * segmentDir;
            Vector3 pB = _segmentPointA + t1 * segmentDir;

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);
            float pBDotC = Vector3.Dot((pB - _cylinderOrigin), cylinderUp);
            if (Mathf.Abs(pADotC) > halfHeight && Mathf.Abs(pBDotC) > halfHeight)
                return 0;
            else if (Mathf.Abs(pADotC) > halfHeight)
            {
                _t0 = t1;
                return 1;
            }
            else if (Mathf.Abs(pBDotC) > halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }

            return 2;
        }
        else
        {
            if (t1Condition)
                t0 = t1;

            Vector3 pA = _segmentPointA + t0 * segmentDir;
            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);

            if (Mathf.Abs(pADotC) <= halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            return 0;
        }
    }

    public static int CheckSegmentSphereCapsIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckSegmentSphereIntersection(_segmentPointA, _segmentPointB, _capsOrigin, _capsRadius, ref t0, ref t1);

        if (count == 0)
            return 0;
        else if (count == 1)
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            Vector3 pA = _segmentPointA + t0 * segmentDir;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);

            if (pADotC < 0.0f)
                return 0;
            else
                _t0 = t0;

            return 1;
        }
        else
        {
            Vector3 segmentDir = _segmentPointB - _segmentPointA;
            Vector3 pA = _segmentPointA + t0 * segmentDir;
            Vector3 pB = _segmentPointA + t1 * segmentDir;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);
            float pBDotC = Vector3.Dot((pB - _capsOrigin), _capsNormal);
            if (pADotC < 0.0f && pBDotC < 0.0f)
                return 0;
            else if (pADotC < 0.0f)
            {
                _t0 = t1;
                return 1;
            }
            else if (pBDotC < 0.0f)
            {
                _t0 = t0;
                return 1;
            }

            _t0 = Mathf.Min(t0, t1);
            _t1 = Mathf.Max(t0, t1);
            return 2;
        }
    }

    public static int CheckSegmentCapsuleIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        Vector3 segmentDir = _segmentPointB - _segmentPointA;
        int count = ParametricCollisionUtility.CheckSegmentCylinderWihtoutCapsIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _t0 = t0;
            _t1 = t1;
            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f;
        int capsCount = CheckSegmentSphereCapsIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2, ref t3);
        int caps2Count = CheckSegmentSphereCapsIntersection(_segmentPointA, _segmentPointB, _cylinderOrigin - cylinderUp * halfHeight, -cylinderUp, _cylinderRadius, ref t4, ref t5);

        if (capsCount == 2)
        {
            _t0 = t2;
            _t1 = t3;
            return 2;
        }
        else if (caps2Count == 2)
        {
            _t0 = t4;
            _t1 = t5;
            return 2;
        }
        else if (capsCount == 1 && caps2Count == 1)
        {
            _t0 = Mathf.Min(t2, t4);
            _t1 = Mathf.Max(t2, t4);

            return 2;
        }
        else if (capsCount == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t2);
            _t1 = Mathf.Max(t0, t2);
            return 2;
        }
        else if (caps2Count == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t4);
            _t1 = Mathf.Max(t0, t4);
            return 2;
        }
        else if (count == 1)
        {
            _t0 = t0;
            return 1;
        }
        else if (capsCount == 1)
        {
            _t0 = t2;
            return 1;
        }
        else if (caps2Count == 1)
        {
            _t0 = t4;
            return 1;
        }

        return 0;
    }

    public static int CheckSegmentMinkowskiBoxSphereIntersection(Vector3 _segmentPointA, Vector3 _segmentPointB, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)
    {
        Vector3 relatifPA = MathUtility.WorldBaseChangementOptimised(_segmentPointA, boxCenter, boxRotation, Vector3.one);
        Vector3 relatifPB = MathUtility.WorldBaseChangementOptimised(_segmentPointB, boxCenter, boxRotation, Vector3.one);
        Vector3 halSize = boxSize / 2.0f;
        Vector3 halSizeR = halSize + Vector3.one * sphereRadius;
        Vector3 segmentDir = relatifPB - relatifPA;


        //Left Quad Intersection
        float t = 0.0f;
        Vector3 quadPointA = halSizeR.x * Vector3.left + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t);

        //Right Quad Intersection
        float t1 = 0.0f;
        quadPointA = halSizeR.x * Vector3.right + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection2 = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Bottom Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.down + halSize.z * Vector3.back;
        intersection2 = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Top Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.up + halSize.z * Vector3.back;
        intersection2 = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Back Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.back;
        intersection2 = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Front Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.forward;
        intersection2 = CheckSegmentQuadIntersection(relatifPA, relatifPB, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }


        float t0Tmp = 0.0f, t1Tmp = 0.0f;
        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.left, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.right, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.right, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.left, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.back, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.forward, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.right, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.left, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.back, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.forward, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.right, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.left, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }


        bool CheckCapsule(Vector3 capsuleOrigin, Vector3 capsuleNormal, float capsuleSize, Vector3 dir1, Vector3 dir2)
        {
            float t2 = 0.0f, t3 = 0.0f;
            int count = CheckSegmentCapsuleIntersection(relatifPA, relatifPB, capsuleOrigin, capsuleNormal, sphereRadius, capsuleSize, ref t2, ref t3);
            if (count > 0)
            {
                int countTMP = count;
                Vector3 resultPoint = relatifPA + t2 * segmentDir;
                if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                    Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                    count--;

                if (countTMP == 2)
                {
                    resultPoint = relatifPA + t3 * segmentDir;
                    if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                        Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                        count--;
                    else if (count == 1)
                        t2 = t3;
                }
            }

            if (count == 2)
            {
                t0Tmp = Mathf.Min(t2, t3);
                t1Tmp = Mathf.Max(t2, t3);
                return true;
            }
            else if (count == 1 && intersection)
            {
                t0Tmp = Mathf.Min(t, t2);
                t1Tmp = Mathf.Max(t, t2);
                return true;
            }
            else if (count == 1)
            {
                intersection = true;
                t = t2;
            }

            return false;
        }

        if (intersection)
        {
            _t0 = t;
            return 1;
        }
        return 0;
    }


    public static bool CheckRayPlanIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _planPoint, Vector3 _planNormal, ref float _t)
    {
        float tNumerator = Vector3.Dot(_planNormal, _planPoint - _rayStartPoint);
        float tDenominator = Vector3.Dot(_planNormal, _rayDirection);
        float epsilon = Mathf.Epsilon;

        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = tNumerator / tDenominator;
        float segmentDirMagitude = _rayDirection.magnitude;
        if (t <= 0)
            return false;

        _t = t;
        return true;
    }

    public static bool CheckRayPlanXZIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, ref float _t)
    {
        if (_rayStartPoint.y > 0 && _rayDirection.y > 0 || _rayStartPoint.y < 0 && _rayDirection.y < 0)
            return false;

        float tDenominator = _rayDirection.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        _t = -_rayStartPoint.y / tDenominator;

        return true;
    }

    public static bool CheckRayQuadIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref float _t0)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _rayStartPoint = MathUtility.WorldBaseChangementOptimised(_rayStartPoint, quadPointA, rotation, Vector3.one);
        _rayDirection = MathUtility.WorldBaseChangementDirection(_rayDirection, rotation);

        float t = 0.0f;
        if (CheckRayPlanXZIntersection(_rayStartPoint, _rayDirection, ref t))
        {
            Vector3 planResultPoint = _rayStartPoint + t * _rayDirection;
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _t0 = t;
                return true;
            }
        }

        return false;
    }


    public static int CheckRayCylinderWihtoutCapsIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {
        Vector3 cSegmentPointADir = _rayStartPoint - _cylinderOrigin;
        Vector3 segmentDir = _rayDirection;

        Vector3 w1 = new Vector3(cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * cylinderUp.x, segmentDir.y * cylinderUp.y, segmentDir.z * cylinderUp.z);
        Vector3 w6 = new Vector3(cylinderUp.y * cylinderUp.y + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.y * cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);

        float cylinderUpSquareLength = cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, cylinderUp).sqrMagnitude / cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = Mathf.Epsilon;
        if (delta < epsilon)
            return 0;

        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0)
            {
                _t0 = t;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0;
        bool t1Condition = t1 >= 0;
        float halfHeight = _cylinderHeight / 2.0f;

        if (!t0Condition && !t1Condition)
            return 0;
        else if (t0Condition && t1Condition)
        {
            Vector3 pA = _rayStartPoint + t0 * segmentDir;
            Vector3 pB = _rayStartPoint + t1 * segmentDir;

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);
            float pBDotC = Vector3.Dot((pB - _cylinderOrigin), cylinderUp);
            if (Mathf.Abs(pADotC) > halfHeight && Mathf.Abs(pBDotC) > halfHeight)
                return 0;
            else if (Mathf.Abs(pADotC) > halfHeight)
            {
                _t0 = t1;
                return 1;
            }
            else if (Mathf.Abs(pBDotC) > halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }

            return 2;
        }
        else
        {
            if (t1Condition)
                t0 = t1;

            Vector3 pA = _rayStartPoint + t0 * segmentDir;
            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);

            if (Mathf.Abs(pADotC) <= halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            return 0;
        }
    }

    public static bool CheckRayCircleIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _circleOrigin, Vector3 _circleNormal, float _radius, ref float _t)
    {
        Vector3 segmentDir = _rayDirection;
        float t = 0;
        if (CheckRayPlanIntersection(_rayStartPoint, _rayDirection, _circleOrigin, _circleNormal, ref t))
        {
            Vector3 point = _rayStartPoint + t * segmentDir;
            if ((point - _circleOrigin).sqrMagnitude <= _radius * _radius)
            {
                _t = t;
                return true;
            }
        }

        return false;
    }

    public static int CheckRaySphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _SphereOrigin, float _radius, ref float _t0, ref float _t1)
    {
        Vector3 segmentDir = _rayDirection;
        Vector3 sphereDir = _rayStartPoint - _SphereOrigin;
        float a = segmentDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(segmentDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        if (c <= 0)
            return 0;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2 * a;

        float epsilon = Mathf.Epsilon;
        if (delta < epsilon)
            return 0;

        if (delta < epsilon)
        {
            float t = -b / a2;
            if (t >= 0 && t < 1)
            {
                _t0 = t;
                return 1;
            }
            else
                return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        bool t0Condition = t0 >= 0;
        bool t1sCondition = t1 >= 0;
        if (!t0Condition && !t1sCondition)
            return 0;

        if (t0Condition && t1sCondition)
        {
            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }
            return 2;
        }
        else if (t0Condition)
            _t0 = t0;
        else
            _t0 = t1;

        return 1;
    }


    public static int CheckRayCapsuleIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        int count = ParametricCollisionUtility.CheckRayCylinderWihtoutCapsIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _t0 = t0;
            _t1 = t1;
            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f;
        int capsCount = CheckRaySphereCapsIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2, ref t3);
        int caps2Count = CheckRaySphereCapsIntersection(_rayStartPoint, _rayDirection, _cylinderOrigin - cylinderUp * halfHeight, -cylinderUp, _cylinderRadius, ref t4, ref t5);

        if (capsCount == 2)
        {
            _t0 = t2;
            _t1 = t3;
            return 2;
        }
        else if (caps2Count == 2)
        {
            _t0 = t4;
            _t1 = t5;
            return 2;
        }
        else if (capsCount == 1 && caps2Count == 1)
        {
            _t0 = Mathf.Min(t2, t4);
            _t1 = Mathf.Max(t2, t4);

            return 2;
        }
        else if (capsCount == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t2);
            _t1 = Mathf.Max(t0, t2);
            return 2;
        }
        else if (caps2Count == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t4);
            _t1 = Mathf.Max(t0, t4);
            return 2;
        }
        else if (count == 1)
        {
            _t0 = t0;
            return 1;
        }
        else if (capsCount == 1)
        {
            _t0 = t2;
            return 1;
        }
        else if (caps2Count == 1)
        {
            _t0 = t4;
            return 1;
        }

        return 0;
    }

    public static int CheckRaySphereCapsIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckRaySphereIntersection(_rayStartPoint, _rayDirection, _capsOrigin, _capsRadius, ref t0, ref t1);

        if (count == 0)
            return 0;
        else if (count == 1)
        {
            Vector3 pA = _rayStartPoint + t0 * _rayDirection;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);

            if (pADotC < 0.0f)
                return 0;
            else
                _t0 = t0;

            return 1;
        }
        else
        {
            Vector3 pA = _rayStartPoint + t0 * _rayDirection;
            Vector3 pB = _rayStartPoint + t1 * _rayDirection;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);
            float pBDotC = Vector3.Dot((pB - _capsOrigin), _capsNormal);
            if (pADotC < 0.0f && pBDotC < 0.0f)
                return 0;
            else if (pADotC < 0.0f)
            {
                _t0 = t1;
                return 1;
            }
            else if (pBDotC < 0.0f)
            {
                _t0 = t0;
                return 1;
            }

            _t0 = Mathf.Min(t0, t1);
            _t1 = Mathf.Max(t0, t1);
            return 2;
        }
    }

    public static int CheckRayMinkowskiBoxSphereIntersection(Vector3 _rayStartPoint, Vector3 _rayDirection, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)
    {
        Vector3 relatifPA = MathUtility.WorldBaseChangementOptimised(_rayStartPoint, boxCenter, boxRotation, Vector3.one);
        Vector3 halSize = boxSize / 2.0f;
        Vector3 halSizeR = halSize + Vector3.one * sphereRadius;
        Vector3 segmentDir = MathUtility.WorldBaseChangementDirection(_rayDirection, boxRotation);


        //Left Quad Intersection
        float t = 0.0f;
        Vector3 quadPointA = halSizeR.x * Vector3.left + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t);

        //Right Quad Intersection
        float t1 = 0.0f;
        quadPointA = halSizeR.x * Vector3.right + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection2 = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Bottom Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.down + halSize.z * Vector3.back;
        intersection2 = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Top Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.up + halSize.z * Vector3.back;
        intersection2 = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Back Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.back;
        intersection2 = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Front Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.forward;
        intersection2 = CheckRayQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }


        float t0Tmp = 0.0f, t1Tmp = 0.0f;
        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.left, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.right, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.right, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.left, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.back, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.forward, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.right, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.left, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.back, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.forward, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.right, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.left, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }


        bool CheckCapsule(Vector3 capsuleOrigin, Vector3 capsuleNormal, float capsuleSize, Vector3 dir1, Vector3 dir2)
        {
            float t2 = 0.0f, t3 = 0.0f;
            int count = CheckRayCapsuleIntersection(relatifPA, segmentDir, capsuleOrigin, capsuleNormal, sphereRadius, capsuleSize, ref t2, ref t3);
            if (count > 0)
            {
                int countTMP = count;
                Vector3 resultPoint = relatifPA + t2 * segmentDir;
                if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                    Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                    count--;

                if (countTMP == 2)
                {
                    resultPoint = relatifPA + t3 * segmentDir;
                    if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                        Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                        count--;
                    else if (count == 1)
                        t2 = t3;
                }
            }

            if (count == 2)
            {
                t0Tmp = Mathf.Min(t2, t3);
                t1Tmp = Mathf.Max(t2, t3);
                return true;
            }
            else if (count == 1 && intersection)
            {
                t0Tmp = Mathf.Min(t, t2);
                t1Tmp = Mathf.Max(t, t2);
                return true;
            }
            else if (count == 1)
            {
                intersection = true;
                t = t2;
            }

            return false;
        }

        if (intersection)
        {
            _t0 = t;
            return 1;
        }
        return 0;
    }


    public static bool CheckLinePlanIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _planPoint, Vector3 _planNormal, ref float _t)
    {
        float tNumerator = Vector3.Dot(_planNormal, _planPoint - _linePoint);
        float tDenominator = Vector3.Dot(_planNormal, _lineDirection);
        float epsilon = Mathf.Epsilon;

        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = tNumerator / tDenominator;
        _t = t;
        return true;
    }

    public static int CheckLineCylinderWihtoutCapsIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {
        Vector3 cSegmentPointADir = _linePoint - _cylinderOrigin;
        Vector3 segmentDir = _lineDirection;

        Vector3 w1 = new Vector3(cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z);
        Vector3 w2 = new Vector3(cSegmentPointADir.y * cylinderUp.y, cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x);
        Vector3 w3 = new Vector3(cSegmentPointADir.z * cylinderUp.z, cSegmentPointADir.x * cylinderUp.x, cSegmentPointADir.y * cylinderUp.y);
        Vector3 w4 = new Vector3(cSegmentPointADir.x * segmentDir.x, cSegmentPointADir.y * segmentDir.y, cSegmentPointADir.z * segmentDir.z);
        Vector3 w5 = new Vector3(segmentDir.x * cylinderUp.x, segmentDir.y * cylinderUp.y, segmentDir.z * cylinderUp.z);
        Vector3 w6 = new Vector3(cylinderUp.y * cylinderUp.y + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.z * cylinderUp.z, cylinderUp.x * cylinderUp.x + cylinderUp.y * cylinderUp.y);
        Vector3 w7 = new Vector3(cSegmentPointADir.x * cSegmentPointADir.x, cSegmentPointADir.y * cSegmentPointADir.y, cSegmentPointADir.z * cSegmentPointADir.z);

        float cylinderUpSquareLength = cylinderUp.magnitude;
        float a = Vector3.Cross(segmentDir, cylinderUp).sqrMagnitude / cylinderUpSquareLength;

        float w4dotw6 = Vector3.Dot(w4, w6);
        float w5dotw2s3 = Vector3.Dot(w5, w2 + w3);
        float b = 2.0f * (w4dotw6 - w5dotw2s3) / cylinderUpSquareLength;

        float w7dotw6 = Vector3.Dot(w7, w6);
        float w1dotw2 = Vector3.Dot(w1, w2);

        float c = (w7dotw6 - 2.0f * w1dotw2) / cylinderUpSquareLength - _cylinderRadius * _cylinderRadius;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2.0f * a;

        float epsilon = Mathf.Epsilon;
        if (delta < epsilon)
            return 0;

        float halfHeight = _cylinderHeight / 2.0f;

        if (delta < epsilon)
        {
            float t = -b / a2;
            Vector3 pA = _linePoint + t * segmentDir;
            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);

            if (Mathf.Abs(pADotC) <= halfHeight)
            {
                _t0 = t;
                return 1;
            }

            return 0;
        }

        float deltaSqrt = Mathf.Sqrt(delta);

        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        {
            Vector3 pA = _linePoint + t0 * segmentDir;
            Vector3 pB = _linePoint + t1 * segmentDir;

            float pADotC = Vector3.Dot((pA - _cylinderOrigin), cylinderUp);
            float pBDotC = Vector3.Dot((pB - _cylinderOrigin), cylinderUp);
            if (Mathf.Abs(pADotC) > halfHeight && Mathf.Abs(pBDotC) > halfHeight)
                return 0;
            else if (Mathf.Abs(pADotC) > halfHeight)
            {
                _t0 = t1;
                return 1;
            }
            else if (Mathf.Abs(pBDotC) > halfHeight)
            {
                _t0 = t0;
                return 1;
            }

            if (t0 < t1)
            {
                _t0 = t0;
                _t1 = t1;
            }
            else
            {
                _t0 = t1;
                _t1 = t0;
            }

            return 2;
        }
    }

    public static bool CheckLineCircleIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _circleOrigin, Vector3 _circleNormal, float _radius, ref float _t)
    {
        Vector3 segmentDir = _lineDirection;
        float t = 0;
        if (CheckLinePlanIntersection(_linePoint, _lineDirection, _circleOrigin, _circleNormal, ref t))
        {
            Vector3 point = _linePoint + t * segmentDir;
            if ((point - _circleOrigin).sqrMagnitude <= _radius * _radius)
            {
                _t = t;
                return true;
            }
        }

        return false;
    }

    public static bool CheckLinePlanXZIntersection(Vector3 _linePoint, Vector3 _lineDirection, ref float _t)
    {
        float tDenominator = _lineDirection.y;
        float epsilon = Mathf.Epsilon;

        //Segment is coplanaire with plane
        if (Mathf.Abs(tDenominator) < epsilon)
            return false;

        float t = -_linePoint.y / tDenominator;

        return true;
    }

    public static int CheckLineCapsuleIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _cylinderOrigin, Vector3 cylinderUp, float _cylinderRadius, float _cylinderHeight, ref float _t0, ref float _t1)
    {

        float t0 = 0.0f;
        float t1 = 0.0f;

        int count = ParametricCollisionUtility.CheckLineCylinderWihtoutCapsIntersection(_linePoint, _lineDirection, _cylinderOrigin, cylinderUp, _cylinderRadius, _cylinderHeight, ref t0, ref t1);
        if (count == 2)
        {
            _t0 = t0;
            _t1 = t1;
            return 2;
        }

        float halfHeight = _cylinderHeight / 2.0f;
        float t2 = 0.0f, t3 = 0.0f, t4 = 0.0f, t5 = 0.0f;
        int capsCount = CheckLineSphereCapsIntersection(_linePoint, _lineDirection, _cylinderOrigin + cylinderUp * halfHeight, cylinderUp, _cylinderRadius, ref t2, ref t3);
        int caps2Count = CheckLineSphereCapsIntersection(_linePoint, _lineDirection, _cylinderOrigin - cylinderUp * halfHeight, -cylinderUp, _cylinderRadius, ref t4, ref t5);

        if (capsCount == 2)
        {
            _t0 = t2;
            _t1 = t3;
            return 2;
        }
        else if (caps2Count == 2)
        {
            _t0 = t4;
            _t1 = t5;
            return 2;
        }
        else if (capsCount == 1 && caps2Count == 1)
        {
            _t0 = Mathf.Min(t2, t4);
            _t1 = Mathf.Max(t2, t4);

            return 2;
        }
        else if (capsCount == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t2);
            _t1 = Mathf.Max(t0, t2);
            return 2;
        }
        else if (caps2Count == 1 && count == 1)
        {
            _t0 = Mathf.Min(t0, t4);
            _t1 = Mathf.Max(t0, t4);
            return 2;
        }
        else if (count == 1)
        {
            _t0 = t0;
            return 1;
        }
        else if (capsCount == 1)
        {
            _t0 = t2;
            return 1;
        }
        else if (caps2Count == 1)
        {
            _t0 = t4;
            return 1;
        }

        return 0;
    }


    public static int CheckLineSphereCapsIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _capsOrigin, Vector3 _capsNormal, float _capsRadius, ref float _t0, ref float _t1)
    {
        float t0 = 0.0f, t1 = 0.0f;
        int count = CheckLineSphereIntersection(_linePoint, _lineDirection, _capsOrigin, _capsRadius, ref t0, ref t1);

        if (count == 0)
            return 0;
        else if (count == 1)
        {
            Vector3 pA = _linePoint + t0 * _lineDirection;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);

            if (pADotC < 0.0f)
                return 0;
            else
                _t0 = t0;

            return 1;
        }
        else
        {
            Vector3 pA = _linePoint + t0 * _lineDirection;
            Vector3 pB = _linePoint + t1 * _lineDirection;

            float pADotC = Vector3.Dot((pA - _capsOrigin), _capsNormal);
            float pBDotC = Vector3.Dot((pB - _capsOrigin), _capsNormal);
            if (pADotC < 0.0f && pBDotC < 0.0f)
                return 0;
            else if (pADotC < 0.0f)
            {
                _t0 = t1;
                return 1;
            }
            else if (pBDotC < 0.0f)
            {
                _t0 = t0;
                return 1;
            }

            _t0 = Mathf.Min(t0, t1);
            _t1 = Mathf.Max(t0, t1);
            return 2;
        }
    }


    public static int CheckLineSphereIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 _SphereOrigin, float _radius, ref float _t0, ref float _t1)
    {
        Vector3 segmentDir = _lineDirection;
        Vector3 sphereDir = _linePoint - _SphereOrigin;
        float a = segmentDir.sqrMagnitude;

        if (a <= 0)
            return 0;

        float b = 2 * Vector3.Dot(segmentDir, sphereDir);
        float c = sphereDir.sqrMagnitude - _radius * _radius;

        if (c <= 0)
            return 0;

        float delta = b * b - 4.0f * a * c;
        float a2 = 2 * a;

        float epsilon = Mathf.Epsilon;
        if (delta < epsilon)
            return 0;

        if (delta < epsilon)
        {
            _t0 = -b / a2;
            return 1;
        }

        float deltaSqrt = Mathf.Sqrt(delta);
        float t0 = (-b - deltaSqrt) / a2;
        float t1 = (-b + deltaSqrt) / a2;

        if (t0 < t1)
        {
            _t0 = t0;
            _t1 = t1;
        }
        else
        {
            _t0 = t1;
            _t1 = t0;
        }
        return 2;
    }

    public static int CheckLineMinkowskiBoxSphereIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 boxCenter, Vector3 boxSize, Quaternion boxRotation, float sphereRadius, ref float _t0, ref float _t1)
    {
        Vector3 relatifPA = MathUtility.WorldBaseChangementOptimised(_linePoint, boxCenter, boxRotation, Vector3.one);
        Vector3 halSize = boxSize / 2.0f;
        Vector3 halSizeR = halSize + Vector3.one * sphereRadius;
        Vector3 segmentDir = MathUtility.WorldBaseChangementDirection(_lineDirection, boxRotation);


        //Left Quad Intersection
        float t = 0.0f;
        Vector3 quadPointA = halSizeR.x * Vector3.left + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t);

        //Right Quad Intersection
        float t1 = 0.0f;
        quadPointA = halSizeR.x * Vector3.right + halSize.y * Vector3.down + halSize.z * Vector3.back;
        bool intersection2 = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.forward * boxSize.z, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Bottom Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.down + halSize.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Top Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSizeR.y * Vector3.up + halSize.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.forward * boxSize.z, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Back Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.back;
        intersection2 = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }

        //Front Quad Intersection
        quadPointA = halSize.x * Vector3.left + halSize.y * Vector3.down + halSizeR.z * Vector3.forward;
        intersection2 = CheckLineQuadIntersection(relatifPA, segmentDir, quadPointA, quadPointA + Vector3.right * boxSize.x, quadPointA + Vector3.up * boxSize.y, ref t1);

        if (intersection && intersection2)
        {
            _t0 = Mathf.Min(t, t1);
            _t1 = Mathf.Max(t, t1);
            return 2;
        }
        else if (intersection2)
        {
            intersection = intersection2;
            t = t1;
            t1 = 0.0f;
        }


        float t0Tmp = 0.0f, t1Tmp = 0.0f;
        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.left, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.back, Vector3.up, boxSize.y, Vector3.right, Vector3.back))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.right, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.z * Vector3.forward, Vector3.up, boxSize.y, Vector3.left, Vector3.forward))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.back, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.down, Vector3.right, boxSize.x, Vector3.forward, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.right, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.down, Vector3.forward, boxSize.z, Vector3.left, Vector3.down))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.back + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.back, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.z * Vector3.forward + halSize.y * Vector3.up, Vector3.right, boxSize.x, Vector3.forward, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.right + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.right, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }

        if (CheckCapsule(halSize.x * Vector3.left + halSize.y * Vector3.up, Vector3.forward, boxSize.z, Vector3.left, Vector3.up))
        {
            _t0 = t0Tmp; _t1 = t1Tmp;
            return 2;
        }


        bool CheckCapsule(Vector3 capsuleOrigin, Vector3 capsuleNormal, float capsuleSize, Vector3 dir1, Vector3 dir2)
        {
            float t2 = 0.0f, t3 = 0.0f;
            int count = CheckLineCapsuleIntersection(relatifPA, segmentDir, capsuleOrigin, capsuleNormal, sphereRadius, capsuleSize, ref t2, ref t3);
            if (count > 0)
            {
                int countTMP = count;
                Vector3 resultPoint = relatifPA + t2 * segmentDir;
                if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                    Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                    count--;

                if (countTMP == 2)
                {
                    resultPoint = relatifPA + t3 * segmentDir;
                    if (Vector3.Dot(resultPoint - capsuleOrigin, dir1) < 0.0f ||
                        Vector3.Dot(resultPoint - capsuleOrigin, dir2) < 0.0f)
                        count--;
                    else if (count == 1)
                        t2 = t3;
                }
            }

            if (count == 2)
            {
                t0Tmp = Mathf.Min(t2, t3);
                t1Tmp = Mathf.Max(t2, t3);
                return true;
            }
            else if (count == 1 && intersection)
            {
                t0Tmp = Mathf.Min(t, t2);
                t1Tmp = Mathf.Max(t, t2);
                return true;
            }
            else if (count == 1)
            {
                intersection = true;
                t = t2;
            }

            return false;
        }

        if (intersection)
        {
            _t0 = t;
            return 1;
        }
        return 0;
    }


    public static bool CheckLineQuadIntersection(Vector3 _linePoint, Vector3 _lineDirection, Vector3 quadPointA, Vector3 quadPointB, Vector3 quadPointC, ref float _t0)
    {
        Vector3 rightDir = quadPointB - quadPointA;
        Vector3 upDir = quadPointC - quadPointA;
        float rightLength = rightDir.magnitude;
        float upLength = upDir.magnitude;

        Vector3 nRightDir = rightDir / rightLength;
        Vector3 nUpDir = upDir / upLength;
        Vector3 planeNormal = Vector3.Cross(nUpDir, nRightDir);

        Quaternion rotation = Quaternion.LookRotation(upDir, planeNormal);
        _linePoint = MathUtility.WorldBaseChangementOptimised(_linePoint, quadPointA, rotation, Vector3.one);
        _lineDirection = MathUtility.WorldBaseChangementDirection(_lineDirection, rotation);

        float t = 0.0f;
        if (CheckRayPlanXZIntersection(_linePoint, _lineDirection, ref t))
        {
            Vector3 planResultPoint = _linePoint + t * _lineDirection;
            if (planResultPoint.x > 0 && planResultPoint.x < rightLength &&
                planResultPoint.z > 0 && planResultPoint.z < upLength)
            {
                _t0 = t;
                return true;
            }
        }

        return false;
    }

}
