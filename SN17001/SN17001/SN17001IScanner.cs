using CR.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR.Core;
using CR.Util;
using System.IO;
using System.Text.RegularExpressions;

namespace SN17001
{
    class SN17001IScanner : ICRISE
    {
        List<CRIndicator> indis;

        public SN17001IScanner()
        {
            indis = new List<CRIndicator>();
        }

        public List<CRIndicator> GetIndicators(string dirPath, CRScanner scanner)
        {
            try
            {
                FileUtil fu = new FileUtil();
                foreach (var f in fu.GetFiles(dirPath))
                {
                    FileInfo fi = new FileInfo(f);
                    foreach (var ex in scanner.FileExtensions)
                    {
                        if (fi.Extension == ex)
                        {
                            //do scan
                            getIndis(f, scanner);

                        }
                    }//end foreach ex
                }//end foreach f
                //May need to add a clean function here to remove duplicate 
                //line triggers
                return indis;

            }
            catch (Exception ex)
            {
                throw new SN17001Exception(ex.Message);
            }

        }

        private void getIndis(string filePath, CRScanner crs)
        {
            try
            {
                foreach (var p in crs.Patterns)
                {
                    var lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var matches = Regex.Matches(lines[i], p);
                        if (matches.Count > 0)
                        {
                            //found match make vul
                            CRIndicator crvul = new CRIndicator();
                            crvul.CRVID = crs.CRVID;
                            crvul.Line = lines[i].ToString();
                            crvul.LineNumber = i + 1;//account for 0
                            crvul.Path = filePath;
                            crvul.MVal = matches[0].Value;
                            //TODO: clean indi
                            indis.Add(crvul);
                        }

                    }//end for 

                }//end foreach
            }
            catch (Exception ex)
            {
                throw new SN17001Exception(ex.Message);
            }
        }
    }
}
