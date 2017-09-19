using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NanoVirus
{
    class TextWriter
    {
        private string FilePath;
        StreamWriter Writer;

        public TextWriter(string FilePath)
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new NullReferenceException("FilePath can not be null empty or cotnain white space");

            if (!FilePath.ToLower().Contains(".txt"))
                FilePath += ".txt";

            if (File.Exists(FilePath))
                File.WriteAllText(FilePath, "");

            this.FilePath = FilePath;
        }

        public void WriteToFile(string line)
        {
            lock (this)
            {
                try
                {
                    using(Writer = new StreamWriter(FilePath, true))
                    {
                        Writer.WriteLine(line);
                    }
                    Writer.Close();
                }
                catch (IOException e)
                {
                    throw e;
                }
            }
        }

        public void WriteToFile(string[] lines)
        {
            foreach (string line in lines)
            {
                WriteToFile(line);
            }
        }
    }
}
