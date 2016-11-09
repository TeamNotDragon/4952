using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4952.Models
{
    public class FileMetadata
    {
        public int fileID;
        public int userID;
        public string fileName;
        public int fileSize;
        public DateTime fileDateCreated;

        public FileMetadata(Models.File file)
        {
            fileID = file.FileID;
            userID = file.userID;
            fileName = file.fileName;
            fileSize = file.fileSize;
            fileDateCreated = file.fileDateCreated;
        }
    }
}