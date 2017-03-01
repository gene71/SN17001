using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN17001;

namespace SN17001Test
{
    [TestClass]
    public class SN17001Test
    {
        /// <summary>
        /// This test verififes proper initialiation of scanner
        /// verifies scanner paths to xml serialized scanner files
        /// verifies scanner variables
        /// </summary>
        [TestMethod]
        public void TestSN17001Scanner_initScanner()
        {
            try
            {
                SN17001Scanner scanner = new SN17001Scanner();
                scanner.initScanner();

                //Check Indicator scanner
                if (scanner.Icrscanner == null)
                {
                    throw new Exception("failed to load object");
                }else if(scanner.Icrscanner.CRVID != "N17001")
                {
                    throw new Exception("CRVID mismatch");
                }else if(scanner.Icrscanner.Patterns.Count < 1)
                {
                    throw new Exception("Icrscanner does not contain patterns");
                }
                else if (scanner.Icrscanner.FileExtensions.Count < 1) 
                {
                    throw new Exception("Icrscanner does not contain file extensions");
                }

                //Check Analyzer scanner
                if (scanner.Acrscanner == null)
                {
                    throw new Exception("failed to load object");
                }
                else if (scanner.Acrscanner.CRVID != "N17001")
                {
                    throw new Exception("CRVID mismatch");
                }
                else if (scanner.Acrscanner.Patterns.Count < 1)
                {
                    throw new Exception("Icrscanner does not contain patterns");
                }
                else if (scanner.Acrscanner.FileExtensions.Count < 1)
                {
                    throw new Exception("Icrscanner does not contain file extensions");
                }

                
            }
            catch(Exception ex)
            {
                throw new AssertFailedException(ex.Message);
            }
            
            
        }

        
    }
}
