using UnityEngine;

public class FluidPipe : MonoBehaviour
{
    [SerializeField]
    private Transform _origin = null;

    [SerializeField]
    private GameObject _streamPrefab = null;

    [SerializeField]
    private bool _desiresPour = false;

    private bool _isPouring = false;

    private Stream _currentStream = null;

    private Color _activeColor;


    private void Update()
    {

        if (_isPouring != _desiresPour)
        {
            _isPouring = _desiresPour;

            if (_isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }


    }

    private void StartPour()
    {
        _currentStream = CreateStream();
        _currentStream.Begin();
    }

    private void EndPour()
    {
        _currentStream.End();
        _currentStream = null;
    }

    private Stream CreateStream()
    {
        Stream stream = Instantiate(_streamPrefab, _origin.position, Quaternion.identity, transform).GetComponent<Stream>();
        stream.SetStreamColor(_activeColor);
        return stream;
    }

    public bool GetDesiresPour()
    {
        return _desiresPour;
    }

    public void SetDesiresPour(bool desiresPour)
    {
        _desiresPour = desiresPour;
    }

    public void SetColorRGB(Color color)
    {
        _activeColor = color;
    }
}
