using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
      
    public float Speed
    {
        get { return speed; }
        private set { speed = value; }
    }
}
