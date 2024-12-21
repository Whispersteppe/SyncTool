using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SyncTool.Test;

public class TestBase
{
    ITestOutputHelper _output;
    public TestBase(ITestOutputHelper output)
    {
        _output = output;
    }

    public void WriteObject(object obj, string? name = null)
    {
        var options = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        string serializedData = JsonConvert.SerializeObject(obj, options);

        string displayName = !string.IsNullOrEmpty(name) ?name : nameof(obj);

        _output.WriteLine(displayName);
        _output.WriteLine(serializedData);
    }

    public void WriteLine(string data)
    {
        _output.WriteLine(data);
    }
}
