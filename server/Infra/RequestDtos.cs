namespace Infra;

public record CreateUserRequestDto(string FirstName, string LastName, string Email, string PhoneNumber, string HomeAddress, string AddressCity, string AddressState, string AddressZipCode);