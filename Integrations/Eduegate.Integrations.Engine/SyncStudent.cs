using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Integrations.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

using System.IO;
using Microsoft.Data.SqlClient;
using Eduegate.Framework.Extensions;

namespace Eduegate.Integrations.Engine
{
    public class SyncStudent
    {
        public static void Sync()
        {
            var studentFactory = IntegratorFactory.GetStudentFactory(ConfigurationManager.AppSettings["Client"]);
            var allStudents = studentFactory.GetStudents();
            var mapper = StudentMapper.Mapper(null);

            foreach (var student in allStudents)
            {
                mapper.SaveEntity(student);
            }
        }

        public static void SyncStudentProfie(ArgParameter param)
        {
            var studentFactory = IntegratorFactory.GetStudentFactory(ConfigurationManager.AppSettings["Client"]);
            var allStudents = studentFactory.GetStudents();
            //try
            //{
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set Timedout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))              
                {
                    try { conn.Open(); } catch { return; }
                    List<string> _sProfiles = new List<string>();
                    foreach (var student in allStudents)
                    {
                        using (var client = new WebClient())
                        {
                            if (!_sProfiles.Contains(student.StudentProfile))
                                _sProfiles.Add(student.StudentProfile);
                            else
                                continue;
                        try
                        {
                            client.DownloadFile(param.ServerPath + "/" + student.StudentProfile, param.LocalPath + student.StudentProfile.ToString());
                        }
                        catch (Exception ex) { continue; }
                            MemoryStream stream = new MemoryStream();
                            try
                            {
                                System.Drawing.Bitmap image = new System.Drawing.Bitmap((param.LocalPath + student.StudentProfile.ToString()));
                                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }

                           
                            string _SQL = @"DECLARE @STUDENTID AS BIGINT =0,@PROFILEID AS BIGINT =0
                                            SELECT @STUDENTID=StudentIID FROM schools.Students WHERE AdmissionNumber='" + student.AdmissionNumber + @"'
                                            INSERT INTO contents.ContentFiles(ReferenceID, ContentFileName, ContentData)
                                            VALUES(@STUDENTID,@ContentFileName,@ContentData)
                                            SET @PROFILEID=@@IDENTITY
                                            UPDATE x SET STUDENTPROFILE =@PROFILEID FROM SCHOOLS.STUDENTS x WHERE STUDENTIID=@STUDENTID
                                            ";

                            SqlCommand cmd = new SqlCommand(_SQL, conn);
                        try
                        {
                            cmd.Parameters.AddWithValue("@ContentFileName", student.StudentProfile.ToString());
                            cmd.Parameters.AddWithValue("@ContentData", stream.ToArray());
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex) { continue; }

                        }
                    }
                }
            //}
            //catch(Exception ex)
            //{ 
            //}
        }

        private byte[] ImageToStream(string fileName)
        {
            MemoryStream stream = new MemoryStream();
        tryagain:
            try
            {
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(fileName);
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                goto tryagain;
            }

            return stream.ToArray();
        }
    }
}
