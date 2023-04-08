using Hapcan.General;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hapcan.Flows;

public class ResponseReceiver : IDisposable
{
    readonly HapcanConnection _conn;
    readonly ConcurrentQueue<HapcanFrame> _queue;
    int _responsetime;
    bool _calculateResponseTime;             //measure real response time and use it as a timeout - it speeds up receiving next frames

    /// <summary>
    /// Receives response HAPCAN frames from the bus
    /// </summary>
    /// <param name="connection">Connection to interface object</param>
    /// <param name="calculateResponseTime">If true then the real real response time will be calculated and used as timeout for the next receiving. If false the Connection timeout is used.</param>
    public ResponseReceiver(HapcanConnection connection, bool calculateResponseTime)
    {
        _conn = connection;
        _calculateResponseTime = calculateResponseTime;
        _responsetime = connection.Timeout;
        _queue = new ConcurrentQueue<HapcanFrame>();
        //subscribe the event
        _conn.CanbusMessageReceived += OnMessageReceived;
        _conn.InterfaceMessageReceived += OnMessageReceived;
    }
    public void Dispose()
    {
        //unsubscribe the event
        _conn.CanbusMessageReceived -= OnMessageReceived;
        _conn.InterfaceMessageReceived -= OnMessageReceived;
    }

    private void OnMessageReceived(HapcanFrame frame)
    {
        _queue.Enqueue(frame);
    }


    /// <summary>
    /// Receives defined frames type and number from the bus withing response time (timeout). Returns list of frames
    /// </summary>
    /// <param name="frameType">Array of frame types to receive</param>
    /// <param name="frames">Number of expected frames. If not entered frames=1000.</param>
    /// <returns>List of received frames List<HapcanFrame></returns>
    public async Task<List<HapcanFrame>> ReceiveAsync(int[] frameType, int frames)
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
            await Task.Delay(TimeSpan.FromMilliseconds(1));
            //anything received?
            if (_queue.Count > 0)
            {
                //for any received frame
                while (_queue.TryDequeue(out var rxFrame) == true)
                {
                    //check if it is a response frame (response frame type defined as frameType array)
                    for (int j = 0; j < frameType.Length; j++)
                    {
                        if (rxFrame.GetFrameType() == frameType[j] && rxFrame.IsResponse())
                        {
                            if (_calculateResponseTime)
                            {
                                sw1.Stop();
                                _responsetime = 5 * (int)sw1.ElapsedMilliseconds;
                                _calculateResponseTime = false;
                            }
                            sw2.Restart();
                            //create list of response frames
                            frameList.Add(rxFrame);
                        }
                    }
                    //if requested number of frames received then exit
                    if (frameList.Count == frames)
                        return frameList;
                }
            }
            else
            {
                //if no response for another responsetime then exit
                if (sw2.ElapsedMilliseconds > _responsetime)
                    return frameList;
            }
        }
        return frameList;
    }

}