using System;
using System.Threading.Tasks;
using System.Threading;
using Hapcan.General;

namespace Hapcan.Flows;

//declare a delegate type for the event
public delegate void ProgrammingEvent(Programming obj);


public class Programming
{
    //EVENTS
    public event ProgrammingEvent ProgrammingProgress;  //progress event

    //FIELDS
    ProgrammingBase _prgmSub;                           //sub programming class

    //PROPERTIES 
    public int Progress                                 //current read/write/erase progress
    {
        get { return _prgmSub.Progress; }
    }
    public int Address                                  //current read/write/erase address
    {
        get { return _prgmSub.Address; }
    }
    public int BytesRead                                //number of bytes processed
    {
        get { return _prgmSub.BytesRead; }
    }
    public int BytesErased                              //number of bytes processed
    {
        get { return _prgmSub.BytesErased; }
    }
    public int BytesWritten                             //number of bytes processed
    {
        get { return _prgmSub.BytesWritten; }
    }
    public enum ProgrammingAction
    {
        SmartReadData,
        SmartWriteData,
        WriteFirmware
    }

    //CONSTRUCTOR
    /// <summary>
    /// Allows reading, writing and erasing of node memory.
    /// </summary>>
    /// <param name="node">HapcanNode object to be programmed.</param>
    public Programming(HapcanNode node)
    {
        //UNIV3
        if (node.HardwareVersion == 3)
            _prgmSub = new ProgrammingUniv3(node, InvokeProgress);
        //UNIV4
        else if (node.HardwareVersion == 4)
            _prgmSub = new ProgrammingUniv4(node, InvokeProgress);
        //
        else
            throw new ArgumentException("Programming does not support this node.");
    }

    //METHODS
    private void InvokeProgress()
    {
        ProgrammingProgress?.Invoke(this);              //raise event
    }

    /// <summary>
    /// Exits node from programming mode.
    /// </summary>
    /// <returns></returns>
    public async Task ExitProgrammingAsync()
    {
        await _prgmSub.ExitProgrammingAsync();
    }

    /// <summary>
    /// Enters node into programming mode.
    /// </summary>
    /// <returns>True when node correctly responds or false when it doesn't.</returns>
    public async Task EnterProgrammingAsync()
    {
        await _prgmSub.EnterProgrammingAsync();
    }

    /// <summary>
    /// Reads firmware revision from node firmware.
    /// </summary>
    /// <returns>Returns int value of firmware revision.</returns>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task<int> GetFirmwareRevision()
    {
        return await _prgmSub.GetFirmwareRevisionAsync();
    }

    /// <summary>
    /// Changes node name
    /// </summary>
    /// <param name="name">New node name.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task ChangeNodeName(string name)
    {
        await _prgmSub.ChangeNodeNameAsync(name);
    }

    /// <summary>
    /// Changes node id.
    /// </summary>
    /// <param name="node">Node new number.</param>
    /// <param name="group">Node new group number.</param>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    /// <returns></returns>
    public async Task ChangeNodeId(byte node, byte group)
    {
        await _prgmSub.ChangeNodeIdAsync(node, group);
    }


    //SMART READING
    /// <summary>
    /// Reads all data only from occupied memory cells of eeprom and flash
    /// </summary>
    /// <param name="cts">Cancellation Token Source to cancel reading.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Occurs when addrFrom or addrTo is incorrect.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task SmartReadDataMemoryAsync(CancellationTokenSource cts)
    {
        await _prgmSub.SmartReadDataMemoryAsync(cts);     
    }
    //SMART WRITING
    /// <summary>
    /// Writes only data that is different from currently written in the node eeprom and flash memory.
    /// </summary>
    /// <param name="eepromBuffer">byte[0x400] buffer with data to written into eeprom memory.</param>
    /// <param name="flashBuffer">byte[0x8000] buffer with data to written into flash memory.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>/// 
    /// <returns></returns>
    public async Task SmartWriteDataMemoryAsync(byte[] eepromBuffer, byte[] flashBuffer, CancellationTokenSource cts)
    {
        await _prgmSub.SmartWriteDataMemoryAsync(eepromBuffer, flashBuffer, cts);
    }
    //FIRMWARE WRITING
    /// <summary>
    /// Writes firmware data from given input buffer into node flash memory firmware section.
    /// </summary>
    /// <param name="buffer">byte[0x10000] buffer with data to be written into firmware section.</param>
    /// <param name="cts">Cancellation Token Source to cancel writing.</param>
    /// <exception cref="ArgumentException">Occurs when input buffer is null.</exception>
    /// <exception cref="TimeoutException">Occurs when requested node doesn't respond.</exception>
    public async Task WriteFirmwareAsync(byte[] firmBuffer, CancellationTokenSource cts)
    {
        await _prgmSub.WriteFirmwareAsync(firmBuffer, cts);
    }
 
}
