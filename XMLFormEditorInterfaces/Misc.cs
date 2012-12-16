using System;
using System.Collections.Generic;
using System.Text;

namespace XMLFormEditor
{
    public class Misc
    {
        public static string EvaluateRelativePath(string mainDirPath, string absoluteFilePath)
        {

            string[] firstPathParts = mainDirPath.Trim(System.IO.Path.DirectorySeparatorChar).Split(System.IO.Path.DirectorySeparatorChar);
            string[] secondPathParts = absoluteFilePath.Trim(System.IO.Path.DirectorySeparatorChar).Split(System.IO.Path.DirectorySeparatorChar);

            int sameCounter = 0;
            for (int i = 0; i < Math.Min(firstPathParts.Length, secondPathParts.Length); i++)
            {
                if (!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
                {
                    break;
                }
                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = String.Empty;

            for (int i = sameCounter; i < firstPathParts.Length - 1; i++)
            {
                newPath += "..";
                newPath += System.IO.Path.DirectorySeparatorChar;
            }

            for (int i = sameCounter; i < secondPathParts.Length - 1; i++)
            {
                newPath += secondPathParts[i];
                newPath += System.IO.Path.DirectorySeparatorChar;
            }

            newPath += secondPathParts[secondPathParts.Length - 1];

            return newPath;
        }


    }
}
