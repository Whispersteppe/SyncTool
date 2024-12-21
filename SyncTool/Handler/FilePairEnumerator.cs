using System.Collections;

namespace SyncTool.Handler;

public class FilePairEnumerator : IEnumerable<FileMatchPair>
{
    DirectoryMatchPair _parentPair;
    public FilePairEnumerator(DirectoryMatchPair parentPair)
    {
        _parentPair = parentPair;
    }

    public IEnumerator<FileMatchPair> GetEnumerator()
    {
        var fromFiles = new List<FileInfo>(_parentPair.FromDirectoryInfo.GetFiles());
        var toFiles = new List<FileInfo>(_parentPair.ToDirectoryInfo.GetFiles());

        string toDirectoryPath = Path.GetFullPath(_parentPair.ToDirectoryInfo.FullName);
        string fromDirectoryPath = Path.GetFullPath(_parentPair.FromDirectoryInfo.FullName);

        List<FileMatchPair> filePairs = new List<FileMatchPair>();

        while (fromFiles.Count() > 0)
        {
            var fromFile = fromFiles.First();
            fromFiles.Remove(fromFile);
            var toFile = toFiles.FirstOrDefault(x => x.Name == fromFile.Name);
            if (toFile != null)
            {
                toFiles.Remove(toFile);
            }
            else
            {
                toFile = new FileInfo(Path.Combine(toDirectoryPath, fromFile.Name));
            }

            FileMatchPair childFile = new FileMatchPair()
            {
                FromFileInfo = fromFile,
                ToFileInfo = toFile
            };
            filePairs.Add(childFile);

        }

        //  from is empty.  anything left is old on the to side
        while (toFiles.Count > 0)
        {
            var toFile = toFiles.First();
            toFiles.Remove(toFile);

            FileMatchPair childPair = new FileMatchPair()
            {
                FromFileInfo = new FileInfo(Path.Combine(fromDirectoryPath, toFile.Name)),
                ToFileInfo = toFile
            };

            filePairs.Add(childPair);
        }

        foreach (var childPair in filePairs)
        {
            yield return childPair;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

