using UnityEngine;

public class Marble : MonoBehaviour
{
    public float mass = 1f;
    public float radius = 0.5f;
    public Vector3 velocity;

    private Vector3 gravity = new Vector3(0, -9.8f, 0);
    private Vector3 frictionForce;

    private void Update()
    {
        ApplyGravity();
        MoveMarble();
    }


    private void ApplyGravity()
    {
        velocity += gravity * Time.deltaTime;
    }


    private void MoveMarble()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void ApplyFriction(Vector3 contactNormal)
    {
        float frictionCoefficient = 0.5f;
        frictionForce = -frictionCoefficient * velocity.magnitude * contactNormal.normalized;
        velocity += frictionForce * Time.deltaTime;
    }

    public void HandleMarbleCollision(Marble other)
    {
        Vector3 direction = other.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance < radius + other.radius)
        {
            Vector3 normal = direction.normalized;
            Vector3 relativeVelocity = velocity - other.velocity;
            float velocityAlongNormal = Vector3.Dot(relativeVelocity, normal);
            if (velocityAlongNormal > 0) return;
            float impulse = (0.1f * velocityAlongNormal) / (mass + other.mass);
            velocity -= impulse * other.mass * normal;
            other.velocity += impulse * mass * normal;
            float overlap = (radius + other.radius) - distance;
            Vector3 separation = normal * overlap * 0.1f;
            transform.position -= separation;
            other.transform.position += separation;
        }
    }

}