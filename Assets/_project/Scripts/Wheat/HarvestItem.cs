using UnityEngine;

public class HarvestItem : MonoBehaviour
{
    public CropData Data { get; private set; }

    [field: SerializeField] public Rigidbody RB { get; private set; }

    [SerializeField] private float _initialSpeed = 5f;

    private ObjectPool _pool;
    public void Init(CropData data, ObjectPool pool)
    {
        Data = data;
        _pool = pool;
    }

    public void Drop()
    {
        RB.AddForce(Vector3.up * _initialSpeed + new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f)), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            var inventory = player.Inventory;
            if (inventory.AddItemToInventory())
            {
                RB.GetComponent<Collider>().isTrigger = true;
                inventory.FlyObjectInStack(this);
            }
        }
    }
    public void Return()
    {
        RB.isKinematic = false;
        RB.useGravity = true;
        transform.localScale = Vector3.one;
        _pool.ReturnObject(gameObject);
    }
}
