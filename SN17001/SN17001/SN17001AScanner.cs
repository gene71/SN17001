using CR.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR.Core;
using System.IO;
using System.Text.RegularExpressions;

namespace SN17001
{
    class SN17001AScanner : ICRIAE
    {
        CRVData crvData;
        public SN17001AScanner()
        {
            crvData = new CRVData();
            crvData.CRID = "N17001";
            
        }

        public CRVData GetCRVData(List<CRIndicator> indicators, CRScanner crscanner)
        {
            int findings = 0;
            bool isFinding = true;
            try
            {
                //loop through indicators
                foreach (var i in indicators)
                {
                    foreach(var p in crscanner.Patterns)
                    {
                        //if indicator has a pattern
                        //then not a finding
                        //else a finding
                        var lines = File.ReadAllLines(i.Path);
                        for (int j = 0; j < lines.Length; j++)
                        {
                            //if any indicator has one of the patterns its not a finding
                            var matches = Regex.Matches(lines[j], p);
                            if (matches.Count > 0)
                            {
                                isFinding = false;

                            }
                            
   
                        }//end lines
                       

                    }//end crscanner patterns

                   
                   //before moving to next indicator increment counter 
                    if (isFinding)
                    { findings++; }
                    isFinding = true;//set back

                }//end indicator loop
                crvData.Indicators = indicators;
                if (findings > 0)
                {
                    crvData.Positive = true;
                }
                else
                {
                    crvData.Positive = false;
                }
                return crvData;
                
            }
            catch (Exception ex)
            {
                throw new SN17001Exception(ex.Message);
            }
        }
            
    }
}
