using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class TrackGeneration : MonoBehaviour, IResetable
{
    public List<GameObject> TrackElements;
    public int EmptyElementIndex = 0;
    public int EasyElementsMaxIndex;
    public int MediumElementsMaxIndex;
    public int HardElementsMaxIndex;

    [Space] public DifficultyParameters DiffParams;

    [Space]
    public Transform PositionCheckTransform;
    public Axis PositionCheckAxis;
    public bool CheckIfGreater;

    private List<SnapPoints> _trackElements;
    private int _lastEnqueued;
    private Queue<int> _activeElements;


    public void Start()
    {
        _trackElements = new List<SnapPoints>(TrackElements.Count);
        _activeElements = new Queue<int>(TrackElements.Count);

        for (int i = 0; i < TrackElements.Count; ++i)
        {
            _trackElements.Add(TrackElements[i].GetComponent<SnapPoints>());
        }

        Reset();
    }

    void Update()
    {
        updateTrack();
    }

    public void Reset()
    {
        if(_trackElements == null) Start();

        _activeElements.Clear();
        for (int i = 0; i < TrackElements.Count; i++)
        {
            TrackElements[i].gameObject.SetActive(false);
        }
        generateInitialTrack();
    }

    void generateInitialTrack()
    {
        activateTrackElement(EmptyElementIndex, transform.position);

        while (_activeElements.Count < 3)
        {
            activateNewTrackElement();
        }
    }

    void activateNewTrackElement()
    {
        int rand_track = getRandomTrackElement();
        if (!_activeElements.Contains(rand_track))
        {
            activateTrackElement(rand_track, _trackElements[_lastEnqueued].EndSnapPoint.position);
        }
    }

    int getRandomTrackElement()
    {
        float diff_rand = Random.value;
        if (diff_rand < DiffParams.ProbabilityEasy)
        {
            return (int) Random.Range(0.0f, EasyElementsMaxIndex + 0.9999f);
        }
        else if (diff_rand < DiffParams.ProbabilityEasy + DiffParams.ProbabilityMedium)
        {
            return (int) Random.Range(EasyElementsMaxIndex + 0.0001f, MediumElementsMaxIndex + 0.9999f);
        }
        else
        {
            return (int)Random.Range(MediumElementsMaxIndex + 0.0001f, HardElementsMaxIndex + 0.9999f);
        }
    }

    void activateTrackElement(int index, Vector3 position)
    {
        _trackElements[index].gameObject.SetActive(true);
        _trackElements[index].StartSnapPoint.position = position;
        _activeElements.Enqueue(index);
        _lastEnqueued = index;
    }

    void deactivateTrackElement()
    {
        _trackElements[_activeElements.Dequeue()].gameObject.SetActive(false);
    }

    void updateTrack()
    {
        if (deactivateElementCondition(_trackElements[_activeElements.Peek()]))
        {
            deactivateTrackElement();
            while (_activeElements.Count < 3)
            {
                activateNewTrackElement();
            }
        }
    }

    private bool deactivateElementCondition(SnapPoints sp)
    {
        Vector3 currSegStartPos = sp.EndSnapPoint.position;
        Vector3 transfPos = PositionCheckTransform.InverseTransformPoint(currSegStartPos);

        switch (PositionCheckAxis)
        {
            case Axis.None:
                return transfPos == Vector3.zero;
                break;
            case Axis.X:
                return transfPos.x > 0 == CheckIfGreater;
                break;
            case Axis.Y:
                return transfPos.y > 0 == CheckIfGreater;
                break;
            case Axis.Z:
                return transfPos.z > 0 == CheckIfGreater;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
