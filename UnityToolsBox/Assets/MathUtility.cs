using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtility
{
    public static Vector3 BaseChangement(Vector3 _point, Matrix4x4 baseA, Matrix4x4 baseB)
    {
        Vector3 worldPoint = baseA * _point;
        Vector3 baseChangement = baseB.inverse * worldPoint;

        return baseChangement;
    }

    public static Vector3 BaseChangementOptimised(Vector3 _point, Vector3 _baseATranslation, Quaternion _baseARotation, Vector3 baseAScale
                                                 , Vector3 _baseBTranslation, Quaternion _baseBRotation, Vector3 baseBScale)
    {

        Vector3 _pointScale = _point;
        _pointScale.Scale(baseAScale);
        Vector3 worldPoint = _baseARotation * _pointScale + _baseATranslation;

        Vector3 baseBInverteScale = new Vector3(1.0f / baseBScale.x, 1.0f / baseBScale.y, 1.0f / baseBScale.z);
        Vector3 newPoint = Quaternion.Inverse(_baseBRotation) * (worldPoint - _baseBTranslation);
        newPoint.Scale(baseBInverteScale);

        return newPoint;
    }

    public static Vector3 WorldBaseChangementOptimised(Vector3 _point, Vector3 _targetBaseTranslation, Quaternion _targetBaseRotation, Vector3 _targetBaseScale)
    {
        Vector3 baseBInverteScale = new Vector3(1.0f / _targetBaseScale.x, 1.0f / _targetBaseScale.y, 1.0f / _targetBaseScale.z);
        Vector3 newPoint = Quaternion.Inverse(_targetBaseRotation) * (_point - _targetBaseTranslation);
        newPoint.Scale(baseBInverteScale);

        return newPoint;
    }

    public static Vector3 WorldBaseChangementDirection(Vector3 _direction, Quaternion _targetBaseRotation)
    {
        Vector3 newDirection = Quaternion.Inverse(_targetBaseRotation) * _direction;

        return newDirection;
    }

    public static Vector3 BaseToWorld(Vector3 _point, Vector3 _baseTranslation, Quaternion _baseRotation, Vector3 _baseScale)
    {
        Vector3 _pointScale = _point;
        _pointScale.Scale(_baseScale);
        Vector3 worldPoint = _baseRotation * _pointScale + _baseTranslation;
        return worldPoint;
    }


}
