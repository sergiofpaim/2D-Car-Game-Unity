using Assets.Logic.Game;
using UnityEngine;

public class CarCollisionReader : MonoBehaviour
{
    public Collisions Collisions;
    private CarWritter carWritter;

    void Start()
    {
        Collisions = new();

        carWritter = GetComponent<CarWritter>();

        GameGoal.RegisterCollidable(carWritter.Name, Collisions);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var gameController = GameObject.Find("Main Camera").gameObject.GetComponent<GameController>();

        if (collision.collider.gameObject.tag == collision.otherCollider.gameObject.tag)
        {
            if (collision.collider.GetType() == typeof(BoxCollider2D) && collision.otherCollider.GetType() == typeof(CircleCollider2D))
                Collisions.OntoMoving = true;
            else if (collision.collider.GetType() == typeof(CircleCollider2D) && collision.otherCollider.GetType() == typeof(BoxCollider2D))
                Collisions.By = true;
            else
            {
                Collisions.By = true;
                Collisions.OntoMoving = true;
            }
        }
        else
            Collisions.OntoStatic = true;

        GameGoal.EvaluateCollisions(Collisions, carWritter.Name);
        carWritter.Explode();
        gameController.ShowEndgame();
    }
}