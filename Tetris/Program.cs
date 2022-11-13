using Tetris;
using Tetris.Interfaces;
using Tetris.Writers;

IWriter writer = new ConsoleWriter();
GameRenderer gameRenderer = new GameRenderer(writer);

Game game = new Game(gameRenderer, writer);
game.Start();