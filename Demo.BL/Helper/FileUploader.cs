using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class FileUploader
    {

        public static string UploadFile(IFormFile file ,string FolderName)
        {
            try
            {

                //1 ) Get Directory
                var CurrentDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName);
                string FolderPath = CurrentDirectory;

                
                //2) Get File Name
                 string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);


                //3) Merge Path with File Name
                 string FinalPath = Path.Combine(FolderPath, FileName);


                //4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }

                return FileName;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RemoveFile(string FolderName , string FileName)
        {
            try
            {

                var CurrentDirectory = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot/Files", FolderName , FileName);
               
                if (File.Exists(CurrentDirectory))
                {
                    File.Delete(CurrentDirectory);
                }

                return "File Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
