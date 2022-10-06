using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Services
{
    public interface IGenerationService
    {
        public void GenerateCreateCommandCode();
        public void GenerateUpdateCommandCode();
        public void GenerateDeleteCommandCode();
        public void GenerateCreateValidatorCode();
        public void GenerateUpdateValidatorCode();
        public void GenerateDeleteValidatorCode();
        public void GenerateCreatedDtoCode();
        public void GenerateUpdatedDtoCode();
        public void GenerateDeletedDtoCode();
        public void GenerateInterfaceRepositoryCode();
        public void GenerateRepositoryCode();
        public void GenerateConstantCode();
        public void GenerateDtoCode();
        public void GenerateModelCode();
        public void GenerateMappingProfileCode();
        public void GenerateRuleCode();
        public void GenerateGetByIdQueryCode();
        public void GenerateGetListQueryCode();
        public void GenerateGetListByDynamicQueryCode();
    }
}
