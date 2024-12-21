using SyncTool.Classes;
using SyncTool.Handler;
using Xunit.Abstractions;

namespace SyncTool.Test;

public class BackupHandlerTest : TestBase
{
    public BackupHandlerTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task TestBackupHandler()
    {

        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var testDirectory = Directory.CreateTempSubdirectory();

        WriteLine($"From: {currentDirectory.FullName}");
        WriteLine($"To: {testDirectory.FullName}");

        BackupHandler handler = new BackupHandler();

        SyncPairOptions options = new SyncPairOptions()
        {
            FromPath = currentDirectory.FullName,
            ToPath = testDirectory.FullName
        };

        await handler.StartHandler(options);

        testDirectory.Delete(true);

    }

    [Fact]
    public void TestDirectoryPairEnumerator()
    {
        var fromDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var toDirectory = Directory.CreateTempSubdirectory();

        WriteLine($"From: {fromDirectory.FullName}");
        WriteLine($"To: {toDirectory.FullName}");

        DirectoryMatchPair startPair = new DirectoryMatchPair() 
        { 
            FromDirectoryInfo = fromDirectory, 
            ToDirectoryInfo = toDirectory 
        };

        DirectoryPairEnumerator enumer = new DirectoryPairEnumerator(startPair);

        foreach (var pair in enumer)
        {
            WriteLine($"{pair.FromDirectoryInfo.FullName} :: {pair.ToDirectoryInfo.FullName}");
        }

        toDirectory.Delete(true);

    }

    [Fact]
    public void TestFilePairEnumerator()
    {
        var fromDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var toDirectory = Directory.CreateTempSubdirectory();

        WriteLine($"From: {fromDirectory.FullName}");
        WriteLine($"To: {toDirectory.FullName}");

        DirectoryMatchPair startPair = new DirectoryMatchPair()
        {
            FromDirectoryInfo = fromDirectory,
            ToDirectoryInfo = toDirectory
        };

        FilePairEnumerator enumer = new FilePairEnumerator(startPair);

        foreach (var pair in enumer)
        {
            WriteLine($"{pair.FromFileInfo.FullName} :: {pair.ToFileInfo.FullName}");
        }


        toDirectory.Delete(true);
    }
}
