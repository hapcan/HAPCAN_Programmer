using Hapcan.General;
using Hapcan.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hapcan.Flows;

class ReceiveFrame
{
    readonly ConcurrentQueue<HapcanFrame> _queue;
    int _responsetime;
    bool _calculateResponseTime = true;


    private ReceiveFrame(int timeout)
    {
        _responsetime = timeout;
        _queue = new ConcurrentQueue<HapcanFrame>();
    }

    private void OnMessageReceived(HapcanFrame frame)
    {
        _queue.Enqueue(frame);
    }

    private async Task<List<HapcanFrame>> GetResponseList(int[] frameType)
    {
        var frameList = new List<HapcanFrame>();

        //measure time to first response
        Stopwatch sw1 = null;
        if (_calculateResponseTime)
        {
            sw1 = new Stopwatch();
            sw1.Start();
        }

        //measure time after last response
        var sw2 = new Stopwatch();
        sw2.Start();

        //check only for defined response time, no longer
        for (int i = 0; i < _responsetime; i++)
        {
            await Task.Delay(1);
            //anything received?
            if (_queue.Count > 0)
            {
                //for any received frame
                while (_queue.TryDequeue(out var rxFrame) == true)
                {
                    //check if it is a response frame (response frame defined as frameType array)
                    for (int j = 0; j < frameType.Length; j++)
                    {
                        if (rxFrame.GetFrameType() == frameType[j] && rxFrame.IsResponse())
                        {
                            if (_calculateResponseTime)
                            {
                                sw1.Stop();
                                _responsetime = (int)sw1.ElapsedMilliseconds;
                                _calculateResponseTime = false;
                            }
                            sw2.Restart();
                            //create list of response frames
                            frameList.Add(rxFrame);
                        }
                    }
                }
            }
            else
            {
                //if no response for another responsetime*5 then exit
                if (sw2.ElapsedMilliseconds > 3 * _responsetime)
                    return frameList;
            }
        }
        return frameList;
    }

}