using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public class CaseToReviewBase:ComponentBase
    {
        [Parameter]
        public Model.CaseToReview CaseToReview { get; set; }
        public string CaseType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SetCaseType();
        }
        private void SetCaseType()
        {
            switch (CaseToReview.CaseType)
            {
                case Model.Enums.CaseType.Address:
                    CaseType = "Dirección Repetida";
                    break;
                case Model.Enums.CaseType.Conflict:
                    CaseType = "Conflicto de Interés";
                    break;
                case Model.Enums.CaseType.RecentCreation:
                    CaseType = "Reciente Creación";
                    break;
            }
        }
    }
}
