using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private const string Idle = "Idle";
    private const string Move = "Move";

    protected Vector2 TargetVelocity;
    protected bool Grounded;
    protected Vector2 GroundNormal;
    protected Rigidbody2D Rb2d;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);

    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;

    private void OnEnable()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _animator.Play(Idle);
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(_layerMask);
        ContactFilter.useLayerMask = true;
    }

    private void Update()
    {
        TargetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (_velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
            _animator.SetBool(Move, true);
        }
        else if (_velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
            _animator.SetBool(Move, true);
        }
        else
        {
            _animator.SetBool(Move, false);
        }

        if (Input.GetKey(KeyCode.Space) && Grounded)
            _velocity.y = 5;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = TargetVelocity.x;

        Grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime * _speed;
        Vector2 moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movements(move, false);

        move = Vector2.up * deltaPosition.y;

        Movements(move, true);
    }

    private void Movements(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = Rb2d.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);

            HitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                Vector2 currentNormal = HitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    Grounded = true;
                    if (yMovement)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = HitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        Rb2d.position = Rb2d.position + move.normalized * distance;
    }
}