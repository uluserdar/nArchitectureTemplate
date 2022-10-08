namespace nArchitectureExtension.Services.GenerationServices.QueryCodeGenerators
{
    public interface IQueryCodeGeneratorService
    {
        public void GenerateGetByIdQueryCode();
        public void GenerateGetListQueryCode();
        public void GenerateGetListByDynamicQueryCode();
    }
}
