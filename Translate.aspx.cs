using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        // The hard part


        // When completed if succsessful
        // Show download button
        if (success)
        {
            downloadButton.Visible = true;
        }
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        //To Get the physical Path of the file(test.txt)
        string filepath = (String)Session["currentFilePath"];

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath);

        // Checking if file exists
        if (myfile.Exists)
        {
            Response.Clear();

            Response.ClearHeaders();

            Response.ClearContent();

            Response.AddHeader("Content-Disposition", "attachment; filename=Tracker.php");

            Response.AddHeader("Content-Length", myfile.Length.ToString());

            Response.ContentType = "text/plain";

            Response.Flush();

            Response.TransmitFile(myfile.FullName);

            Response.End();
        }
    }
}