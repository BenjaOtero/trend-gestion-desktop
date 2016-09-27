using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace BL
{
    public class UtilFTP
    {
        public static void UploadFromFile(string nombreLocal, string nombreServidor)
        {
            List<String> credentials = UtilVarios.GetCredentialsFTP();
            string server = credentials[0];
            string user = credentials[1];
            string pass = credentials[2];
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + server + nombreServidor);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, pass);
            byte[] fileContents = File.ReadAllBytes(nombreLocal);
            request.ContentLength = fileContents.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }

        public static void DownloadFile(string nombreLocal, string nombreServidor)
        {
            List<String> credentials = UtilVarios.GetCredentialsFTP();
            string server = credentials[0];
            string user = credentials[1];
            string pass = credentials[2];
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + server + nombreServidor);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(user, pass);
            FtpWebResponse objResponse = (FtpWebResponse)request.GetResponse();
            byte[] buffer = new byte[32768];
            using (Stream input = objResponse.GetResponseStream())
            {
                if (File.Exists(nombreLocal)) File.Delete(nombreLocal);
                using (FileStream output = new FileStream(nombreLocal, FileMode.CreateNew))
                {
                    int bytesRead;
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }
            objResponse.Close();
        }

        public static void UploadFromMemoryStream(MemoryStream memoryStream, string nombreRemoto, string servidor)
        {
            List<String> credentials = UtilVarios.GetCredentialsFTP();
            string server = credentials[0] + "/datos";
            string user = credentials[1];
            string pass = credentials[2];
            FtpWebRequest reqFTP;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + server + "/" + nombreRemoto));
            reqFTP.Credentials = new NetworkCredential(user, pass);
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = memoryStream.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // Stream to which the file to be upload is written
            Stream strm = reqFTP.GetRequestStream();
            // Read from the file stream 2kb at a time
            memoryStream.Seek(0, SeekOrigin.Begin);
            contentLen = memoryStream.Read(buff, 0, buffLength);
            // Till Stream content ends
            while (contentLen != 0)
            {
                // Write Content from the file stream to the FTP Upload Stream
                strm.Write(buff, 0, contentLen);
                contentLen = memoryStream.Read(buff, 0, buffLength);
            }

            // Close the file stream and the Request Stream
            strm.Close();
            memoryStream.Close();
        }

        public static FtpWebRequest FtpRequest(string path)
        {
            List<String> credentials = UtilVarios.GetCredentialsFTP();
            string server = credentials[0];
            string user = credentials[1];
            string pass = credentials[2];
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + server + path);
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            return ftpRequest;
        }
    }
}
