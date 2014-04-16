using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileUniter;
using ST;
using System.Text;
using System.Threading;



public partial class Translate : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["currentFilePath"] == null)
            {
                // If we have no uploaded files then we should redirect them to the Default page
                Response.Redirect("Default.aspx");
            }
            else if (!File.Exists((String)Session["currentFilePath"]))
            {
                // If for some reason the file path no longer exists, set the session to null and redirect
                Session["currentFilePath"] = null;

                Response.Redirect("Default.aspx");
            }
        }

    }

    /// <summary>
    /// Begin the translation of the session
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TranslateClick(object sender, EventArgs e)
    {
        bool success = true;

        // The file is assumed to exist at this point
        String filePath = (String)Session["currentFilePath"];
        String fileName = (String)Session["filename"];
        String languageCode = languagesDDL.SelectedValue;

        #region Split the file -- Todd
        // Given uploaded filePath and fileName WITHOUT extension 
        // (i.e. "~/uploadedfiles/forum.php" AND "forum")

        //                        Create: 
        // Server.MapPath("~/indexedFiles/" + fileName + "_indexed.php") 
        //                         AND 
        // Server.MapPath("~/wordFiles/" + fileName + "_words.txt")

        String trackerFilePath = filePath;
        String trackerTmpFilePath = Server.MapPath("~/indexedFiles/" + fileName + "_tmp.php");
        String trackerWordsFilePath = Server.MapPath("~/wordFiles/" + fileName + "_words.txt");
        String trackerWordsTmpFilePath = Server.MapPath("~/wordFiles/" + fileName + "_words_tmp.txt");
        String trackerIndexedFilePath = Server.MapPath("~/indexedFiles/" + fileName + "_indexed.php");

        try
        {



            #region Extract Words

            // Set up to read in the original lang file to be translated
            StreamReader sr = new StreamReader(trackerFilePath);

            // Set up to remove escaped characters
            StreamWriter sanitizedInput = new StreamWriter(trackerTmpFilePath);

            // Set up the 'words' file for it's first stage of writing
            StreamWriter wordWriter_Stage1 = new StreamWriter(trackerWordsTmpFilePath);

            // Read in the file to translate
            String text = "";
            String[] extractedWords;

            text = sr.ReadToEnd();  // Raw text
            sr.Close();             // Done reading so close and dispose
            sr.Dispose();

            // Replace all escaped characters with &var, where 'var' is the char name
            // And write to new file
            sanitizedInput.Write(text.Replace("\\'", "&tick"));
            sanitizedInput.Close();         // Done writing so close and dispose
            sanitizedInput.Dispose();

            // Read sanitized tracker.php file
            StreamReader siReader = new StreamReader(trackerTmpFilePath);
            String sanitizedText = siReader.ReadToEnd();
            siReader.Close();
            siReader.Dispose();

            /** Here we split the now sanitized file that only contains the $string values we want
             * into subsections delimited by the '.
             * 
             * Split functions in such a way that the following string evalutes to the subsequent subsections:
             *   'In the sun,' she said, 'I would melt.' => [In the sun,] [ she said, ] [I would melt.]
             *   
             * Our expected input format is: $string['varname'] = 'value';
             * 
             * In order to extract the value we must take the 3rd substring. This is true for input with only one line
             * If our input has more than one line then the value lies in every 4th subsection                 * 
             *
             */

            // Divide the text into subsections
            extractedWords = sanitizedText.Split(new char[] { '\'', '\'' });


            // Extract the third occurance, giving us the first value
            wordWriter_Stage1.WriteLine("@" + extractedWords[3]);
            for (int i = 7; i < extractedWords.Length; i += 4)
            {
                // exctract ever 4th occurance
                wordWriter_Stage1.WriteLine("@" + extractedWords[i]);
            }

            wordWriter_Stage1.Close();
            wordWriter_Stage1.Dispose();

            // Read 'words' file from stage 1
            StreamReader wordReader = new StreamReader(trackerWordsTmpFilePath);
            String wordsList = wordReader.ReadToEnd();      // dump the file contents into a string
            // Probably not the most efficient way, but it is the most
            // robust for the dev'r
            wordReader.Close();
            wordReader.Dispose();

            // Replace &tick with "\\'"
            StreamWriter wordWriter_Stage2 = new StreamWriter(trackerWordsFilePath);
            wordWriter_Stage2.Write(wordsList.Replace("&tick", "\\'"));
            wordWriter_Stage2.Close();

            #endregion

            #region Index the file

            StreamReader trackerReader = new StreamReader(trackerFilePath);
            StreamWriter trackerTmpWriter = new StreamWriter(trackerTmpFilePath);

            String text1 = trackerReader.ReadLine();

            // Extract only the lines that begin the variable definition
            while (text1 != null)
            {
                if (text1 != "" && text1[0].Equals('$'))
                {
                    trackerTmpWriter.WriteLine(text1);
                }

                text1 = trackerReader.ReadLine();
            }

            trackerTmpWriter.Close();
            trackerTmpWriter.Dispose();


            StreamReader trackerTmpReader = new StreamReader(trackerTmpFilePath);
            String text2 = trackerTmpReader.ReadLine();
            StringBuilder sb = new StringBuilder(text2);
            StringBuilder tmp = new StringBuilder();

            int index = 0;
            int length = 0;

            while (text2 != null)
            {
                foreach (char ch in text2)
                {
                    if (ch.Equals('='))
                    {
                        // This assumes proper file input format
                        index = text2.IndexOf(ch);
                        if ((text2.Length - (index + 2)) >= 0)
                            sb[++index] = ' ';
                        else
                            sb.Append(" ");

                        if ((text2.Length - (index + 2)) >= 0)
                            sb[++index] = '\'';
                        else
                            sb.Append("'");

                        if ((text2.Length - (index + 2)) >= 0)
                            sb[++index] = '#';
                        else
                            sb.Append("#");

                        if ((text2.Length - (index + 2)) >= 0)
                            sb[++index] = '\'';
                        else
                            sb.Append("'");

                        if ((text2.Length - (index + 2)) >= 0)
                            sb[++index] = ';';
                        else
                            sb.Append(";");

                        length = text2.Length - index - 1;

                        if (length < 0)
                        {
                            length++;
                        }
                        // string length - char position + 1 removes the excess :)
                        sb.Remove(++index, length);

                    }
                }

                tmp.AppendLine(sb.ToString());
                text2 = trackerTmpReader.ReadLine();
                sb = new StringBuilder(text2);
            }

            trackerTmpReader.Close();
            trackerTmpReader.Dispose();


            StreamWriter trackerIndexedWriter = new StreamWriter(trackerIndexedFilePath);
            trackerIndexedWriter.Write(tmp.ToString());
            trackerIndexedWriter.Close();
            trackerIndexedWriter.Dispose();

            #endregion


        }
        catch (Exception eTodd1)
        {
            TranslateStatusLabel.Text = "Could not split the file! Error 1\n" + eTodd1.Message + "\n" + eTodd1;
        }

        try
        {
            File.Delete(trackerTmpFilePath);
            File.Delete(trackerWordsTmpFilePath);
        }
        catch (Exception eTodd2)
        {
            TranslateStatusLabel.Text = eTodd2 + "\nCould not delete temporary files. Their existence may pose a security threat";
        }
        #endregion

        #region Translate the file -- Taylor

        try
        {
            int isDone = -1;

            // Given a language code AND a file with words  AND fileName WITHOUT extension(i.e. "es" AND "~/wordFiles/forum_words.txt" AND "forum" "
            // Create "~/wordFiles/forum_words_translated.txt"

            SeleniumTranslator.TranslateInputFile(Server.MapPath("~/wordFiles/" + fileName + "_words.txt"), languageCode, Server.MapPath("~/tmp/" + fileName + "_words.txt"), ref isDone);

            // Wait until the process is completed...
            while (isDone < 0) { Thread.Sleep(5000); };

            SeleniumFixer.FixInput(Server.MapPath("~/tmp/" + fileName + "_words.txt"), Server.MapPath("~/wordFiles/" + fileName + "_words_translated.txt"));
        }
        catch (Exception eTaylor)
        {
            TranslateStatusLabel.Text = "Could not translate the file! Error 2";
        }


        #endregion

        #region Recompile the files

        try
        {
            Recompiler r = new Recompiler();

            String translationFileIndexed = Server.MapPath("~/indexedFiles/" + fileName + "_indexed.php");
            String translatedWords = Server.MapPath("~/wordFiles/" + fileName + "_words_translated.txt");
            String destinationFile = Server.MapPath("~/Results/" + fileName + "_final.php");

            success = r.Recompile(translationFileIndexed, translatedWords, destinationFile);

            // When completed, if succsessful
            // Show download button
            if (success)
            {
                downloadButton.Visible = true;
                Session["downloadMe"] = destinationFile;
            }
            else
            {
                Session["downloadMe"] = null;
                TranslateStatusLabel.Text = "Could not recompile the file!";
            }
        }
        catch (Exception eDalton)
        {
            TranslateStatusLabel.Text = "Could not recompile the file! Error 3";
        }

        #endregion


    }


    protected void DownloadFile(object sender, EventArgs e)
    {
        // Check to ensure that there exists a final file to download
        if (Session["downloadMe"] == null)
        {
            TranslateStatusLabel.Text = "Could not download the file!";
            return;
        }

        //To Get the physical Path of the file(test.txt)
        string filepath = (String)Session["downloadMe"];
        String fileName = (String)Session["filename"];

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath);

        // Checking if file exists
        if (myfile.Exists)
        {
            Response.Clear();

            Response.ClearHeaders();

            Response.ClearContent();

            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + "_Final.php");

            Response.AddHeader("Content-Length", myfile.Length.ToString());

            Response.ContentType = "text/plain";

            Response.Flush();

            Response.TransmitFile(myfile.FullName);

            Response.End();
        }
    }
}