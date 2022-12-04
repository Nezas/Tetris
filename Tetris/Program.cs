using Tetris;
using Tetris.Helpers;
using Tetris.Interfaces;
using Tetris.Writers;

IWriter writer = new ConsoleWriter();
IGameRenderer gameRenderer = new GameRenderer(writer);

Game game = new Game(Validator.Instance, gameRenderer);
game.Start();