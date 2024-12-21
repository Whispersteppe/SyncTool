using System.Collections;

namespace SyncTool.Handler;

public class DirectoryPairEnumerator : IEnumerable<DirectoryMatchPair>
{

    DirectoryMatchPair _parent;

    public DirectoryPairEnumerator(DirectoryMatchPair parent)
    {
        _parent = parent;
    }
    public IEnumerator<DirectoryMatchPair> GetEnumerator()
    {
        var fromChildDirectories = new List<DirectoryInfo>(_parent.FromDirectoryInfo.GetDirectories());
        var toChildDirectories = new List<DirectoryInfo>(_parent.ToDirectoryInfo.GetDirectories());
        string toDirectoryPath = Path.GetFullPath(_parent.ToDirectoryInfo.FullName);
        string fromDirectoryPath = Path.GetFullPath(_parent.FromDirectoryInfo.FullName);

        List<DirectoryMatchPair> childPairs = new List<DirectoryMatchPair>();

        while (fromChildDirectories.Count > 0)
        {
            var fromChildDirectory = fromChildDirectories.First();
            fromChildDirectories.Remove(fromChildDirectory);

            var toChildDirectory = toChildDirectories.FirstOrDefault(x => x.Name == fromChildDirectory.Name);
            if (toChildDirectory != null)
            {
                toChildDirectories.Remove(toChildDirectory);
            }
            else
            {
                toChildDirectory = new DirectoryInfo(Path.Combine(toDirectoryPath, fromChildDirectory.Name));
            }

            DirectoryMatchPair childPair = new DirectoryMatchPair()
            {
                FromDirectoryInfo = fromChildDirectory,
                ToDirectoryInfo = toChildDirectory
            };

            childPairs.Add(childPair);
        }

        //  from is empty.  anything left is old on the to side
        while (toChildDirectories.Count > 0)
        {
            var toDirectory = toChildDirectories.First();
            toChildDirectories.Remove(toDirectory);

            DirectoryMatchPair childPair = new DirectoryMatchPair()
            {
                FromDirectoryInfo = new DirectoryInfo(Path.Combine(fromDirectoryPath, toDirectory.Name)),
                ToDirectoryInfo = toDirectory
            };

            childPairs.Add(childPair);
        }

        foreach(var childPair in childPairs)
        {
            yield return childPair;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

