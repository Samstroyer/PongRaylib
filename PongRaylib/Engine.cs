using System.Numerics;
using Raylib_cs;

public class Engine
{
    (int player1Score, int player2Score) scores = new(0, 0);
    private Vector2 screenDim;

    Paddle player1;
    Paddle player2;

    Ball ball;

    public Engine()
    {
        screenDim = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

        ball = new(screenDim);

        // Spawn (x)px from each side - default 20px
        int distanceFromWall = 20;
        player1 = new(distanceFromWall);
        player2 = new((int)(screenDim.X) - distanceFromWall - (int)(Paddle.Size.X));
    }

    public void Run()
    {
        Raylib.BeginDrawing();

        Background();
        PaddleController();
        BallController();

        Raylib.EndDrawing();
    }

    private void Background()
    {
        int screenMidX = (int)screenDim.X / 2;
        int screenMidY = (int)screenDim.Y / 2;
        Raylib.ClearBackground(Color.WHITE);

        // Middle circles
        Raylib.DrawCircleLines(screenMidX, screenMidY, 50, Color.BLUE);
        Raylib.DrawCircle(screenMidX, screenMidY, 10, Color.BLUE);

        // Horizontal Lines
        Raylib.DrawLine(0, screenMidY, screenMidX - 50, screenMidY, Color.BLUE);
        Raylib.DrawLine(screenMidX + 50, screenMidY, screenMidX * 2, screenMidY, Color.BLUE);

        // Vertical Lines
        Raylib.DrawLine(screenMidX, 0, screenMidX, screenMidY - 50, Color.RED);
        Raylib.DrawLine(screenMidX, screenMidY + 50, screenMidX, screenMidY * 2, Color.RED);

        int player1Offset = Raylib.MeasureText(scores.player1Score.ToString(), 48);
        int player2Offset = Raylib.MeasureText(scores.player2Score.ToString(), 48);

        Raylib.DrawText(scores.player1Score.ToString(), (screenMidX + 100) - player1Offset / 2, 70, 48, Color.BLACK);
        Raylib.DrawText(scores.player2Score.ToString(), (screenMidX - 100) - player2Offset / 2, 70, 48, Color.BLACK);
    }

    private void BallController()
    {
        ball.Bounds(ref scores);
        ball.Move();
        ball.Render();
    }

    private void PaddleController()
    {
        CheckCollision();
        CheckMovement();
        RenderPaddles();
    }

    private void CheckCollision()
    {
        player1.Collide(ref ball);
        player2.Collide(ref ball);
    }

    private void CheckMovement()
    {
        KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();

        if (key == KeyboardKey.KEY_P) ball.TogglePause();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) player1.Move(Paddle.Direction.Up);
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) player1.Move(Paddle.Direction.Down);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) player2.Move(Paddle.Direction.Up);
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) player2.Move(Paddle.Direction.Down);
    }

    private void RenderPaddles()
    {
        player1.Render();
        player2.Render();
    }
}
