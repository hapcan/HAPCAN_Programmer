using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Hapcan;

public class ReadWriteFile<T>
{
    private readonly object _lockproj = new object();

    /// <summary>
    /// Reads (xml deserializes) T type object from file.
    /// </summary>
    /// <param name="filename">Object file path.</param>
    /// <returns>Task with object instance</returns>
    public async Task<T> DeserializeAsync(string filepath)
    {
        return await Task.Run(
            () =>
            {
                lock (_lockproj)
                {
                    try
                    {
                        T project = default(T);
                        using var sr = new StreamReader(filepath, Encoding.UTF8);
                        var ser = new XmlSerializer(typeof(T));
                        project = (T)ser.Deserialize(sr);
                        return project;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Saves (xml serializes) T type object into the file.
    /// </summary>
    /// <param name="fileobject">T type object instance.</param>
    /// <param name="filepath">Object file path.</param>
    /// <returns>True if save was successful, otherwise false.</returns>
    public async Task<bool> SerializeAsync(T fileobject, string filepath)
    {
        return await Task.Run(
            () =>
            {
                lock (_lockproj)
                {
                    try
                    {
                        var ser = new XmlSerializer(typeof(T));
                        using var sw = new StreamWriter(filepath, false, Encoding.UTF8);
                        using var xw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true });
                        ser.Serialize(xw, fileobject);
                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        ).ConfigureAwait(false);
    }
}