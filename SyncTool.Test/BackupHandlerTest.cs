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
    public async Task TestBackupHandlerWithBackup()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var testDirectoryPath = Path.Combine(currentDirectory.Parent.FullName, "TestBackup");
        var testDirectory = Directory.CreateDirectory(testDirectoryPath);

        WriteLine($"From: {currentDirectory.FullName}");
        WriteLine($"To: {testDirectory.FullName}");

        BackupHandler handler = new BackupHandler();

        SyncPairOptions options = new SyncPairOptions()
        {
            FromPath = currentDirectory.FullName,
            ToPath = testDirectory.FullName
        };

        await handler.StartHandler(options);
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

    [Fact]
    public void VerifyPathBreakdown()
    {
        string testPath = @"E:\SqlData\TCIngester_log.ldf";
        string backupExtension = Path.GetExtension(testPath);
        string backupPath = Path.GetDirectoryName(testPath);
        string backupFileName = Path.GetFileNameWithoutExtension(testPath);
        string backupFullFileName = backupPath + @"\" + backupFileName + '.' + DateTime.Now.ToString("yyyyMMddHHmmss") + backupExtension;

        WriteLine(testPath);
        WriteLine(backupExtension);
        WriteLine(backupPath);
        WriteLine(backupFileName);
        WriteLine(backupFullFileName);
    }
}
