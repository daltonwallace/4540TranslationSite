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

            #region We have a valid file
            // Bind the dropdown list
            languagesDDL.DataSource = new ArrayList() { "English", "Spanish", "Italian", "German", "French", "Lithuanian", };
            languagesDDL.DataBind();
            #endregion
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
        String givenFileName = "tracker";
        String wordFileName = "es";

        // The hard part
        // Taylor is having difficulty getting the file paths correct during testing.
        // Translate requires 3 params : 1-> the input path; 2-> language code; 3-> the output path
        // Fixer requires 2 params : 1-> the input path (which is the recent output path of the translator); 2-> the output path
        SeleniumTranslator.TranslateInputFile("/* INPUT FILE */", "/* LANG */", "/* DESTINATION */");
        SeleniumFixer.FixInput("/* INPUT */", "/* FIXED OUTPUT */");
       
        #region Recompile the Files back together

        Recompiler r = new Recompiler();

        String translationFileIndexed = Server.MapPath("~/indexedFiles/" + givenFileName +"_indexed.php");
        String wordsFile = Server.MapPath("~/wordFiles/" + wordFileName + ".txt");
        String destinationFile = Server.MapPath("~/Results/" + givenFileName + "_final.php");

        success = r.Recompile(translationFileIndexed, wordsFile, destinationFile);

        // When completed if succsessful
        // Show download button
        if (success)
        {
            downloadButton.Visible = true;
            Session["downloadMe"] = destinationFile;
        }
        else
        {
            Session["downloadMe"] = null;
        }

        #endregion
    
    }


    protected void DownloadFile(object sender, EventArgs e)
    {
        // Check to ensure that there exists a final file to download
        if (Session["downloadMe"] == null)
        {
            return;
        }

        //To Get the physical Path of the file(test.txt)
        string filepath = (String)Session["downloadMe"];

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath);

        // Checking if file exists
        if (myfile.Exists)
        {
            Response.Clear();

            Response.ClearHeaders();

            Response.ClearContent();

            Response.AddHeader("Content-Disposition", "attachment; filename=Final.php");

            Response.AddHeader("Content-Length", myfile.Length.ToString());

            Response.ContentType = "text/plain";

            Response.Flush();

            Response.TransmitFile(myfile.FullName);

            Response.End();
        }
    }
}