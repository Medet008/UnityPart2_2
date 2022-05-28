
using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private bool _ikActive;

    [SerializeField]
    private Transform _pointRightHand ;

    [SerializeField]
    private Transform _pointLook;

    [SerializeField]
    private LayerMask _rayLayer;    

    [SerializeField]
    private Transform _footTransform;

    [SerializeField]
    private Vector3 _offsetFootL;

    [SerializeField]
    private Vector3 _offsetFootR;

    [SerializeField]
    private float _valueWeight;

    

    private static readonly int Leftleg = Animator.StringToHash("Left_leg");
    private static readonly int Rightleg = Animator.StringToHash("Right_leg");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
      
        if (_ikActive)
        {
            _animator.SetLookAtWeight(1);
            _animator.SetLookAtPosition(_pointLook.position);

            ChangeWeightAvatar(AvatarIKGoal.RightHand, 1);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _pointRightHand.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _pointRightHand.rotation);

            LegsIk();
        }
        else
        {
            ChangeWeightAvatar(AvatarIKGoal.RightHand, 0);
            _animator.SetLookAtWeight(0);
        }
    }

    private void LegsIk()
    {
        var weightFootL = _animator.GetFloat(Leftleg);
        ChangeWeightAvatar(AvatarIKGoal.LeftFoot, weightFootL);
        var weightFootR = _animator.GetFloat(Rightleg);
        ChangeWeightAvatar(AvatarIKGoal.RightFoot, weightFootR);

        ChangePositionLeg(AvatarIKGoal.LeftFoot, _offsetFootL);
        ChangePositionLeg(AvatarIKGoal.RightFoot, _offsetFootR);
    }

    private void ChangePositionLeg(AvatarIKGoal avatarIKGoal, Vector3 offset)
    {
        var footPos = _animator.GetIKPosition(avatarIKGoal); 
        if(Physics.Raycast(footPos + Vector3.up, Vector3.down, out var hit, 2.0f,  _rayLayer))
        {
            _animator.SetIKPosition(avatarIKGoal, hit.point + offset);
            _animator.SetIKRotation(avatarIKGoal, Quaternion.LookRotation(Vector3.ProjectOnPlane(_footTransform.forward, hit.normal), hit.normal));
        }
    }

    private void ChangeWeightAvatar(AvatarIKGoal avatarIKGoal, float value)
    {
        _animator.SetIKPositionWeight(avatarIKGoal, value);
        _animator.SetIKRotationWeight(avatarIKGoal, value);
    }
}
