using MotorRental.Domain.Enums;

namespace MotorRental.Domain.Dtos
{
    public class RegisterDeliverDriverDto
    {
        //identificador, nome, cnpj, data de nascimento, número da CNHh, tipo da CNH, imagemCNH
        public string FullName { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public long LicenseDriverNumber { get; set; }
        public LicenseDriverTypeEnum LicenseDriverType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
