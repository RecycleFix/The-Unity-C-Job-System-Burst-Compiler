using UnityEngine;

public class MonoBehaviorStarController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = null;
    [SerializeField] private int _numberOfObjects = 20000;
    [SerializeField] private float _min = -50f;
    [SerializeField] private float _max = 50f;
    
    private Transform[] _transforms;
    
    void Start()
    {
        if (_prefab == null)
        {
            Debug.LogError("Prefab is null");
        }
        else
        {
            Allocate();
            CreateGameObjects();
        }
    }
    
    void Update()
    {
        float delta = Time.deltaTime;

        foreach (Transform @transform in _transforms)
        {
            @transform.position = new Vector3(@transform.position.x + delta, @transform.position.y + delta, @transform.position.z + delta);
        }
    }

    private void Allocate()
    {
        _transforms = new Transform[_numberOfObjects];
    }
    
    private void CreateGameObjects()
    {
        for (int i = 0; i < _numberOfObjects; i++)
        {
            GameObject gameObject = Instantiate(_prefab, new Vector3(Random.Range(_min, _max), Random.Range(_min, _max), Random.Range(_min, _max)), Quaternion.identity);
            _transforms[i] = gameObject.transform;
        }
    }
}