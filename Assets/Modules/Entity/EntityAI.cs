using UnityEngine;

[CreateAssetMenu(fileName = "EntityAI", menuName = "Familia/EntityAI", order = 0)]
public class EntityAI : ScriptableObject
{
    [SerializeField] private Transform transform;
    [SerializeField] private GameObject _target;
    [SerializeField] private CircleCollider2D _circleRange;
    [SerializeField] private float _speed;

    private float _startTime;
    public void MoveUnit(Vector2 vector2)
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        transform.rotation.SetLookRotation(_target.transform.position, Vector3.down);
    }
    public void SetTarget(GameObject gameObject)
    {
        _target = gameObject;
    }
}