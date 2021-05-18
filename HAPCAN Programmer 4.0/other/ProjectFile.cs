using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Threading;

namespace Hapcan
{
    public class ProjectFile<T>
    {
        private static readonly object _lockproj = new object();

        /// <summary>
        /// Reads (xml deserializes) T type project from file.
        /// </summary>
        /// <param name="filename">Project file path.</param>
        /// <returns>Task with project instance</returns>
        public async Task<T> DeserializeAsync(string filepath)
        {
            return await Task.Run(
                () =>
                {
                    lock (_lockproj)
                    {
                        T project = default(T);
                        try
                        {
                            using (StreamReader sr = new StreamReader(filepath, Encoding.UTF8))
                            {
                                XmlSerializer ser = new XmlSerializer(typeof(T));
                                project = (T)ser.Deserialize(sr);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("", ex);
                        }
                        return project;
                    }
                }
            );
        }

        /// <summary>
        /// Saves (xml serializes) T type project into the file.
        /// </summary>
        /// <param name="project">T type project instance.</param>
        /// <param name="filepath">Project file path.</param>
        /// <returns>True if save was successful, otherwise false.</returns>
        public async Task<bool> SerializeAsync(T project, string filepath)
        {
            return await Task.Run(
                () =>
                {
                    lock (_lockproj)
                    {
                        var result = false;
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(filepath, false, Encoding.UTF8))
                            {
                                XmlSerializer ser = new XmlSerializer(typeof(T));
                                ser.Serialize(sw, project);
                            }
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("", ex);
                        }
                        return result;
                    }
                }
            );
        }
    }
}
