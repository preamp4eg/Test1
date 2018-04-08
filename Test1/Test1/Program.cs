using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialExplorer.IO.FastDBF;
using System.IO;

namespace transfer
{
    class Program
    {

        static void convertFileInfo(ref string filePath)
        {
            FileInfo fiPath;
            if (Path.IsPathRooted(filePath))
            {
                fiPath = new FileInfo(filePath);
            }
            else
            {
                fiPath = new FileInfo(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + filePath);
            }
            filePath = fiPath.FullName;
        }

        static void Main(string[] args)
        {
            string []csvFiles= { "ext.csv", "ext2.csv" };
            string dbfFile = "Extnames.dbf";

            foreach (string csvFile in csvFiles)
            {
                TranslateData(csvFile, dbfFile);
            }

        }

        public void TranslateData(string csvFile, string dbfFile)
        {
           
            convertFileInfo(ref csvFile);
            convertFileInfo(ref dbfFile);
            try
            {

                DbfFile odbf = new DbfFile();
                odbf.Open(dbfFile, FileMode.OpenOrCreate);
                odbf.Header.AddColumn(new DbfColumn("SEQUENCE", DbfColumn.DbfColumnType.Number, 6, 0));
                odbf.Header.AddColumn(new DbfColumn("LEVEL", DbfColumn.DbfColumnType.Number, 2, 0));
                odbf.Header.AddColumn(new DbfColumn("LINENUM", DbfColumn.DbfColumnType.Character, 10, 0));
                odbf.Header.AddColumn(new DbfColumn("NAME", DbfColumn.DbfColumnType.Character, 50, 0));
                odbf.Header.AddColumn(new DbfColumn("ACCNTCODE", DbfColumn.DbfColumnType.Character, 10, 0));
                odbf.Header.AddColumn(new DbfColumn("TENANT", DbfColumn.DbfColumnType.Number, 2, 0));
                odbf.Header.AddColumn(new DbfColumn("COS", DbfColumn.DbfColumnType.Number, 2, 0));
                odbf.Header.AddColumn(new DbfColumn("EXG", DbfColumn.DbfColumnType.Number, 2, 0));
                DbfRecord orec = new DbfRecord(odbf.Header);
                orec.AllowDecimalTruncate = true;
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(csvFile, Encoding.GetEncoding(1251));
                    int count = 1;
                    foreach (string line in lines)
                    {
                        if (!line.StartsWith("No."))
                        {
                            string tmpStr = line;
                            orec[0] = (count++).ToString();
                            orec[1] = "1";
                            tmpStr = tmpStr.Remove(0, tmpStr.IndexOf(";") + 1);
                            orec[2] = tmpStr.Substring(0, tmpStr.IndexOf(";")).Replace("Dial=", "");
                            tmpStr = tmpStr.Remove(0, tmpStr.IndexOf(";") + 1);
                            orec[3] = tmpStr;
                            orec[4] = "";
                            orec[5] = "1";
                            orec[6] = "0";
                            orec[7] = "0";
                            odbf.Write(orec, true);
                        }
                    }
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("Ошибка: не найден файл *.CSV " + ex.Message);
                }
                finally
                {
                    odbf.WriteHeader();
                    odbf.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: не найден файл данных *.CSV " + ex.Message);
            }
            finally
            {
                Console.WriteLine("\n Статус:  преобразования данных выполнено.");

            }

        }

    }
}
