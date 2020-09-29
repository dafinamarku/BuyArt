using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ProjektiPerfundimtarIkub
{
  public class FileClass
  {
    public bool ValidateFile(HttpPostedFileBase file)
    {
      string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
      string[] allowedFileTypes = { ".png", ".jpeg", ".jpg" };
      if (allowedFileTypes.Contains(fileExtension))
      {
        return true;
      }
      return false;
    }

    public void StoreFile(HttpPostedFileBase file)
    {
      WebImage img = new WebImage(file.InputStream);
      img.Save(Constant.ArtworkImagePath + file.FileName);
    }
  }
}