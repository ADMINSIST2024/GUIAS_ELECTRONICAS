using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IService
{

	
	[OperationContract]
    ResultFacturacion ProcesaFacturaElectronica(string Tabla, string Usuario);
    // TODO: agregue aquí sus operaciones de servicio

    [OperationContract]
    byte[] ObtenerLog(string Nombre);
	[OperationContract]
	List<ResultListLog>  ObtenerListaLog(string Usuario);
}

[DataContract]
public class ResultFacturacion
{
    [DataMember ]
    public string Mensaje { get; set; }
    [DataMember]
    public string Codigo { get; set; }
    [DataMember]
    public byte[] Contenido { get; set; }
}
[DataContract]
public class ResultListLog
{
	[DataMember]
	public string Nombre { get; set; }
	[DataMember]
	public string Hora { get; set; }
	[DataMember]
	public long Tamano { get; set; }
}
// Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
