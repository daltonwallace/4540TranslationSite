﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileUniter;
using ST;
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
        String filePath = (String) Session["currentFilePath"];
        String fileName = (String)Session["filename"];
        String languageCode = languagesDDL.SelectedValue;

        #region Split the file -- Todd

        // Given uploaded filePath and fileName WITHOUT extension 
        // (i.e. "~/uploadedfiles/forum.php" AND "forum")

        //                        Create: 
        // Server.MapPath("~/indexedFiles/" + fileName + "_indexed.php") 
        //                         AND 
        // Server.MapPath("~/wordFiles/" + fileName + "_words.txt")

            try
            {

            }
            catch (Exception eTodd)
            {
                TranslateStatusLabel.Text = "Could not split the file! Error 1";
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