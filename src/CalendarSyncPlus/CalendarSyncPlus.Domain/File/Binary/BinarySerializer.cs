﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CalendarSyncPlus.Domain.File.Binary
{
    public class BinarySerializer<T> : IBinarySerializer<T> where T : class, new()
    {
        #region IBinarySerializer<T> Members

        public T Deserialize(string xml)
        {
            return Deserialize(xml, Encoding.UTF8);
        }

        public T Deserialize(string xml, Encoding encoding)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException("XML cannot be null or empty", "xml");
            }

            var formatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream(encoding.GetBytes(xml)))
            {
                return (T) formatter.Deserialize(memoryStream);
            }
        }

        public T DeserializeFromFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException("filename", "XML filename cannot be null or empty");
            }

            if (!System.IO.File.Exists(filename))
            {
                throw new FileNotFoundException("Cannot find XML file to deserialize", filename);
            }

            T obj;
            // Open the file containing the data that you want to deserialize.
            var fs = new FileStream(filename, FileMode.Open);
            try
            {
                var formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and  
                // assign the reference to the local variable.
                obj = (T) formatter.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
            return obj;
        }

        public string Serialize(T source)
        {
            throw new NotImplementedException();
        }

        public void SerializeToFile(T source, string filename)
        {
            // To serialize the hashtable and its key/value pairs,   
            // you must first open a stream for writing.  
            // In this case, use a file stream.
            var fs = new FileStream(filename, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, source);
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion
    }
}