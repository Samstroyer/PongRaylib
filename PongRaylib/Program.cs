using Raylib_cs;

Engine e;

Setup();
Draw();

void Setup()
{
    Raylib.SetTargetFPS(144);
    Raylib.InitWindow(1000, 800, "Pong");
    e = new();
}

void Draw()
{
    while (!Raylib.WindowShouldClose())
    {
        e.Run();
    }
}