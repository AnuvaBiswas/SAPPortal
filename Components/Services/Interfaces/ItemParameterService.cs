using SAPPortal.Models;

namespace SAPPortal.Components.Services
{
    public class ItemParameterService
    {
        private readonly ApplicationDbContext _context;

        public ItemParameterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveItemParameterAsync(ItemParameter itemParameter)
        {
            var entity = new ItemParameter
            {
                ItemCode = itemParameter.ItemCode,
                ItemDescription = itemParameter.ItemDescription,
                EffectiveFrom = itemParameter.EffectiveFrom,
                EffectiveTo = itemParameter.EffectiveTo,
                ProcessStage = itemParameter.ProcessStage,
                NumberOfSample = itemParameter.NumberOfSample,
                Production = itemParameter.Production
           
            };

            _context.ItemParameters.Add(entity);
            await _context.SaveChangesAsync();

            foreach (var param in itemParameter.Parameters)
            {
                var parameterEntity = new ParameterRow
                {
                    ItemParameterId = entity.Id,
                    Code = param.Code,
                    Name = param.Name,
                    Type = param.Type,
                    Standard = param.Standard,
                    MinValue = param.MinValue,
                    MaxValue = param.MaxValue,
                    HoldMinValue = param.HoldMinValue,
                    HoldMaxValue = param.HoldMaxValue,
                    RejMinValue = param.RejMinValue,
                    RejMaxValue = param.RejMaxValue,
                    
                    Mandatory = param.Mandatory,
                    Sequence = param.Sequence,
                    ShowOnPrint = param.ShowOnPrint,
                    AfterBeforeGRN = param.AfterBeforeGRN,
                    ViewOnPO = param.ViewOnPO
                };

                _context.ParameterRows.Add(parameterEntity);
            }

            await _context.SaveChangesAsync();
        }
    }

}
