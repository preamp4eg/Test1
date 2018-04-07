using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.ReadKey();

            DataTable table = new DataTable("Table1");
           
            table.Columns.Add("Column1", typeof(int));
            
            

            string path = "~jopa.txt";
            Console.WriteLine("Enter for upload files from "+ path);
            Console.ReadKey();

            //здесь пошла программа

            //здесь распознаются файлы в папке
            //цикл по файлам
            //прочитать файл
            //парсить то что прочитали
            //условия из задания
            //закончить цикл




        }
    }
}
