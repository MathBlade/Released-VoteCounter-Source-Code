using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;



public static class FileWriting
{
  
    public static string WriteString(string fileName, string extension, string newText)
    {
        return WriteString(fileName, extension, newText, false);
    }
    private static string WriteString(string fileName, string extension, string newText, bool overwrite)
    {
        

        string endFileName = fileName;

        string path = Application.dataPath +"/" + endFileName + extension;
        int counter = 0;
        while (counter < 100)
        {

            if (IsFileLocked(new FileInfo(path)))
            {
                counter++;
                endFileName = fileName + counter;
                path = Application.dataPath +"/" + endFileName + extension;
            }
            else
            {
                break;
            }
        }

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, overwrite);
        writer.Write(newText);
        writer.Close();

        return endFileName;

        
    }

    private static bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            if (!file.Exists)
                return false;

            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }
}

