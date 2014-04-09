using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUniter
{
    class Program
    {
        static void Main(string[] args)
        {
            string line, translatedWord;
            System.IO.StreamReader inputFile;
            System.IO.StreamReader trackerFile = null;
            System.IO.StreamWriter outputFile = null;
            string currentLanguage = "";

            #region Iterate through the directory and get all languages

            string[] files = Directory.GetFiles("..\\..\\..\\..\\taylorTXTS\\", "*", SearchOption.AllDirectories);
            String f;

            for (int i = 0; i < files.Length; i++)
            {
                f = files[i];
                f = f.Replace("..\\..\\..\\..\\taylorTXTS\\", "");
                f = f.Replace(".txt", "");
                files[i] = f;
            }

            #endregion


            foreach (String file in files)
            {
                currentLanguage = file;

                #region Open the tracker
                try
                {
                    trackerFile = new System.IO.StreamReader("..\\..\\..\\..\\tracker_indexed.php");
                }
                catch
                {
                    // Error logging
                    File.AppendAllText("log.txt", "Could not open the tracker_indexed file: " + DateTime.Now + " \n");
                }
                #endregion

                #region Create the destination folder and file if they don't exist AND open the new outputFile
       
                bool FolderExists = System.IO.Directory.Exists("..\\..\\..\\..\\lang\\" + currentLanguage);

                if (!FolderExists)
                    System.IO.Directory.CreateDirectory("..\\..\\..\\..\\lang\\" + currentLanguage);

                bool FileExists = System.IO.File.Exists("..\\..\\..\\..\\lang\\" + currentLanguage + "\\tracker.php");

                if (!FileExists)
                    using (System.IO.File.Create("..\\..\\..\\..\\lang\\" + currentLanguage + "\\tracker.php")) { };         

                try
                {
                    outputFile = new System.IO.StreamWriter("..\\..\\..\\..\\lang\\" + currentLanguage + "\\tracker.php");
                }
                catch
                {
                    // Error logging
                    File.AppendAllText("log.txt", "Could not open the new tracker file: " + DateTime.Now + " FOR language: " + currentLanguage + "\n");
                }

                #endregion

                #region Open the input txt file, write to tracker.php and close it all down

                try
                {
                    using (inputFile = new System.IO.StreamReader("..\\..\\..\\..\\taylorTXTS\\" + currentLanguage + ".txt"))
                    {

                        while ((translatedWord = inputFile.ReadLine()) != null)
                        {
                            // If we have an actual word
                            if (translatedWord.StartsWith("@"))
                            {
                                while ((line = trackerFile.ReadLine()) != null)
                                {
                                    // If we have an actual line to be replaced
                                    if (line.Contains('#'))
                                    {
                                        // Remove the @ from the translated word
                                        translatedWord = translatedWord.Substring(1);

                                        // Replace it, write to the file and continue;
                                        string newLine = line.Replace("#", translatedWord);

                                        outputFile.WriteLine(newLine);

                                        break;
                                    }

                                    outputFile.WriteLine(line);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // Error logging
                    File.AppendAllText("log.txt", "Could not complete the process during " + currentLanguage + ": " + DateTime.Now + " \n");
                }
                finally
                {
                    // Close it all down
                    trackerFile.Close();
                    outputFile.Close();
                }

                #endregion
            }
        }
    }
}
