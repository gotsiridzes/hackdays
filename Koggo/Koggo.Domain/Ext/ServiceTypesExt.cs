using Koggo.Domain.Models;

namespace Koggo.Domain.Ext;

public static class ServiceTypesExt
{
	public static string? ToGeo(this ServiceType serviceType)
	{
		return serviceType switch
		{
			ServiceType.Tracktor => "ტრაქტორი",
			ServiceType.AutoKran => "ამწე-კრანი",
			ServiceType.AmweSpiderLift => "Spider Left",
			ServiceType.AmweEuakuatori => "ამწე-ევაკუატორი",
			ServiceType.AmweKalata => "ამწე-კალათა",
			ServiceType.BetonisXelsawyo => "ბეტონის რაღაც",
			ServiceType.GanatebaDaVentilacia => "განათება და ვენტილაცია",
			ServiceType.GanatebisKoshki => "განათების კოშკი",
			ServiceType.Generatori => "გენერატორი",
			ServiceType.MdzimeWonisRolikebi => "მძიმე წონის როლიკები",
			ServiceType.MdzimeWonisTeknika => "მძიმე წონის ტექნიკა",
			ServiceType.SasawyobeTeknika => "სასაწყობე ტექნიკა",
			ServiceType.Satvirtveli => "სატვირთველი",
			ServiceType.SawvavisAvzi => "საწვავის ავზი",
			ServiceType.SaxliEzo => "სახლი/ეზო",
			ServiceType.TeleskopuriSatvirteli => "ტელესკოპური სატვირთველი",
			ServiceType.Xaracho => "ხარაჩო",
			ServiceType.XisDanapoteba => "ხის დანაფოტება",
			ServiceType.HidravlikuriDomkrati => "ჰიდრავლიკური დომკრატო",
			_ => "სხვა"
		};
	}
}