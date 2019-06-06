using Hel.Extensions;
using Hel.Inputs;
using UnityEngine;

namespace Hel.Player
{
    /// <summary>
    /// Used to handle player movement.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private float speedSmoothTime = 0.1f;
        [SerializeField] private float targetDistance = 5f;

        private CharacterController characterController = null;
        private Animator animator = null;
        private Transform mainCameraTransform = null;

        private Vector3 currentImpact = new Vector3();
        private Vector3 targetPosition = new Vector3();
        private Transform chestTransform = null;
        private float velocityY = 0f;
        private float speedSmoothVelocity = 0f;
        private float currentSpeed = 0f;
        private float deltaTime = 0f;

        private readonly float gravity = Physics.gravity.y;

        private static readonly int hashSpeedPercentage = Animator.StringToHash("SpeedPercentage");
        private static readonly int hashForwardMovement = Animator.StringToHash("ForwardMovement");
        private static readonly int hashRightMovement = Animator.StringToHash("RightMovement");
        private static readonly int hashIsAiming = Animator.StringToHash("IsAiming");
        private static readonly int hashIsAttacking = Animator.StringToHash("IsAttacking");

        public Vector3 AimAtOffset { get; set; }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            mainCameraTransform = Camera.main.transform;
            chestTransform = animator.GetBoneTransform(HumanBodyBones.Spine);

            SceneLinkedSMB<PlayerMovementController>.Initialise(animator, this);
        }

        private void OnAnimatorMove()
        {
            if (animator.GetBool(hashIsAttacking))
            {
                characterController.Move(animator.deltaPosition);
            }
        }

        private void Update()
        {
            HandleExternalForces();
        }

        private void LateUpdate()
        {
            RotateChestToAim();
        }

        private void RotateChestToAim()
        {
            if (AimAtOffset == Vector3.zero) { return; }

            chestTransform.LookAt(targetPosition);
            chestTransform.rotation *= Quaternion.Euler(AimAtOffset);
        }

        public void Move()
        {
            Vector2 movementInput = PlayerInputManager.MovementInput;
            Vector2 movementDirection = movementInput.normalized;

            Vector3 forward = mainCameraTransform.forward;
            Vector3 right = mainCameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

            if (desiredMoveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            }

            float targetSpeed = movementSpeed * movementDirection.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            characterController.Move(desiredMoveDirection * targetSpeed * deltaTime);

            animator.SetFloat(hashSpeedPercentage, 0.5f * movementDirection.magnitude, speedSmoothTime, deltaTime);
            animator.SetFloat(hashForwardMovement, movementInput.y, speedSmoothTime, deltaTime);
            animator.SetFloat(hashRightMovement, movementInput.x, speedSmoothTime, deltaTime);
        }

        public void FacePlayerForward()
        {
            Vector3 lookRotation = targetPosition - transform.position;
            lookRotation.y = 0;
            transform.rotation = Quaternion.LookRotation(lookRotation);
        }

        private void HandleExternalForces()
        {
            deltaTime = Time.deltaTime;

            if (characterController.isGrounded && velocityY < 0f) { velocityY = 0f; }

            velocityY += gravity * deltaTime;

            Vector3 velocity = Vector3.up * velocityY;

            if (currentImpact.magnitude > 0.2f) { velocity += currentImpact; }

            characterController.Move(velocity * deltaTime);

            currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, 5f * deltaTime);

            targetPosition = mainCameraTransform.position + mainCameraTransform.forward * targetDistance;
        }
    }
}
