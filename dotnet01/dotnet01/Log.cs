﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace dotnet01
{
    public static class Log
    {
        private static object sync = new object();

        /// <summary>
        /// папка с логами %Репозиторий%\dotnet01\dotnet01\Log
        /// создается по одному логу в день, если было что-то внесено
        /// </summary>
        /// <param name="ex"> информация об исключении записывается в лог</param>
        public static void Write(Exception ex)
        {
            try
            {
                 string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
                string filename = Path.Combine(pathToLog, string.Format("{0:dd.MM.yyy}.log",
                 DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}.{2}()] {3}\r\n",
                DateTime.Now, ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }
        }
        /// <summary>
        /// папка с логами %Репозиторий%\dotnet01\dotnet01\Log
        /// создается по одному логу в день, если было что-то внесено
        /// </summary>
        /// <param name="text"> текст, записываемый в лог</param>
        public static void Write(string  text)
        {
            try
            {
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog); // Создаем директорию, если нужно
                string filename = Path.Combine(pathToLog, string.Format("{0:dd.MM.yyy}.log",
                 DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] {1}\r\n",
                DateTime.Now, text);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
                // Перехватываем все и ничего не делаем
            }

        }

       
        
    
    }
}