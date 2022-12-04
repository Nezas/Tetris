using Tetris;
using Tetris.Helpers;
using Tetris.Interfaces;
using Tetris.Writers;

IWriter writer = new ConsoleWriter();
IGameRenderer gameRenderer = new GameRenderer(writer);
Validator validator = new Validator();

Game game = new Game(validator, gameRenderer);
game.Start();