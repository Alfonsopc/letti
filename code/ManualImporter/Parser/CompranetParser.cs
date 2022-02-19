using DocumentFormat.OpenXml.Spreadsheet;
using Letti.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManualImporter.Parser
{
    public class CompranetParser : ExcelParser<Contract>
    {
        
        public CompranetParser()
        {           
        }
        public void Load(string filePath)
        {
            var headers = new List<string>() { "NOMBRE" };
            Initialize(filePath,string.Empty,headers.ToArray());
           

            return ;
        }
        public override IList<Contract> Parse(List<string> filters)
        {
            List<Contract> contractRecords = new List<Contract>();
            foreach (var row in Rows)
            {
                string data= GetValueString(row.ChildElements[2] as Cell);
                string organization = string.Empty;
                foreach (string filter in filters)
                {
                    if (data.Contains(filter) || filter.Contains(data))
                    {
                        organization = filter;
                        break;
                    }
                }
                if(!string.IsNullOrEmpty(organization))
                {
                    Contract contract = new Contract();
                    try
                    {
                        int columnCount = row.ChildElements.Count;
                        if (columnCount < 8)
                        {
                            string test = GetValueString(row.ChildElements[4] as Cell);
                            break;
                        }

                        contract.Organization = new Organization() { OrganizationName = organization };
                        contract.Contractor = new Contractor();
                        contract.Contractor.TaxId = GetValueString(row.ChildElements[37] as Cell);
                        contract.Contractor.OfficialName = GetValueString(row.ChildElements[38] as Cell);
                        contract.Bidding= GetValueString(row.ChildElements[12] as Cell);
                        contract.ContractObjectType= GetValueString(row.ChildElements[17] as Cell);
                        contract.ContractType= GetValueString(row.ChildElements[18] as Cell);                        
                        contract.Number = GetValueString(row.ChildElements[21] as Cell);
                        contract.Concept = GetValueString(row.ChildElements[22] as Cell);
                        contract.Description= GetValueString(row.ChildElements[23] as Cell);
                        contract.Url= GetValueString(row.ChildElements[44] as Cell);
                        string amount = GetValueString(row.ChildElements[26] as Cell);
                        decimal contractAmount;
                        bool isDecimal = Decimal.TryParse(amount, out contractAmount);
                        if (isDecimal)
                        {
                            contract.Value = contractAmount;
                        }
                        contract.Currency= GetValueString(row.ChildElements[27] as Cell);
                        string initial = GetValueString(row.ChildElements[24] as Cell);
                       
                        DateTime init = DateTime.Now;
                        bool isDate = DateTime.TryParse(initial, out init);
                        if (isDate)
                        {
                            contract.StartDate = init;
                        }
                        string final = GetValueString(row.ChildElements[25] as Cell);

                        DateTime end = DateTime.Now;
                        isDate = DateTime.TryParse(final, out end);
                        if (isDate)
                        {
                            contract.EndDate = end;
                        }

                        contractRecords.Add(contract);
                    }
                    catch (Exception exp)
                    {
                        string message = exp.Message;
                    }
                }
               
               
            }
            return contractRecords;
        }
    }
}
