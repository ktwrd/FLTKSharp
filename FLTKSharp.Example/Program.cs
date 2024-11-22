using FLTKSharp.Core;
using NLog;
using FLTKSharp.Example;

var log = LogManager.GetLogger("Example");


FLTK.Initialize();
new HelloWorldExample().Show();
FLTK.Run();