namespace nArchitectureExtension.Services.GenerationServices.DtoCodeGenerators
{
    public interface IDtoCodeGeneratorService
    {
        public void GenerateCreatedDtoCode();
        public void GenerateUpdatedDtoCode();
        public void GenerateDeletedDtoCode();
        public void GenerateModelDtoCode();
        public void GenerateListModelCode();
    }
}
