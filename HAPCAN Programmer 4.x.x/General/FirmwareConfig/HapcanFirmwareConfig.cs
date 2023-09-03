using Hapcan.Flows;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Hapcan.General;

[XmlRoot("HapcanFirmwareConfig")]
public class HapcanFirmwareConfig
{
    //PROPERTIES
    public static List<HapcanFirmwareConfig> FirmwareConfigList { get; set; } = new List<HapcanFirmwareConfig>();
    public FileClass File { get; set; }
    public FirmwareClass Firmware { get; set; }
    public NodeClass Node { get; set; }
    public ChannelsClass Channels { get; set; }


    /// <summary>
    /// Reads all firmware config files from given directory path and its subdirectories
    /// </summary>
    /// <param name="path">Folder where firmware config are read</param>
    /// <returns>Array of string containing file paths</returns>
    /// <exception cref="Exception"></exception>
    public static string[] GetFiles(string path)
    {
        try
        {
            FirmwareConfigList.Clear();
            return Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Reads and deserialize all firmware config files from given directory path and its subdirectories
    /// </summary>
    /// <param name="path">Folder where firmware config are read</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<HapcanFirmwareConfig> ReadAndValidateFileAsync(string path)
    {
        HapcanFirmwareConfig config = null;
        try
        {
            var rwf = new ReadWriteFile<HapcanFirmwareConfig>();
            config = await rwf.DeserializeAsync(path);
            //File
            if (config.File == null)
                throw new ArgumentException("'File' section is missing");
            config.File.Validate();
            //Firmware
            if (config.Firmware == null)
                throw new ArgumentException("'Firmware' section is missing");
            config.Firmware.Validate();
            //Node
            if (config.Node == null)
                throw new ArgumentException("'Node' section is missing");
            config.Node.Validate(config.Firmware);
            //Channels
            if (config.Channels == null)
                throw new ArgumentException("'Channels' section is missing");
            config.Channels.Validate(config.Firmware);
            //add
            AddConfigToList(config);
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
                throw new Exception(ex.InnerException.Message + " " + ex.Message + " " + Path.GetFullPath(path), ex);
            throw new Exception(ex.Message + " " + Path.GetFullPath(path), ex);
        }
        return config;
    }

    /// <summary>
    /// Checks if firmware config file exists for given node
    /// </summary>
    /// <param name="node">Hapcan node with hardware and firmware defined</param>
    /// <returns>Firmware config object</returns>
    public static HapcanFirmwareConfig GetMatched(HapcanNode node)
    {
        var firm = string.Format("{0:X4}.{1}.{2}.{3}.{4}",
            node.HardwareType, node.HardwareVersion, node.ApplicationType, node.ApplicationVersion, node.FirmwareVersion);
        var config = HapcanFirmwareConfig.FirmwareConfigList.FirstOrDefault(o => o.Firmware.Version == firm);
        if (config != null)
            node.Supported = true;
        return config;
    }

    /// <summary>
    /// Updates node properties from matched firmware config
    /// </summary>
    /// <param name="node">Node to be updated</param>
    public static void UpdateNodeFromFirmwareConfigs(HapcanNode node)
    {
        //find if firmware config exists
        var cfg = GetMatched(node);
        if (cfg != null)
        {
            node.FirmwareDescription = cfg.Firmware.Description;
            node.NotesAdr = cfg.Node.NotesAdr;
        }
        else
        {
            node.FirmwareDescription = "Unknown firmware, not supported by Programmer";
        }
    }


    /// <summary>
    /// Firmware config file template generator function
    /// </summary>
    public static async void GenerateFirmwareConfigTemplateAsync()
    {
        var f = new HapcanFirmwareConfig();
        //file
        f.File = new FileClass() { Revision = 456 };
        //firmware
        f.Firmware = new FirmwareClass() { Name = "UNIV 4.x.x.x", Version = "3000.4.x.x.x", Description = "Firmware description" };
        //node
        f.Node = new NodeClass() { NotesAdrStr = "0x010000" };
        //channels
        var channelList = new List<ChannelClass>();
        channelList.Add(new ChannelClass() { Id = 1, NameAdrStr = "0x010800", Type = HapcanChannel.ChannelType.Relay });
        f.Channels = new ChannelsClass() { ChannelList = channelList };

        //save
        var rw = new ReadWriteFile<HapcanFirmwareConfig>();
        await rw.SerializeAsync(f, "firmware_config_template.xml");
    }

    private static void AddConfigToList(HapcanFirmwareConfig config)
    {
        //check for duplcates on list
        var currentCfg = FirmwareConfigList.FirstOrDefault(o => o.Firmware.Version == config.Firmware.Version);
        //duplicate found
        if (currentCfg != null)
        {
            //current config is newer?
            if (currentCfg.File.Revision >= config.File.Revision)
            {
                throw new ArgumentException(string.Format("Found duplicate {0} (file revision: {1}) has been ignored.", config.Firmware.Version, config.File.Revision));
            }
            //current config is older
            else
            {
                FirmwareConfigList.Remove(currentCfg);
            }
        }
        //add new
        FirmwareConfigList.Add(config);
    }
}