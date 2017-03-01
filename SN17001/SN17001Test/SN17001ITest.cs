using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN17001;
using CR.Core.Services;
using CR.Core;

namespace SN17001Test
{
    /// <summary>
    /// Summary description for SN17001ITest
    /// </summary>
    [TestClass]
    public class SN17001ITest
    {
        [TestMethod]
        public void Test17001IScanner()
        {
            SN17001Scanner sn17 = new SN17001Scanner();

            try
            {
                var d = sn17.Scan(@"C:\Working\Project\CampaignPlanner");
                d.Notes = "This is a test scan on a real code base.";
                CRObjSerializer cros = new CRObjSerializer();
                cros.SaveCRObj(CRGlobal.CRScanData + "\\" + d.CRID + "_testScan" + ".xml", d);
            }
            catch (Exception ex)
            {
                throw new AssertFailedException(ex.Message);
            }
            
        }
    }
}
