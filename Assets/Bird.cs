using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;
    private const float PositiveSceneEdgeLimit = 10;
    private const float NegativeSceneEdgeLimit = -20;
    private const float IddleTimeLimit = 3;

    [SerializeField] private float _launchPower = 500;


    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {

        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);        

        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if(isOffLimit(transform.position.y) || isOffLimit(transform.position.x)            
            || _timeSittingAround > IddleTimeLimit)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }        
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    private bool isOffLimit(float axisPosition)
    {
        return axisPosition > PositiveSceneEdgeLimit || axisPosition < NegativeSceneEdgeLimit;
    }
}
