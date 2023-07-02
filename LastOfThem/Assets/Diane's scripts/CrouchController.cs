//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

//public class NewBehaviourScript : MonoBehaviour
//{
//    [SerializeField] private float crouchHeight = 0.5f;
//    [SerializeField] private float crouchSpeed = 5f;
//    [SerializeField] private Transform cameraTransform;
//    [SerializeField] private FirstPersonController firstPersonController;

//    private CharacterController characterController;
//    private Vector3 originalPosition;
//    private float originalHeight;
//    private bool isCrouching;

//    private void Start()
//    {
//        characterController = GetComponent<CharacterController>();
//        originalPosition = cameraTransform.localPosition;
//        originalHeight = characterController.height;
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            ToggleCrouch();
//        }
//    }

//    private void ToggleCrouch()
//    {
//        if (isCrouching)
//        {
//            characterController.height = originalHeight;
//            cameraTransform.localPosition = originalPosition;
//            firstPersonController.ResetSpeed();
//        }
//        else
//        {
//            characterController.height = crouchHeight;
//            cameraTransform.localPosition = new Vector3(
//                originalPosition.x,
//                originalPosition.y - (originalHeight - crouchHeight),
//                originalPosition.z
//            );
//            firstPersonController.SetCrouchSpeed(crouchSpeed);
//        }

//        isCrouching = !isCrouching;

//        // Additional actions when crouch state changes
//        // For example, adjusting movement speed, enabling/disabling colliders, etc.
//    }
//}
