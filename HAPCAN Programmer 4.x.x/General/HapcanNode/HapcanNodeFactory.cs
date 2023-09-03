using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hapcan.General;

public class HapcanNodeFactory
{
    /// <summary>
    /// Creates HapcanNode object with defined hardware version (processor type)
    /// </summary>
    /// <param name="hardwareVersion">Hardware version of HapcanNode - it depends on processor type. Eg. UNIV3 (hardwareVersion = 3) nodes use PIC18F26K80.</param>
    /// <returns></returns>
    public static HapcanNode CreateHapcanNode(byte hardwareVersion)
    {
        return hardwareVersion switch
        {
            1 => new HapcanNodeUniv1(),
            2 => new HapcanNodeUniv2(),
            3 => new HapcanNodeUniv3(),
            4 => new HapcanNodeUniv4(),
            _ => new HapcanNodeUnivX(hardwareVersion)      //default
        }; 
    }
}
