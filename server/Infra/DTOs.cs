using Facet;
using Infra.Models;


namespace Infra.DTOs;

[Facet(sourceType: typeof(SkiTrip), exclude: nameof(SkiTrip.TotalCost))]
public partial class SkiTripDTO;



[Facet(sourceType: typeof(User), exclude: [nameof(User.PhoneNumber), nameof(User.HomeAddress), nameof(User.AddressCity),
 nameof(User.AddressState), nameof(User.AddressZipCode)])]
public partial class UserGearDTO;