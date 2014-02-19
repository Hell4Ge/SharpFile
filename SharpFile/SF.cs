using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SharpFile
{
    public class SF
    {
        private string fName;

        public SF(String fName)
        {
            this.fName = fName;
        }

        public void Write(String text)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(fName, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                sw.Write(text);
            }
        }

        public void Write(String[] text)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(fName, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                String w = "";
                for (int i = 0; i < text.Length; i++)
                    w += text[i];

                sw.Write(w);
            }
        }

        public void InsertLine(String text, int number)
        {
            if (number >= 1)
            {
                number--;                           // number-- for array index

                text += "\n";

                if (GetLinesCount() < number + 1)     // number+1 for real number
                    throw new Exception("The number is bigger than file lines count (" + GetLinesCount() + ")");

                String[] arr = ReadToArray();
                List<String> list = toList(arr);

                list.Insert(number, text);
                String[] final = fromList(list);
                Write(final);
            }
            else
                throw new Exception("The number is not equal or bigger than 1");
        }

        public void InsertLine(String text, int number, bool RAW)
        {
            if (number >= 1)
            {
                number--;                           // number-- for array index

                if (GetLinesCount() < number + 1)     // number+1 for real number
                    throw new Exception("The number is bigger than file lines count (" + GetLinesCount() + ")");

                String[] arr = ReadToArray();
                List<String> list = toList(arr);

                list.Insert(number, text);
                String[] final = fromList(list);
                Write(final);
            }
            else
                throw new Exception("The number is not equal or bigger than 1");
        }

        public void ReplaceLine(String text, int number)
        {
            number = (number == 1) ? 1 : number;
            if (number >= 1)
            {
                String[] arr = ReadToArray();
                List<String> list = toList(arr);

                list[number].Remove(0, list[number].Length);
                list[number] = (text += '\n');
                String[] final = fromList(list);
                Write(final);
            }
            else
                throw new Exception("The number is not equal or bigger than 1");
        }

        public String Read()
        {
            using (StreamReader sr = new StreamReader(fName))
            {
                StringBuilder sb = new StringBuilder();
                String line;

                while ((line = sr.ReadLine()) != null)
                    sb.AppendLine(line);

                return sb.ToString();
            }
        }

        public String[] ReadToArray()
        {
            String[] tmp = Read().Split('\n');
            String[] ret = new String[tmp.Length - 1];

            for (int i = 0; i < ret.Length; i++)
                ret[i] = tmp[i].Replace("\r", "\n");

            return ret;
        }

        public int GetLinesCount()
        {
            using (StreamReader sr = new StreamReader(fName))
            {
                int i = 0;
                while (sr.ReadLine() != null)
                    i++;

                return i;
            }
        }

        public void Append(String text)
        {
            String[] arr = ReadToArray();
            List<String> list = toList(arr);
            list.Add(text);
            String[] final = fromList(list);
            Write(final);
        }

        public int OccurencesOf(String searchString)
        {
            int count = 0, n = 0;
            String source = Read();
            while ((n = source.IndexOf(searchString, n, StringComparison.InvariantCulture)) != -1)
            {
                n += searchString.Length;
                ++count;
            }

            return count;
        }

        private List<String> toList(String[] array)
        {
            if (array.Length > 0)
            {
                List<String> list = new List<String>();

                for (int i = 0; i < array.Length; i++)
                    list.Add(array[i]);


                return list;
            }

            return null;
        }

        private String[] fromList(List<String> list)
        {
            if (list.Count > 0)
            {
                String[] ret = new String[list.Count];
                for (int i = 0; i < list.Count; i++)
                    ret[i] = list[i];

                return ret;
            }

            return null;
        }
    }
}