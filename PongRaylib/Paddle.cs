using System.Numerics;
using Raylib_cs;

public class Paddle
{
    public static Vector2 Size { get; private set; } = new(5, 50); //pixels
    public Vector2 position;

    public Rectangle PaddleRectangle
    {
        get
        {
            return new Rectangle(position.X, position.Y, Size.X, Size.Y);
        }
    }

    public enum Direction
    {
        Up = -1,
        Down = 1,
    }

    public Paddle(int xPosition)
    {
        position = new(xPosition, 375);
    }

    public void Render()
    {
        Raylib.DrawRectangleRec(PaddleRectangle, Color.BLACK);
    }

    public void Move(Direction dir)
    {
        position.Y += (int)dir;

        if (position.Y < 0) position.Y = 0;
        else if (position.Y + Size.Y > Raylib.GetScreenHeight()) position.Y = Raylib.GetScreenHeight() - Size.Y;
    }

    public void Collide(ref Ball ball)
    {
        // I have made this complicated so it bounces depending on where you hit it

        float ballX = ball.position.X;

        if (ballX < Raylib.GetScreenWidth() / 2)
        {
            // I do not want it to bounce on the top / bottom of a paddle
            if (ballX < position.X) return;

            if (Raylib.CheckCollisionCircleRec(ball.position, ball.Radius, PaddleRectangle))
            {
                // Max distance (in Y) is Size.Y
                float collisionOnPaddleY = ball.position.Y - position.Y;

                // Calculate the position of collision to a "weight" float
                // The float puts weight on how much up - down the shot bounces
                float lerpWeight = Raymath.Remap(collisionOnPaddleY, 0, Size.Y, 0.25f, 0.75f);

                // Lerp
                ball.speedVector = Raymath.Vector2Lerp(new(1, -3), new(1, 3), lerpWeight);
            }
        }

        // Exact same as above, just with the other side
        if (ballX > Raylib.GetScreenWidth() / 2)
        {
            if (ballX > position.X) return;

            if (Raylib.CheckCollisionCircleRec(ball.position, ball.Radius, PaddleRectangle))
            {
                // Max distance (in Y) is Size.Y
                float collisionOnPaddleY = ball.position.Y - position.Y;

                // Calculate the position of collision to a "weight" float
                // The float puts weight on how much up - down the shot bounces
                float lerpWeight = Raymath.Remap(collisionOnPaddleY, 0, Size.Y, 0.25f, 0.75f);

                // Lerp
                ball.speedVector = Raymath.Vector2Lerp(new(-1, -3), new(-1, 3), lerpWeight);
            }
        }
    }
}
