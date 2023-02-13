using System.Numerics;
using Raylib_cs;

public class Ball
{
    private static Random randomGen = new();


    public Vector2 position;
    public Vector2 speedVector;
    public int Radius { get; private set; } = 5;

    private bool paused = true;

    Vector2 screenDim;

    public Ball(Vector2 screenSize)
    {
        screenDim = screenSize;
        Spawn();
    }

    private void Spawn()
    {
        position = screenDim / 2;

        int dir = randomGen.Next(9) <= 4 ? -1 : 1;
        speedVector = Vector2.Normalize(new Vector2(dir, 1)) * 1;
    }

    public void Move()
    {
        if (paused) return;
        position += speedVector;
    }

    public void Render()
    {
        Raylib.DrawCircle((int)position.X, (int)position.Y, Radius, Color.BLACK);
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    public void Bounds(ref (int, int) scores)
    {
        if (position.Y + Radius > Raylib.GetScreenHeight()) speedVector *= new Vector2(1, -1);
        if (position.Y - Radius < 0) speedVector *= new Vector2(1, -1);

        if (position.X < 0)
        {
            Spawn();
            scores.Item1++;
        }
        if (position.X > Raylib.GetScreenWidth())
        {
            Spawn();
            scores.Item2++;
        }
    }
}
