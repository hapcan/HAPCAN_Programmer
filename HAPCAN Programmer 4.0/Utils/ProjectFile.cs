using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Hapcan;

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
                    try
                    {
                        T project = default(T);
                        using var sr = new StreamReader(filepath, Encoding.UTF8);
                        var ser = new XmlSerializer(typeof(T));
                        project = (T)ser.Deserialize(sr);
                        return project;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("", ex);
                    }
                }
            }
        ).ConfigureAwait(false);
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
                    try
                    {
                        var ser = new XmlSerializer(typeof(T));
                        using var sw = new StreamWriter(filepath, false, Encoding.UTF8);
                        using var xw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true });
                        ser.Serialize(xw, project);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("", ex);
                    }
                }
            }
        ).ConfigureAwait(false);
    }
}