namespace nArchitectureExtension.Services.GenerationServices.ValidatorCodeGenerators
{
    public interface IValidatorCodeGeneratorService
    {
        public void GenerateCreateValidatorCode();
        public void GenerateUpdateValidatorCode();
        public void GenerateDeleteValidatorCode();
    }
}
