using DocumentFormat.OpenXml.Spreadsheet;
using Letti.Model;
using ManualImporter.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualImporter.Parser
{
    public class TransparenciaParser : ExcelParser<Contract>
    {
        public Dictionary<int, string> Columns = new Dictionary<int, string>();
        public string Organization { get; private set; }
        public void Load(string filePath)
        {
            
            Initialize(filePath, "Nombre del Sujeto Obligado:", null);

            return;
        }
        public override IList<Contract> Parse(List<string> filters)
        {
            List<Contract> contractRecords = new List<Contract>();
            FileDefinitionReader definitionReader = new FileDefinitionReader();
            int rowCount = 0;
            Organization = string.Empty;
            foreach (var row in Rows)
            {
                if(rowCount==0)
                {
                    string tmp = GetValueString(row.ChildElements[0] as Cell);
                    string data = GetValueString(row.ChildElements[1] as Cell);                  
                    foreach (string filter in filters)
                    {
                        if (data.Contains(filter) || filter.Contains(data))
                        {
                            Organization = filter;
                            definitionReader.Load(Organization);
                            break;
                        }
                    }
                }
                if(rowCount==4)
                {
                    int col=row.ChildElements.Count;
                    for(int k=0;k<col;k++)
                    {
                        string columnName= GetValueString(row.ChildElements[k] as Cell);
                        Columns.Add(k, columnName.Trim());
                    }
                }
                if(rowCount>4)
                { 
                if (!string.IsNullOrEmpty(Organization))
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

                        contract.Organization = new Organization() { OrganizationName = Organization };
                        contract.Contractor = new Contractor();
                        int adjudicadoIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Adjudicado")).Key;
                        string adjudicado= GetValueString(row.ChildElements[adjudicadoIndex] as Cell)+" "+ GetValueString(row.ChildElements[adjudicadoIndex+1] as Cell)+" "+ GetValueString(row.ChildElements[adjudicadoIndex+2] as Cell); 
                        string contractor= GetValueString(row.ChildElements[adjudicadoIndex+3] as Cell);
                        contract.Contractor.TaxId = GetValueString(row.ChildElements[adjudicadoIndex+4] as Cell);
                        contract.Contractor.OfficialName = string.IsNullOrEmpty(contractor) ? adjudicado : contractor;
                        int addressIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Address")).Key;
                        string address = GetValueString(row.ChildElements[addressIndex] as Cell) + " " + GetValueString(row.ChildElements[addressIndex+1] as Cell) + " " +
                        GetValueString(row.ChildElements[addressIndex+2] as Cell) + " " + GetValueString(row.ChildElements[addressIndex+3] as Cell) + " " + GetValueString(row.ChildElements[addressIndex+4] as Cell) + " " +
                        GetValueString(row.ChildElements[addressIndex+5] as Cell);
                        address = address.Replace("ver nota", "");
                        address = address.Replace("No se cuenta con esta informacion", "");
                        contract.Contractor.FiscalAddress = address;
                        contract.Contractor.City = GetValueString(row.ChildElements[addressIndex+7] as Cell);
                        contract.Contractor.State= GetValueString(row.ChildElements[addressIndex+9] as Cell);
                        int biddingIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Bidding")).Key;
                        contract.Bidding = GetValueString(row.ChildElements[biddingIndex] as Cell);

                        int objectTypeIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Object Type")).Key;
                        contract.ContractObjectType = GetValueString(row.ChildElements[objectTypeIndex] as Cell);

                        int contractTypeIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Contract Type")).Key;
                        contract.ContractType = GetValueString(row.ChildElements[contractTypeIndex] as Cell);

                        int contractConceptIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Contract Concept")).Key;
                        contract.Concept = GetValueString(row.ChildElements[contractConceptIndex] as Cell);

                        int contractObjectIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Contract Object")).Key;
                        contract.Description = GetValueString(row.ChildElements[contractObjectIndex] as Cell);

                        int urlIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Url")).Key;
                        contract.Url = GetValueString(row.ChildElements[urlIndex] as Cell);
                        
                        int contractNumberIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Contract Number")).Key;
                        contract.Number = GetValueString(row.ChildElements[contractNumberIndex] as Cell);


                        int contractValueIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Value")).Key;
                        string amount = GetValueString(row.ChildElements[contractValueIndex] as Cell);
                        decimal contractAmount;
                        bool isDecimal = Decimal.TryParse(amount, out contractAmount);
                        if (isDecimal)
                        {
                            contract.Value = contractAmount;
                        }

                        int contractCurrencyIndex = Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Currency")).Key;
                        contract.Currency =StandarizeCurrency(GetValueString(row.ChildElements[contractCurrencyIndex] as Cell).Trim());
                        
                        int dateIndex= Columns.FirstOrDefault(x => x.Value == definitionReader.GetFilePropertyName("Date")).Key;
                        string initial = GetValueString(row.ChildElements[dateIndex] as Cell);

                        DateTime init = DateTime.Now;
                        bool isDate = DateTime.TryParse(initial, out init);
                        if (isDate)
                        {
                            contract.StartDate = init;
                        }
                        string final = GetValueString(row.ChildElements[dateIndex+1] as Cell);

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
                rowCount++;

            }
            return contractRecords;
        }

        public void LoadColumns()
        {
            List<Contract> contractRecords = new List<Contract>();
            int rowCount = 0;
           
            foreach (var row in Rows)
            {
                if (rowCount == 0)
                {
                    string tmp = GetValueString(row.ChildElements[0] as Cell);
                    Organization = GetValueString(row.ChildElements[1] as Cell);
 
                    Organization = Organization.Replace('\n', ' ').TrimEnd();
                    
                }
                if (rowCount == 4)
                {
                    int col = row.ChildElements.Count;
                    for (int k = 0; k < col; k++)
                    {
                        string columnName = GetValueString(row.ChildElements[k] as Cell);
                        Columns.Add(k, columnName.Trim());
                    }
                }
               
                rowCount++;

            }
         
        }
        private string StandarizeCurrency(string original)
        {
            switch(original)
            {
                case "Peso":
                    return "MXN";
                default:
                    return "MXN";
            }
        }
    }
}
