using System;
using System.Web;

namespace SquishIt.Framework.Utilities
{

    public class DebugStatusReader : IDebugStatusReader
    {
        readonly IMachineConfigReader machineConfigReader;
        private bool forceDebug = false;
        private bool forceRelease = false;

        public DebugStatusReader()
            : this(new MachineConfigReader())
        {

        }

        internal DebugStatusReader(IMachineConfigReader machineConfigReader)
        {
            this.machineConfigReader = machineConfigReader;
        }

        public bool IsDebuggingEnabled()
        {
            if(forceDebug)
            {
                return true;
            }

            if(forceRelease)
            {
                return false;
            }

            if(HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled)
            {
                return !TrustLevel.IsHighOrUnrestrictedTrust || machineConfigReader.IsRetailDeployment;
            }
            return false;
        }

        #region IDebugStatusReader Members


        public void ForceDebug()
        {
            forceDebug = true;
        }

        public void ForceRelease()
        {
            forceRelease = true;
        }

        #endregion
    }
}