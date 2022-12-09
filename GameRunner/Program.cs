using GameRunner;
using GameRunner.Data;
using GameRunner.Logic;

var validator = new Validator();
var mapReader = new MapReader(validator);
var exitFinder = new ExitFinder();

IGame game = new Game(mapReader, exitFinder);

var result = game.Run(@"TestData\map2.txt");