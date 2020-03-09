using UnityEngine;
using QRTools.Utilities.Tween;

public class TestTween : MonoBehaviour
{
    public TweenSequenceVector3 myTweenVect3;
    public TweenSequenceQuaternion myTweenQuat;

    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(myTweenVect3.LerpValueWithAnimationCurve(
                pos,
                (x) => pos = x
                ));

            StartCoroutine(myTweenQuat.LerpValueWithAnimationCurve(
                rot,
                (x) => rot = x
                ));

            StartCoroutine(myTweenVect3.LerpValueWithAnimationCurve(
                scale,
                (x) => scale = x
                ));
        }
            

        GetComponent<Transform>().position = pos;
        GetComponent<Transform>().rotation = rot;
        GetComponent<Transform>().localScale = scale;
    }
}
