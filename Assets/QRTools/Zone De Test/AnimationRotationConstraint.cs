using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Functions;
using System;
using QRTools.Inputs;

public class AnimationRotationConstraint : MonoBehaviour
{
    public float constraint = 90f;
    public Axis axe;

    public float speed;

    public AxisInput axeInput;

    Transform t;
    float _angle;
    public float angle
    {
        get
        {
            return _angle;
        }
        set
        {
            _angle = value;
            switch (axe)
            {
                case Axis.X:
                    t.eulerAngles = new Vector3(value, 0, 0);
                    break;
                case Axis.Y:
                    t.eulerAngles = new Vector3(0, value, 0);
                    break;
                case Axis.Z:
                    t.eulerAngles = new Vector3(0, 0, value);
                    break;
            }
        }
    }

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    private void Update()
    {
        SetAngle(v(), Axis.Y, axeInput.ReturnValue());
    }

    Vector3 v()
    {
        if (axeInput.ReturnValue() != 0)
            return new Vector3(0, constraint * axeInput.ReturnValue(), 0);
        else
            return new Vector3(0, 0, 0);
    }

    public void SetAngle(Vector3 angle, Axis axe, float input)
    {
        switch (axe)
        {
            case Axis.X:
                this.angle = Mathf.Lerp(this.angle, angle.x, Time.deltaTime * speed);
                break;
            case Axis.Y:
                this.angle = Mathf.Lerp(this.angle, angle.y, Time.deltaTime * speed);
                break;
            case Axis.Z:
                this.angle = Mathf.Lerp(this.angle, angle.z, Time.deltaTime * speed);
                break;
        }
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
