using Leeti.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;


namespace Letti.Parsers
{
    public class SigerParser
    {
        public List<SiegerCompanyRecord> GetCompanies(string source)
        {
            List<SiegerCompanyRecord> records = new List<SiegerCompanyRecord>();
            int startIndex = 0;
            int recordIndex = 0;
            for(int k=0;k<4;k++)
            {
                startIndex = source.IndexOf("formValores:dataTableCaratula:", startIndex) + 1;
            }
            
            while (source.IndexOf("formValores:dataTableCaratula:",startIndex)>0)
            {
                SiegerCompanyRecord companyRecord = new SiegerCompanyRecord();
                //find id
                int idIndex = source.IndexOf("formValores:dataTableCaratula:", startIndex);
                int endIndex=source.IndexOf('"',idIndex);
                companyRecord.ControlIndex = recordIndex++;
                startIndex = ++endIndex;
                //find company
                idIndex = source.IndexOf("formValores:dataTableCaratula:", startIndex);
                endIndex = source.IndexOf('"', idIndex);
                idIndex = source.IndexOf('>', endIndex)+1;
                endIndex= source.IndexOf('<', endIndex);
                companyRecord.CompanyName= source.Substring(idIndex, endIndex-idIndex);
                startIndex = ++endIndex;
                //roll over to the next record
                idIndex = source.IndexOf("formValores:dataTableCaratula:", startIndex);
                endIndex = source.IndexOf('"', idIndex);
                startIndex = ++endIndex;
                records.Add(companyRecord);
            }
            return records;
        }
    }
}
