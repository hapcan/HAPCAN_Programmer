using Hapcan.General;
using Hapcan.Messages;
using ScottPlot.MarkerShapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hapcan.Flows;

public class ScanForChannels
{
    //FIELDS


    //CONSTRUCTOR


    //METHODS
    public static bool CreateChannelsFromFlash(HapcanNode node)
    {
        //find if firmware config exists
        var cfg = HapcanFirmwareConfig.GetMatched(node);
        if (cfg == null)
            return false;

        //get channel
        var channels = new BindingList<HapcanChannel>();

        for (byte i = 0; i < cfg.Channels.ChannelList.Count; i++)
        {
            var channel = CreateChannel(i + 1, node, cfg);
            channels.Add(channel);
        }
        node.Channels = channels;
        return true;
    }
    public static async Task<bool> CreateChannelsFromBusAsync(ResponseReceiver rcv, HapcanNode node, CancellationTokenSource cts)
    {
        //find if firmware config exists
        var cfg = HapcanFirmwareConfig.GetMatched(node);
        if (cfg == null)
            return false;

        //get channel
        var channels = new BindingList<HapcanChannel>();
        var sr = new SystemRequest(node.Subnet.Connection);
        for (byte i = 0; i < cfg.Channels.ChannelList.Count; i++)
        {
            //get channel names
            var name = await sr.ChannelNameRequest(rcv, node, (byte)(i + 1));

            var channel = CreateChannel(i + 1, node, cfg);
            channel.Name = name;
            channels.Add(channel);

            if (cts != null)
                if (cts.Token.IsCancellationRequested)
                    break;
        }
        node.Channels = channels;
        return true;
    }

    private static HapcanChannel CreateChannel(int channelNo, HapcanNode node, HapcanFirmwareConfig cfg)
    {
        //get channel config
        var cfgChannel = cfg.Channels.ChannelList.FirstOrDefault(o => o.Id == channelNo);
        if (cfgChannel == null)
            throw new ArgumentException("Can't find definition of channel number " + channelNo + " in " + cfg.Firmware.Name);

        //create channel
        return new HapcanChannel()
        {
            Node = node,                           //order of properties is important when creating the channel
            Type = cfgChannel.Type,
            Id = channelNo,
            NameAdr = cfgChannel.NameAdr,
            Description = cfgChannel.Description
        };

    } 
}
