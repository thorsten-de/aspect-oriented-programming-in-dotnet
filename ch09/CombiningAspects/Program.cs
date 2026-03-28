

using Lamar;

var container = new Container(x =>
{
    x.Scan(s =>
    {
        s.TheCallingAssembly();
        s.WithDefaultConventions();
    });
});

