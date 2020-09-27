using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class StarController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = null;
    [SerializeField] private int _numberOfObjects = 20000;
    [SerializeField] private int _batchSize = 100;
    [SerializeField] private float _min = -50f;
    [SerializeField] private float _max = 50f;

    private NativeArray<float3> _positions;
    private TransformAccessArray _transforms;

    private JobHandle _moveJob;
    private JobHandle _updateTransformJob;
    
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
        if (_moveJob.IsCompleted)
        {
            _moveJob.Complete();
            _moveJob = MoveJob.Begin(_positions, _batchSize);
        }

        _updateTransformJob = UpdateTransformsJob.Begin(_transforms, _positions, _moveJob);
    }

    void LateUpdate()
    {
        _updateTransformJob.Complete();
    }

    private void OnDestroy()
    {
        JobHandle.ScheduleBatchedJobs();
        JobHandle.CompleteAll(ref _moveJob, ref _updateTransformJob);
        _positions.Dispose();
        _transforms.Dispose();
    }

    private void Allocate()
    {
        _positions = new NativeArray<float3>(_numberOfObjects, Allocator.Persistent);
        _transforms = new TransformAccessArray(_numberOfObjects);
    }

    private void CreateGameObjects()
    {
        for (int i = 0; i < _numberOfObjects; i++)
        {
            GameObject gameObject = Instantiate(_prefab, Util.GetNext(_min, _max), Quaternion.identity);
            _positions[i] = gameObject.transform.position;
            _transforms.Add(gameObject.transform);
        }
    }
}