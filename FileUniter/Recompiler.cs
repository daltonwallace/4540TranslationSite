using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUniter
{
    public class Recompiler
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recompiler()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationFileIndexed">The translation file with hashtages where the translated words need to be placed</param>
        /// <param name="wordsFile">A list of translated words that need to be placed in the translated file</param>
        public bool Recompile(String translationFileIndexed, String wordsFile, String destinationFile)
        {
            bool success = true;
            string line, translatedWord;
            System.IO.StreamReader inputFile;
            System.IO.StreamReader trackerFile = null;
            System.IO.StreamWriter outputFile = null;

            #region Open the indexedTranslationFile
            try
            {
                trackerFile = new System.IO.StreamReader(translationFileIndexed);
            }
            catch
            {
                // Error logging
                File.AppendAllText("ASPLog.txt", "Could not open the tracker_indexed file: " + DateTime.Now + " \n");
                success = false;
            }
            #endregion

            #region Create the destination folder and file if they don't exist AND open the new outputFile

            bool FileExists = System.IO.File.Exists(destinationFile);

            if (!FileExists)
                using (System.IO.File.Create(destinationFile)) { };

            try
            {
                outputFile = new System.IO.StreamWriter(destinationFile);
            }
            catch
            {
                // Error logging
                File.AppendAllText("log.txt", "Could not open the new tracker file: " + DateTime.Now + "\n");
                success = false;
            }

            #endregion

            #region Open the input txt file, write to tracker.php and close it all down

                try
                {
                    using (inputFile = new System.IO.StreamReader(wordsFile))
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
                    File.AppendAllText("log.txt", "Could not complete the process during : " + DateTime.Now + " \n");
                    success = false;
                }
                finally
                {
                    // Close it all down
                    trackerFile.Close();
                    outputFile.Close();
                }

                return success;

                #endregion
        }
    }
}
