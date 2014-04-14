using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void uploadFile (object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                // Ensure correct file type (either txt or php)
                if (FileUploadControl.PostedFile.ContentType == "text/plain" || FileUploadControl.PostedFile.ContentType == "application/octet-stream")
                {
                    // Ensure appropriate length
                    if (FileUploadControl.PostedFile.ContentLength < 102400)
                    {
                        string path = "~/UploadedFiles/" + Path.GetFileName(FileUploadControl.FileName);
                        path = Server.MapPath(path);

                        // Saves the uploaded file to the UploadedFiles folder in our root directory
                        FileUploadControl.SaveAs(path);
                        StatusLabel.Text = "Upload status: File uploaded!";

                        // Set the session variables and redirect
                        Session["currentFilePath"] = path;
                        Session["filename"] = Path.GetFileNameWithoutExtension(FileUploadControl.FileName);
                        
                        // Redirect to translation page
                        Response.Redirect("Translate.aspx");
                    }
                    else
                        StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                }
                else
                    StatusLabel.Text = "Upload status: Only TXT and PHP files are accepted!";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }

        }
        else
        {
            // No file
            StatusLabel.Text = "You have not selected a file to be uploaded";
        }
    }
}