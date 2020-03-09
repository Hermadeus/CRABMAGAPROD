using QRTools.Variables;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HighHopes
{
    public class IK_Handling : MonoBehaviour
    {
        public Animator anim;

        Vector3 leftFootPos;
        Vector3 rightFootPos;

        Quaternion leftFootRot;
        Quaternion rightFootRot;

        [BoxGroup("Foot")]
        public BoolVariable isIK_Foot;
        float leftFootWeight;
        float rigthFootWeight;

        Transform leftFoot;
        Transform rightFoot;

        [BoxGroup("Foot")]
        public float offsetY = 0.12f;
        [BoxGroup("Foot")]
        public LayerMask stairsMask;

        [BoxGroup("Eyes")]
        public BoolVariable isIK_LookAtWeight;
        [BoxGroup("Eyes")][Range(0,1)]
        public float lookIKWeight = .5f;
        [BoxGroup("Eyes")][Range(0,1)]
        public float eyesWeight = .5f;
        [BoxGroup("Eyes")] [Range(0, 1)]
        public float bodyWeight = .5f;
        [BoxGroup("Eyes")] [Range(0, 1)]
        public float headWeight = .5f;
        [BoxGroup("Eyes")] [Range(0, 1)]
        public float clampWeight = .5f;
        [BoxGroup("Eyes")]
        public Transform lookPos;

        [BoxGroup("Hand")]
        public BoolVariable isIK_Hand;
        [BoxGroup("Hand")]
        public Vector3 rightHandPos;
        [BoxGroup("Hand")] [Range(0,1)]
        public float rightHandWeight;
        [BoxGroup("Hand")]
        public Transform pointer;
        [BoxGroup("Hand")]
        public float offSetRight;
        [BoxGroup("Hand")]
        public float offSetLeft;
        [BoxGroup("Hand")]
        public Transform leftIKTarget;
        [BoxGroup("Hand")]
        public Transform rightIKTarget;
                
        public bool isActive = true;

        public Camera cam;

        private void Start()
        {
            anim = GetComponent<Animator>();

            leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

            leftFootRot = leftFoot.rotation;
            rightFootRot = rightFoot.rotation;
        }

        private void FixedUpdate()
        {
            RaycastHit leftHit;
            RaycastHit rightHit;

            RaycastHit rightHand;
            RaycastHit leftHand;

            Vector3 leftPos = leftFoot.TransformPoint(Vector3.zero);
            Vector3 rightPos = rightFoot.TransformPoint(Vector3.zero);

            if(Physics.Raycast(leftPos, -Vector3.up, out leftHit, 1))
            {
                leftFootPos = leftHit.point;
                leftFootRot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
            }    

            if (Physics.Raycast(rightPos, -Vector3.up, out rightHit, 1, stairsMask))
            {
                rightFootPos = rightHit.point;
                rightFootRot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
            }

            if(isIK_LookAtWeight.Value == false)
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                lookPos.position = ray.GetPoint(15);
            }

            if (Physics.Raycast(pointer.position, pointer.right, out rightHand, 1))
            {
                rightIKTarget.position = rightHand.point;
                rightIKTarget.rotation = Quaternion.FromToRotation(transform.up, rightHand.normal) * transform.rotation;
            }

            if (Physics.Raycast(pointer.position, pointer.right, out leftHand, 1))
            {
                leftIKTarget.position = leftHand.point;
                leftIKTarget.rotation = Quaternion.FromToRotation(transform.up, leftHand.normal) * transform.rotation;
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!isActive)
                return;
                        
            anim.SetLookAtWeight(lookIKWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
            anim.SetLookAtPosition(lookPos.position);

            if (isIK_Foot.Value == true)
            {
                //FOOT
                leftFootWeight = anim.GetFloat("IK_LeftFoot");
                rigthFootWeight = anim.GetFloat("IK_RightFoot");

                //Position
                anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
                anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rigthFootWeight);

                anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos + new Vector3(0, offsetY, 0));
                anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos + new Vector3(0, offsetY, 0));

                //Rotation
                anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rigthFootWeight);

                anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
            }

            if (isIK_Hand.Value == true)
            {
                //HAND
                float leftHandWeight = anim.GetFloat("IK_LeftHand");
                float rightHandWeight = anim.GetFloat("IK_RightHand");

                //Position
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);

                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTarget.position + new Vector3(0, offSetLeft, 0));
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTarget.position + new Vector3(0, offSetRight, 0));

                //Rotation
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);

                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIKTarget.rotation);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightIKTarget.rotation);
            }
        }
    }
}
