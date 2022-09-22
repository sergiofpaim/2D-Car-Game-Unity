using Assets.Logic.CarLocation;
using UnityEngine;

public class CarWritter : MonoBehaviour
{
    public string Id;
    public string Name;

    public CarLocation Location;

    [SerializeField] public AudioSource engineSound;

    public Sprite ExplosionSprite;

    private CarWritter car;

    private readonly float WHEEL_BASE_IN_METERS = 1F;
    private const float METER_TO_PIXEL = 0.7F;

    void Awake()
    {
        Location = new(Id, WHEEL_BASE_IN_METERS, LocationFrom(transform.position, transform.rotation));

        car = GetComponent<CarWritter>();
        engineSound.Play();
    }

    void Update()
    {
        var next = Location.NextPosition();

        if (next is not null)
        {
            transform.position = PositionFrom(next);
            transform.rotation = RotationFrom(next);
        }

        if (Name == "blue")
            engineSound.panStereo = 1.0f;
        if (Name == "green")
            engineSound.panStereo = -1.0f;

        engineSound.pitch = car.Location.PowerTrain.RPM / 3000;
    }

    public void Explode()
    {
        var collision = GetComponent<CarCollisionReader>();

        if (collision.Collisions.By || collision.Collisions.OntoStatic)
        {
            GetComponent<SpriteRenderer>().sprite = ExplosionSprite;

            engineSound.Stop();
        }
    }

    private Location LocationFrom(Vector3 position, Quaternion rotation)
    {
        return new(position.x / METER_TO_PIXEL, position.y / METER_TO_PIXEL, rotation.z + 90);
    }

    private Quaternion RotationFrom(Location next)
    {
        return Quaternion.Euler(0, 0, -90 + next.DirectionInDegrees);
    }

    private Vector3 PositionFrom(Location next)
    {
        return new(next.Point.X * METER_TO_PIXEL, next.Point.Y * METER_TO_PIXEL);
    }
}
