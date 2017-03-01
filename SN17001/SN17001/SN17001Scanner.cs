using CR.Core.Services;
using System;
using System.Collections.Generic;
using CR.Core;

namespace SN17001
{
    public class SN17001Scanner : ICRScanner
    {
        public CRScanner Icrscanner;
        public CRScanner Acrscanner;

        private string iScanner = CRGlobal.CRScanners + "\\N17001I.xml";
        private string aScanner = CRGlobal.CRScanners + "\\N17001A.xml";

        public CRVData Scan(string dirPath)
        {
            try
            {
                initScanner();
                //Call the ICRISE implementation for this scanner
                ICRISE icrise = new SN17001IScanner();
                List<CRIndicator> indicators = icrise.GetIndicators(dirPath, Icrscanner);
                
                //Call the ICRIAE implementation for this scanner
                ICRIAE icriae = new SN17001AScanner();
                CRVData crd = icriae.GetCRVData(indicators, Acrscanner);

                return crd;
            }
            catch (Exception ex)
            {
                throw new SN17001Exception(ex.Message);
            }

            
        }

        /// <summary>
        /// initScanner() loads scanner objects from serialized scanner files        
        // </summary>
        /// <exception cref="SN17001Exception">SN17001Exception</exception>
        public void initScanner()
        {
            try
            {
                //Get the scanner objects
                CRObjSerializer cros = new CRObjSerializer();
                Icrscanner = cros.LoadCRScanner(iScanner);
                Acrscanner = cros.LoadCRScanner(aScanner);
            }
            catch (Exception ex)
            {
                throw new SN17001Exception(ex.Message);
            }
        }
    }
}
